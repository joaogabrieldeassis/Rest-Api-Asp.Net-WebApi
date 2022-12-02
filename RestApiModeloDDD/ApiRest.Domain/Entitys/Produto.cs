using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Domain.Entitys
{
    public class Produto
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool IsDisponivel{ get; set; }
    }
}
