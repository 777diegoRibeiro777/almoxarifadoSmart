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
    public class BenchmarkingRepository : IBenchmarkingRepository
    {
        private readonly db_almoxarifadoContext _dbContext;

        public BenchmarkingRepository(db_almoxarifadoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task AdicionarBenchmarking(Benchmarking benchmarking)
        {
            _dbContext.Benchmarkings.Add(benchmarking);
            await _dbContext.SaveChangesAsync();
        }

    }
}
