using AlmoxarifadoSmart.API;
using AlmoxarifadoSmart.Core.Entities;
using AlmoxarifadoSmart.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly db_almoxarifadoContext _context;

        public LogRepository(db_almoxarifadoContext context)
        {
            _context = context;
        }

        public async Task InsertLog(LogModel log)
        {
            _context.LOGROBO.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LogModel>> GetAllLogs()
        {
            return await _context.LOGROBO.ToListAsync();
        }

        public async Task<List<LogModel>> GetPagedLogs(int page, int pageSize, string filter)
        {
            if(filter == "Selecione")
            {
                return await _context.LOGROBO.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return await _context.LOGROBO.Where(x => x.InformacaoLog == filter).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetTotalLogsCount()
        {
            return await _context.LOGROBO.CountAsync();
        }
    }
}
