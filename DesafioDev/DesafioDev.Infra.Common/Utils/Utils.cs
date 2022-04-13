namespace DesafioDev.Infra.Common.Utils
{
    public static class Utils
    {
        public static string GerarCodigoPedido()
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
