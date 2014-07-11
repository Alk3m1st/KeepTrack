using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using asp_example.Utils;

namespace asp_example.Repositories
{
    public interface ITableStorageRepository
    {
        void Insert<T>(T entity) where T : TableEntity;
        T Get<T>(Guid id, string userId) where T : TableEntity;
        IList<T> Get<T>(string userId) where T : TableEntity, new();
        void InsertOrReplace<T>(T entity) where T : TableEntity;
        void Delete<T>(Guid id, string userId) where T : TableEntity;
    }

    public class TableStorageRepository<T> : ITableStorageRepository//<T>
    {
        protected CloudStorageAccount StorageAccount;

        public TableStorageRepository()
        {
            StorageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
        }

        public void Insert<T>(T entity) where T : TableEntity
        {
            var tableClient = StorageAccount.GetTable<T>();

            var insertOperation = TableOperation.Insert(entity);
            tableClient.Execute(insertOperation);
        }

        public T Get<T>(Guid id, string userId) where T : TableEntity
        {
            var tableClient = StorageAccount.GetTable<T>();

            var retrieveOperation = TableOperation.Retrieve<T>(userId.ToString(), id.ToString());
            return tableClient.Execute(retrieveOperation).Result as T;
        }

        public IList<T> Get<T>(string userId) where T : TableEntity, new()
        {
            var tableClient = StorageAccount.GetTable<T>();

            var query = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId.ToString()));

            return tableClient.ExecuteQuery<T>(query).ToList<T>();
        }

        // TODO: Remove this and use event sourcing
        public void InsertOrReplace<T>(T entity) where T : TableEntity
        {
            var tableClient = StorageAccount.GetTable<T>();

            var insertOperation = TableOperation.InsertOrReplace(entity);
            tableClient.Execute(insertOperation);
        }

        public void Delete<T>(Guid id, string userId) where T : TableEntity
        {
            // TODO: Don't actually delete!
            var tableClient = StorageAccount.GetTable<T>();

            var retrieveOperation = TableOperation.Retrieve<T>(userId.ToString(), id.ToString());
            var entity = tableClient.Execute(retrieveOperation).Result as T;

            if (entity == null)
                return;

            var deleteOperation = TableOperation.Delete(entity);
            tableClient.Execute(deleteOperation);
        }
    }
}