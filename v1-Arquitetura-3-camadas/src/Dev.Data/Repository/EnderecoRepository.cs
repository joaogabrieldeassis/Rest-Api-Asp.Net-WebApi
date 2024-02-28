using Dev.Bussines.Interfaces;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) 
        {

        }
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await _context.Enderecos.AsNoTracking().FirstOrDefaultAsync(x => x.FornecedorId == fornecedorId);
        }
    }
}
