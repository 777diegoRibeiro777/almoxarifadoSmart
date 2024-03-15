using AlmoxarifadoSmart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Core.Repositories
{
    public interface ILogRepository
    {
        Task InsertLog(LogModel log);
        Task<List<LogModel>> GetAllLogs();
        Task<List<LogModel>> GetPagedLogs(int page, int pageSize, string filter);
        Task<int> GetTotalLogsCount();
    }
}
