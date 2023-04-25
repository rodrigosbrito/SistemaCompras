using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            Itens = new List<Item>();
            TotalGeral = new Money();
        }

        public void AdicionarItem(Produto produto, int qtde) => Itens.Add(new Item(produto, qtde));

        public void ObterTotalGeral()
        {
            foreach (var item in Itens)
               TotalGeral = TotalGeral.Add(item.Subtotal);
        }

        private void AdicionarCondicaoPagamento()
        {
            if (TotalGeral.Value > 50000m) CondicaoPagamento = new CondicaoPagamento(30);
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (!itens.Any()) throw new BusinessRuleException("A solicitação de compra deve possuir itens!");

            foreach (var item in itens)
                AdicionarItem(item.Produto, item.Qtde);

            ObterTotalGeral();

            AdicionarCondicaoPagamento();

            AddEvent(new CompraRegistradaEvent(Id, Itens, TotalGeral.Value));
        }
        
    }
}
