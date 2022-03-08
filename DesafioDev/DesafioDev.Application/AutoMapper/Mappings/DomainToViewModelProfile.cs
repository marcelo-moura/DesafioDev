using AutoMapper;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.AutoMapper.Mappings
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Produto, ProdutoViewModelSaida>()
                .ForMember(d => d.CodigoUsuario, o => o.MapFrom(s => s.CodigoUsuarioCadastro))
                .ForMember(d => d.NomeUsuario, o => o.MapFrom(s => s.NomeUsuarioCadastro))
                .ForMember(d => d.Links, o => o.Ignore());
        }
    }
}
