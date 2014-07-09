using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Utils
{
    public static class TableStorageHelpers
    {
        public static CloudTable GetTable(this CloudStorageAccount storageAccount, string tableName)
        {
            // Create the table client
            var cloudTableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist
            var table = cloudTableClient.GetTableReference(tableName.ToLowerInvariant());
            table.CreateIfNotExists();

            return table;
        }
        
        public static CloudTable GetTable<T>(this CloudStorageAccount storageAccount) where T : TableEntity
        {
            // Create the table client
            var cloudTableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist
            var table = cloudTableClient.GetTableReference(typeof(T).Name.ToLowerInvariant());
            table.CreateIfNotExists();

            return table;
        }
    }
}