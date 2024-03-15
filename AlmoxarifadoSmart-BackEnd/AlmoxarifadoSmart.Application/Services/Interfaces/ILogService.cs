using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Services.Interfaces
{
    public interface ILogService
    {
        Task RegistrarLog(string usuRob, DateTime dateLog, string processo, string infLog, int idProd);
        Task<List<LogModel>> GetAll();
        Task<List<LogModel>> GetPagedLogs(int page, int pageSize, string filter);
        Task<int> GetTotalLogsCount();
    }
}
