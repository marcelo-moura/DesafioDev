using System.Text;

namespace DesafioDev.Core.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }
        private string href;
        public string Href
        {
            get
            {
                object _lock = new object();
                lock (_lock)
                {
                    var sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").Replace("?version=1", "").ToString();
                }
            }
            set { href = value; }
        }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
