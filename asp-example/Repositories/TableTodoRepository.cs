using asp_example.Utils;
using asp_example.models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace asp_example.Repositories
{
    public interface ITableTodoRepository : ITableStorageRepository//<TableTodo>
    {
        void Archive(Guid id, string userId);
    }

    public class TableTodoRepository : TableStorageRepository<TableTodo>, ITableTodoRepository
    {
        public void Archive(Guid id, string userId)
        {
            var tableClient = StorageAccount.GetTable<TableTodo>();

            var retrieveOperation = TableOperation.Retrieve<TableTodo>(userId.ToString(), id.ToString());
            var entity = tableClient.Execute(retrieveOperation).Result as TableTodo;

            if(entity == null)
                return;

            entity.Archived = true;

            base.InsertOrReplace(entity);
        }
    }
}