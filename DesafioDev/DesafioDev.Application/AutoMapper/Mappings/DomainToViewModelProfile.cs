using AutoMapper;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.AutoMapper.Mappings
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Produto, ProdutoViewModelSaida>()
                .ForMember(d => d.CodigoUsuario, o => o.MapFrom(s => s.CodigoUsuarioCadastro))
                .ForMember(d => d.NomeUsuario, o => o.MapFrom(s => s.NomeUsuarioCadastro));
        }
    }
}
