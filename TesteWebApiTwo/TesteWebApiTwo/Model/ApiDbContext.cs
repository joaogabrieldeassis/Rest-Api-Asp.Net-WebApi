using Microsoft.EntityFrameworkCore;

namespace TesteWebApiTwo.Model
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Fornecedor> Fornecedores{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=MinhaApiRest;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
        }
    }
}
