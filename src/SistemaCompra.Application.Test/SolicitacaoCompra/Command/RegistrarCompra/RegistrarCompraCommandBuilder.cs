using Castle.Components.DictionaryAdapter;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SistemaCompra.Application.Test.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandBuilder
    {
        private string _usuarioSolicitante = "Usuario Solicitante";
        private string _nomeFornecedor = "Fornecedor";
        private IList<ItemViewModel> _itens = new List<ItemViewModel>();

        public RegistrarCompraCommandBuilder ComUsuarioSolicitante(string usuarioSolicitante)
        {
            _usuarioSolicitante = usuarioSolicitante;
            return this;
        }

        public RegistrarCompraCommandBuilder ComNomeFornecedor(string nomeFornecedor)
        {
            _nomeFornecedor = nomeFornecedor;
            return this;
        }

        public RegistrarCompraCommandBuilder ComApenasUmItem(int qtdeProdutos, Guid? idProduto = null)
        {
            _itens.Add(new ItemViewModel { Produto = new ProdutoViewModel { Id = idProduto != null ? idProduto.Value : Guid.NewGuid() }, Qtde = qtdeProdutos });
            return this;
        }

        public RegistrarCompraCommandBuilder ComItemsCustomizados(int qtdeItens, int qtdeProdutos)
        {
            for (int i = 1; i <= qtdeItens; i++)
                _itens.Add(new ItemViewModel { Produto = new ProdutoViewModel { Id = Guid.NewGuid() }, Qtde = qtdeProdutos });
            return this;
        }

        public RegistrarCompraCommand Build()
        {
            return new RegistrarCompraCommand 
            { 
                UsuarioSolicitante = _usuarioSolicitante, 
                NomeFornecedor = _nomeFornecedor,
                Itens = _itens
            };
        }
    }
}
