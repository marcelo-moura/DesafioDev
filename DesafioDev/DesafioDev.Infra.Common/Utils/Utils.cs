using LinqKit;
using System.ComponentModel;
using System.Reflection;

namespace DesafioDev.Infra.Common.Utils
{
    public static class Utils
    {
        public static string GerarCodigoPedido()
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public static string GetDescription<T>(this T source)
        {
            FieldInfo fieldInfo = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }

        public static ExpressionStarter<TResult> MontarPredicateFiltro<TResult, T>(T objFiltro)
        {
            var predicate = PredicateBuilder.New<TResult>();

            var filtroType = objFiltro.GetType();
            foreach (var property in filtroType.GetProperties())
            {
                var field = property.Name;
                var valor = property.GetValue(objFiltro, null);
                if (valor != null && !string.IsNullOrEmpty(valor.ToString()))
                {
                    predicate = predicate.And(x => x.GetType().GetProperty(field).GetValue(x) != null &&
                                                   x.GetType().GetProperty(field).GetValue(x).ToString().Contains(valor.ToString(), StringComparison.OrdinalIgnoreCase));
                }
            }
            return predicate;
        }
    }
}
