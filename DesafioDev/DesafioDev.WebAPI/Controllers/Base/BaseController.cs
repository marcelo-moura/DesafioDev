using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.Controllers.Base
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotificador _notificador;

        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object data = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    succes = true,
                    data
                });
            }

            return RetornarBadRequest(data);
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        private ActionResult RetornarBadRequest(object result)
        {
            return BadRequest(new
            {
                succes = false,
                erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem),
                detail = result
            });
        }
    }
}
