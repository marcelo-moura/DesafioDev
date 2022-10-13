using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : BaseController
    {
        private readonly ITokenService _tokenService;
        public AuthController(INotificador notificador,
                              ITokenService tokenService) : base(notificador)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signIn")]
        public async Task<IActionResult> SingIn([FromBody] LoginViewModelEntrada loginEntrada)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);

            var token = await _tokenService.ValidateCredentials(loginEntrada);
            return CustomResponse(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenViewModelEntrada tokenEntrada)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);

            var token = await _tokenService.ValidateCredentials(tokenEntrada);
            return CustomResponse(token);
        }

        [HttpGet]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var login = User?.Claims?.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
            var result = await _tokenService.RevokeToken(login);
            return CustomResponse(result);
        }
    }
}
