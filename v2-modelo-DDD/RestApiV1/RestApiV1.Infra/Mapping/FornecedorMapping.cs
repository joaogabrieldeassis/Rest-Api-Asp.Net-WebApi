
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiV1.Domain.Models;

namespace RestApiV1.Infra.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.HasOne(c => c.Endereco)
                .WithOne(c=>c.Fornecedor);

            builder.HasMany(c => c.Produtos)
                .WithOne(c => c.Fornecedor)
                .HasForeignKey(c=>c.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
