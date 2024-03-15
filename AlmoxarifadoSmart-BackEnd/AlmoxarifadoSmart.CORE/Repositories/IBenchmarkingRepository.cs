using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Repositories
{
    public interface IBenchmarkingRepository
    {
        Task AdicionarBenchmarking(Benchmarking benchmarking);
    }
}
