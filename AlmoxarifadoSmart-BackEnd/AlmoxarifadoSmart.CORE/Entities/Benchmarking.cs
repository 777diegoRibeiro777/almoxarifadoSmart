using AlmoxarifadoSmart.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Entities
{
    public class Benchmarking
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public StoresEnum Loja { get; set; }

        public decimal Economia { get; set; }

        public string Link { get; set; }

        public StatusEmailEnum StatusEmail { get; set; }



        public DateTime Create_at { get; set; }

        public int IdProduto { get; set; }

        public virtual Produto ProdutoNavegation { get; set; } = null!;



    }
}
