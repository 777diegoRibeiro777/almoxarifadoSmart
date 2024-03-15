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
    public class StoreProdutoRepository : IStoreProdutoRepository
    {
        private readonly db_almoxarifadoContext _dbContext;

        public StoreProdutoRepository(db_almoxarifadoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AdicionarStoreProduto(StoreProdutoModel storeProduto)
        {
            _dbContext.StoreProdutos.Add(storeProduto);
            await _dbContext.SaveChangesAsync();
        }

    }
}
