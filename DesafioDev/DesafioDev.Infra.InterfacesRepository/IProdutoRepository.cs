﻿using DesafioDev.Business.Models;
using DesafioDev.Infra.InterfacesRepository.Base;

namespace DesafioDev.Infra.InterfacesRepository
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<List<Produto>> ObterPorCategoria(Guid? categoriaId);
    }
}
