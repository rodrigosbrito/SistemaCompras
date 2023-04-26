using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
        [Fact]
        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            itens.Add(new Item(produto, 50));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            Assert.Equal(30, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void DefinirPrazo0DiasAoComprarMenos50mil()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1000);
            itens.Add(new Item(produto, 50));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            Assert.Equal(0, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(itens));

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }

        [Fact]
        public void CalcularTotalGeralAoComprarUmProduto() 
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            itens.Add(new Item(produto, 50));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            Assert.Equal(50050, solicitacao.TotalGeral.Value);
        }

        [Fact]
        public void CalcularTotalGeralComMaisProdutos() 
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto1 = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            var produto2 = new Produto("Nogueira", "Transversal 3/3", Categoria.Madeira.ToString(), 1000);
            itens.Add(new Item(produto1, 50));
            itens.Add(new Item(produto2, 1));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            Assert.Equal(51050, solicitacao.TotalGeral.Value);
        }

        [Fact]
        public void AdicionarItemCorretamente()
        {
            // Arrange
            var solicitacaoCompra = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            int qtde = 5;

            // Act
            solicitacaoCompra.AdicionarItem(produto, qtde);

            // Assert
            Assert.Single(solicitacaoCompra.Itens);
            Assert.Equal(produto, solicitacaoCompra.Itens[0].Produto);
            Assert.Equal(qtde, solicitacaoCompra.Itens[0].Qtde);
        }

        [Fact]
        public void AdicionarMultiplosItensCorretamente()
        {
            // Arrange
            var solicitacaoCompra = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var produto1 = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            int qtde1 = 5;
            var produto2 = new Produto("Nogueira", "Transversal 3/3", Categoria.Madeira.ToString(), 1000);
            int qtde2 = 3;

            // Act
            solicitacaoCompra.AdicionarItem(produto1, qtde1);
            solicitacaoCompra.AdicionarItem(produto2, qtde2);

            // Assert
            Assert.Equal(2, solicitacaoCompra.Itens.Count);
            Assert.Equal(produto1, solicitacaoCompra.Itens[0].Produto);
            Assert.Equal(qtde1, solicitacaoCompra.Itens[0].Qtde);
            Assert.Equal(produto2, solicitacaoCompra.Itens[1].Produto);
            Assert.Equal(qtde2, solicitacaoCompra.Itens[1].Qtde);
        }
    }
}
