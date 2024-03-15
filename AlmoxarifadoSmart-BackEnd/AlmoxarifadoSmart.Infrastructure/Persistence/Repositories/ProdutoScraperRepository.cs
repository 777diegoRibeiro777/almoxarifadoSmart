
using AlmoxarifadoSmart.API;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Infrastructure.Persistence.Repositories
{
    public class ProdutoScraperRepository : IProdutoScraperRepository
    {
        private readonly db_almoxarifadoContext _dbContext;

        public ProdutoScraperRepository(db_almoxarifadoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AdicionarProdutoScraper(ProdutoScraperModel produtoScraper)
        {
            _dbContext.ProdutoScraper.Add(produtoScraper);
            await _dbContext.SaveChangesAsync();
        }

    }
}
