using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Scrapers
{
    public interface IScraperMercadoLivre
    {
      
            StoreProdutoModel GetInfoProduct(string descricaoProduto, int idProduto);
        
    }
}
