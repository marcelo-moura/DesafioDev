using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace DesafioDev.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, Serilog.ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    string mensagemErro = string.Empty;
                    string stackTrace = string.Empty;
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error.InnerException != null)
                            mensagemErro = contextFeature.Error.InnerException.Message;
                        else
                            mensagemErro = contextFeature.Error.Message;

                        stackTrace = contextFeature.Error.StackTrace;

                        var detalhesErro = new DetalhesErro()
                        {
                            Erro = mensagemErro,
                            StatusCode = context.Response.StatusCode,
                            Detalhes = stackTrace
                        }.ToString();

                        logger.Error(detalhesErro);
                        await context.Response.WriteAsync(detalhesErro);
                    }
                });
            });
        }

        public class DetalhesErro
        {
            public string Erro { get; set; }
            public string Detalhes { get; set; }
            public int StatusCode { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
