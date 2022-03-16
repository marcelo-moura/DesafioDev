using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;

namespace DesafioDev.Application.Interfaces
{
    public interface ITokenService
    {
        Task<TokenViewModelSaida> ValidateCredentials(LoginViewModelEntrada loginEntrada);
        Task<TokenViewModelSaida> ValidateCredentials(RefreshTokenViewModelEntrada token);
        Task<bool> RevokeToken(string login);
    }
}
