using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;
using DesafioDev.Core.DomainObjects;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DesafioDev.Application.Services
{
    public class TokenService : ITokenService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public TokenService(IOptions<TokenConfiguration> tokenConfiguration,
                            IUsuarioRepository usuarioRepository,
                            IMapper mapper)
        {
            _tokenConfiguration = tokenConfiguration.Value;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<TokenViewModelSaida> ValidateCredentials(LoginViewModelEntrada loginEntrada)
        {
            var usuario = await _usuarioRepository.BuscarPor(u => u.Login == loginEntrada.Login);
            if (usuario == null) return null;

            var pass = ComputeHash(loginEntrada.Senha, new SHA256CryptoServiceProvider());

            if (usuario.Senha != pass) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login)
            };

            var accessToken = await GenerateAccessToken(claims);
            var refreshToken = await GenerateRefreshToken();

            usuario.SetRefreshToken(refreshToken);
            usuario.SetRefreshTokenExpiryTime(DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry));

            await RefreshUserInfo(usuario);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenViewModelSaida
            {
                Authenticated = true,
                Created = createDate.ToString(DATE_FORMAT),
                Expiration = expirationDate.ToString(DATE_FORMAT),
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenViewModelSaida> ValidateCredentials(RefreshTokenViewModelEntrada tokenEntrada)
        {
            var accessToken = tokenEntrada.AccessToken;
            var refreshToken = tokenEntrada.RefreshToken;

            var principal = await GetPrincipalFromExpiredToken(accessToken);

            var login = principal?.Identity?.Name;
            var user = await _usuarioRepository.BuscarPor(u => u.Login == login);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return null;

            accessToken = await GenerateAccessToken(principal.Claims);
            refreshToken = await GenerateRefreshToken();

            user.SetRefreshToken(refreshToken);
            user.SetRefreshTokenExpiryTime(DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry));

            await RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenViewModelSaida
            {
                Authenticated = true,
                Created = createDate.ToString(DATE_FORMAT),
                Expiration = expirationDate.ToString(DATE_FORMAT),
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<bool> RevokeToken(string login)
        {
            var user = await _usuarioRepository.BuscarPor(u => u.Login == login);

            if (user == null) return false;

            user.SetRefreshToken(null);
            await _usuarioRepository.Atualizar(user);

            return true;
        }

        private string ComputeHash(string senha, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(senha);
            Byte[] hasehdBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hasehdBytes);
        }

        private Task<string> GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenHandle = new JwtSecurityTokenHandler();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _tokenConfiguration.Issuer,
                                             audience: _tokenConfiguration.Audience,
                                             claims: claims,
                                             expires: DateTime.Now.AddMinutes(_tokenConfiguration.Minutes),
                                             signingCredentials: signinCredentials);

            var encondedToken = tokenHandle.WriteToken(token);
            return Task.FromResult(encondedToken);
        }

        private Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Task.FromResult(Convert.ToBase64String(randomNumber));
            }
        }

        private async Task<Usuario> RefreshUserInfo(Usuario usuario)
        {
            if (await _usuarioRepository.ExisteRegistro(u => u.Login != usuario.Login)) return null;

            if (usuario != null)
            {
                try
                {
                    await _usuarioRepository.Atualizar(usuario);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return usuario;
        }

        private async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret)),
                ValidateLifetime = false
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandle.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
               !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token!");

            return await Task.FromResult(principal);
        }
    }
}
