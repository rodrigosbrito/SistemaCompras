using AutoMapper;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ItemViewModel, SolicitacaoAgg.Item>();
            CreateMap<ProdutoViewModel, ProdutoAgg.Produto>();
        }
    }
}
