using Microsoft.EntityFrameworkCore;
using RestApiV1.Domain.Models;

namespace RestApiV1.Infra.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext() { }
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(c => c.GetProperties()
                .Where(c => c.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

                modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);
        }
    }
}
