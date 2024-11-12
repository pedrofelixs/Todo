﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Models
{
    public class LogEntry
    {
            public int Id { get; set; }
            public string Level { get; set; }
            public DateTime Timestamp { get; set; }
            public string Message { get; set; }
            public string Source { get; set; }
            public string Details { get; set; }
    }
}
