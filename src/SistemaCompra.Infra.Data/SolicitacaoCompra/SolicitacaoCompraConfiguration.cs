using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder.HasKey("Id");

            builder.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

            builder.Property<DateTime>("Data")
                .HasColumnType("datetime2");

            builder.Property(c => c.Situacao)
                .HasColumnName("Situacao")
                .HasColumnType("int");

            builder.OwnsOne(c => c.TotalGeral, b => b.Property("Value")
                .HasColumnName("TotalGeral")
                .HasColumnType("decimal(18,2)"));

            builder.OwnsOne(c => c.CondicaoPagamento, b => b.Property("Valor")
                .HasColumnName("CondicaoPagamento")
                .HasColumnType("int"));

            builder.OwnsOne(c => c.NomeFornecedor, b => b.Property("Nome")
                .HasColumnName("NomeFornecedor")
                .HasColumnType("varchar(100)"));

            builder.OwnsOne(c => c.UsuarioSolicitante, b => b.Property("Nome")
                .HasColumnName("UsuarioSolicitante")
                .HasColumnType("varchar(100)"));
        }
    }
}
