using DesafioDev.Business.Models.Base;

namespace DesafioDev.Business.Models
{
    public class Usuario : Entity
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Codigo { get; private set; }
        public string NomeCompleto { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiryTime { get; private set; }

        protected Usuario() { }

        public void SetRefreshToken(string? refreshToken) => RefreshToken = refreshToken;
        public void SetRefreshTokenExpiryTime(DateTime? refreshTokenExpiryTime) => RefreshTokenExpiryTime = refreshTokenExpiryTime;
    }
}
