using Microsoft.WindowsAzure.Storage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asp_example.Utils;
using asp_example.models.TableModels;
using Microsoft.WindowsAzure.Storage.Table;
using asp_example.Queries;
using asp_example.Handlers.Queries;

namespace asp_example.tests.Handlers.Queries
{
    [TestFixture]
    public class GetItemsByUserNameQueryHandlerTests
    {
        private CloudStorageAccount StorageAccount;
        private const string UserName = "bob";

        [SetUp]
        public void SetUp()
        {
            // TODO: Move some of this to a base class or extension possibly...
            StorageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            var entity = new TableTodo(UserName, "Description");
            var tableClient = StorageAccount.GetTable<TableTodo>();

            var insertOperation = TableOperation.Insert(entity);
            tableClient.Execute(insertOperation);
        }

        [Test]
        public void HandleQuery()
        {
            var query = new GetItemsByUserNameQuery { UserName = UserName };
            var handler = new GetItemsByUserNameQueryHandler();

            var results = handler.Handle(query);

            Assert.That(results.Count(), Is.EqualTo(1));
        }
    }
}
