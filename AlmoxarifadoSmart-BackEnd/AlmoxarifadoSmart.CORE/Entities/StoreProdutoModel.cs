using AlmoxarifadoSmart.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Entities
{
    public class StoreProdutoModel
    {

        public int Id { get; set; }
        public StoresEnum Store { get; set; }

        public decimal Price { get; set; }

        public string Link { get; set; }

        public int ProdutoScraperModelId { get; set; }
        public ProdutoScraperModel? ProdutoScraperModel { get; set; }



    }
}
