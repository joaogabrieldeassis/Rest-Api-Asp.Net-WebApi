using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Data.Mappings
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
