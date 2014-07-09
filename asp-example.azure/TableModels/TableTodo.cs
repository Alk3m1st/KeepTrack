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
        public string Description { get; set; }
        public bool Archived { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
        public int DisplayOrder { get; set; }
        public int UserId { get; set; }
        public bool Deleted { get; set; }

        public TableTodo()
        {
            Created = DateTime.Now;
        }

        public TableTodo(int userId, string description) : this()
        {
            this.Id = new Guid();
            this.RowKey = this.Id.ToString();

            this.UserId = userId;
            this.PartitionKey = this.UserId.ToString();

            this.Description = description;
        }
    }
}
