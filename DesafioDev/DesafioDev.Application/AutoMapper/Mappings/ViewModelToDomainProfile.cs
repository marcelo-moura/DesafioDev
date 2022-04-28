using AutoMapper;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.AutoMapper.Mappings
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<ProdutoViewModelEntrada, Produto>()
                .ForMember(d => d.CodigoUsuarioCadastro, o => o.MapFrom(s => s.CodigoUsuario))
                .ForMember(d => d.NomeUsuarioCadastro, o => o.MapFrom(s => s.NomeUsuario));

            CreateMap<AtualizarProdutoViewModelEntrada, Produto>()
                .ForMember(d => d.CodigoUsuarioAlteracao, o => o.MapFrom(s => s.CodigoUsuario))
                .ForMember(d => d.NomeUsuarioAlteracao, o => o.MapFrom(s => s.NomeUsuario));


            CreateMap<CategoriaViewModelEntrada, Categoria>()
                .ForMember(d => d.CodigoUsuarioCadastro, o => o.MapFrom(s => s.CodigoUsuario))
                .ForMember(d => d.NomeUsuarioCadastro, o => o.MapFrom(s => s.NomeUsuario));

            CreateMap<AdicionarItemPedidoViewModelEntrada, Pedido>()
                .ForMember(d => d.CodigoUsuarioCadastro, o => o.MapFrom(s => s.CodigoUsuario))
                .ForMember(d => d.NomeUsuarioCadastro, o => o.MapFrom(s => s.NomeUsuario));

            CreateMap<PagamentoViewModelEntrada, Pagamento>();
        }
    }
}
