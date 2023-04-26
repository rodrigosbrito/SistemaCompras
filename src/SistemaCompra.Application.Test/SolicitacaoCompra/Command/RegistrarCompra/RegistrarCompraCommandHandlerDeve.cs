using MediatR;
using Moq;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.Test.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandlerDeve
    {
        private readonly Mock<SolicitacaoCompraAgg.ISolicitacaoCompraRepository> _solicitacaoCompraRepository;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMediator> _mediator;
        private readonly RegistrarCompraCommandHandler _handler;

        public RegistrarCompraCommandHandlerDeve()
        {
            _solicitacaoCompraRepository = new Mock<SolicitacaoCompraAgg.ISolicitacaoCompraRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _handler = new RegistrarCompraCommandHandler(_solicitacaoCompraRepository.Object, _uow.Object, _mediator.Object, _produtoRepository.Object);
        }


        /// <summary>
        /// Este teste verifica se o Metodo Handle Registra uma compra corretamente usando o Repositorio
        /// </summary>
        [Fact]
        public void RegistrarCompraNoRepositorio()
        {
            // Dado
            var produto = new Domain.ProdutoAggregate.Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            var request = new RegistrarCompraCommandBuilder()
                .ComApenasUmItem(10, produto.Id)
                .Build();
                        
            _produtoRepository.Setup(r => r.Obter(produto.Id)).Returns(produto);

            // Quando
            var handle = _handler.Handle(request, CancellationToken.None);

            // Então
            _solicitacaoCompraRepository.Verify(r => r.RegistrarCompra(It.IsAny<SolicitacaoCompraAgg.SolicitacaoCompra>()), Times.Once);
            _uow.Verify(u => u.Commit(), Times.Once);
            _mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.AtLeastOnce);
            Assert.True(handle.Result);

        }

        [Fact]
        public async Task NotificarErroQuandoNaoEncontrarProduto()
        {
            // Dado
            var request = new RegistrarCompraCommandBuilder()
                .ComApenasUmItem(10)
                .Build();
            // simular produto não encontrado
            _produtoRepository.Setup(r => r.Obter(request.Itens.First().Produto.Id)).Returns((Domain.ProdutoAggregate.Produto)null);

            //Quando 
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request, CancellationToken.None));

            //Então
            Assert.Equal($"Produto {request.Itens.First().Produto.Id} não encontrado!", ex.Message);
        }
    }
}
