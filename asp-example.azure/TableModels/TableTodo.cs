using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp_example.models.TableModels
{
    public class TableTodo : TableEntity
    {
        public Guid Id { get; set; }
        public bool Archived { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string UserId { get; set; }

        public TableTodo()
        {
            Created = DateTime.Now;
        }

        public TableTodo(bool archived,
            DateTime? completed,
            DateTime created,
            string description,
            int displayOrder,
            string userId)
        {
            Id = Guid.NewGuid();
            this.RowKey = this.Id.ToString();

            Archived = archived;
            Completed = completed;
            Created = created;
            Description = description;
            DisplayOrder = displayOrder;
            UserId = userId;
            this.PartitionKey = this.UserId.ToString();
        }

        public TableTodo(string userId, string description) : this()
        {
            this.Id = Guid.NewGuid();
            this.RowKey = this.Id.ToString();

            this.UserId = userId;
            this.PartitionKey = this.UserId.ToString();

            this.Description = description;
        }
    }
}
