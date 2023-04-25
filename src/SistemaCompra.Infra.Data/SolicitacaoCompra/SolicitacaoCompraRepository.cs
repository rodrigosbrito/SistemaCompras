using System.Linq;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : SolicitacaoCompraAgg.ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext _context;

        public SolicitacaoCompraRepository(SistemaCompraContext context) => _context = context;

        public void RegistrarCompra(SolicitacaoCompraAgg.SolicitacaoCompra entity)
        {
            entity.Itens.ToList().ForEach(item => _context.Attach(item.Produto));
            _context.Set<SolicitacaoCompraAgg.SolicitacaoCompra>().Add(entity);
        }
    }
}
