using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (loginEntrada == null) return BadRequest("Invalid user request!");

            var token = await _tokenService.ValidateCredentials(loginEntrada);
            if (token == null) return Unauthorized();

            return CustomResponse(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenViewModelEntrada tokenEntrada)
        {
            if (tokenEntrada is null) return BadRequest("Invalid user request!");

            var token = await _tokenService.ValidateCredentials(tokenEntrada);
            if (token == null) return BadRequest("Invalid user request!");

            return CustomResponse(token);
        }

        [HttpGet]        
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var login = User?.Identity?.Name;
            var result = await _tokenService.RevokeToken(login);

            if (!result) return BadRequest("Invalid client request!");

            return CustomResponse(result);
        }
    }
}
