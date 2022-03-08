using DesafioDev.Core.Hypermedia;

namespace DesafioDev.Core.Interfaces
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
