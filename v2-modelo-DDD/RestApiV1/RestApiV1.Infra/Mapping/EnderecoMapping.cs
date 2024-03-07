
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiV1.Domain.Models;

namespace RestApiV1.Infra.Mapping
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(x => x.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(x => x.Complemento)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Enderecos");
        }
    }
}
