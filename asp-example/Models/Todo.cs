using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace asp_example.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }

        public Todo()
        {
            Created = DateTime.Now;
        }
    }
}