using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemViewModel
    {
        public ProdutoViewModel Produto { get; set; }
        public int Qtde { get; set; }
    }

    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
    }
}
