using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Data;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Infrastruture.Repositories
{
    public class LogRepository: ILogRepository
    {
        private readonly TodoDataContext _context;

        public LogRepository(TodoDataContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(LogEntry log)
        {
            await _context.LogEntries.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
