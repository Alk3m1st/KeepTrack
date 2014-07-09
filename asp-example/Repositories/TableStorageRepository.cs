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
        T Get<T>(Guid id, int userId) where T : TableEntity;
        IList<T> Get<T>(string userId) where T : TableEntity, new();
        void InsertOrReplace<T>(T entity) where T : TableEntity;
    }

    public class TableStorageRepository : ITableStorageRepository
    {
        private CloudStorageAccount _storageAccount;

        public TableStorageRepository()
        {
            _storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
        }

        public void Insert<T>(T entity) where T : TableEntity
        {
            var tableClient = _storageAccount.GetTable<T>();

            var insertOperation = TableOperation.Insert(entity);
            tableClient.Execute(insertOperation);
        }

        public T Get<T>(Guid id, int userId) where T : TableEntity
        {
            var tableClient = _storageAccount.GetTable<T>();

            var retrieveOperation = TableOperation.Retrieve<T>(userId.ToString(), id.ToString());

            return tableClient.Execute(retrieveOperation).Result as T;
        }

        public IList<T> Get<T>(string userId) where T : TableEntity, new()
        {
            var tableClient = _storageAccount.GetTable<T>();

            var query = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId.ToString()));

            return tableClient.ExecuteQuery<T>(query).ToList<T>();
        }

        // TODO: Remove this and use event sourcing
        public void InsertOrReplace<T>(T entity) where T : TableEntity
        {
            var tableClient = _storageAccount.GetTable<T>();

            var insertOperation = TableOperation.InsertOrReplace(entity);
            tableClient.Execute(insertOperation);
        }
    }
}