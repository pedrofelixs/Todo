using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface ILogRepository
    {
        Task AddLogAsync(LogEntry log);

    }
}
