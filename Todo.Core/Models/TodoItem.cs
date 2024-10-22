﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;  

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? DueDate { get; set; }  
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
