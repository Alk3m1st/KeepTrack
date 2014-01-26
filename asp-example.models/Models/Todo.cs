using System;

namespace asp_example.models.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public int DisplayOrder { get; set; }

        public Todo()
        {
            Created = DateTime.Now;
        }
    }
}