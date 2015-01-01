using asp_example.interfaces.Queries;
using asp_example.Queries;
using asp_example.models.TableModels;
using asp_example.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

namespace asp_example.Handlers.Queries
{
    public class GetItemsByUserNameQueryHandler : IQueryHandler<GetItemsByUserNameQuery, IEnumerable<TableTodo>>
    {
        protected CloudStorageAccount StorageAccount;

        public GetItemsByUserNameQueryHandler() // TODO: Move somewhere common
        {
            StorageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
        }

        public IEnumerable<TableTodo> Handle(GetItemsByUserNameQuery query)
        {
            var tableClient = StorageAccount.GetTable<TableTodo>();
            var tableQuery = new TableQuery<TableTodo>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, query.UserName));

            return tableClient.ExecuteQuery<TableTodo>(tableQuery).ToList<TableTodo>();
        }
    }
}