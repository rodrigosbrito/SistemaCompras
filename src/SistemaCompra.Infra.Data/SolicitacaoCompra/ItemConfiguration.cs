using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class ItemConfiguration : IEntityTypeConfiguration<Domain.SolicitacaoCompraAggregate.Item>
    {
        public void Configure(EntityTypeBuilder<Domain.SolicitacaoCompraAggregate.Item> builder)
        {
            builder.ToTable("Item");

            builder.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

            builder.Property<Guid?>("ProdutoId")
                .HasColumnType("uniqueidentifier");

            builder.Property<int>("Qtde")
                .HasColumnType("int");

            builder.Property<Guid?>("SolicitacaoCompraId")
                .HasColumnType("uniqueidentifier");

            builder.HasKey("Id");

            builder.HasIndex("ProdutoId");

            builder.HasIndex("SolicitacaoCompraId");

            builder.HasOne("SistemaCompra.Domain.ProdutoAggregate.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

            builder.HasOne("SistemaCompra.Domain.SolicitacaoCompraAggregate.SolicitacaoCompra", null)
                        .WithMany("Itens")
                        .HasForeignKey("SolicitacaoCompraId");
        }
    }
}
