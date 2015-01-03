using asp_example.Commands;
using asp_example.Handlers.Commands;
using asp_example.models.TableModels;
using asp_example.Utils;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp_example.tests.Handlers.Commands
{
    [TestFixture]
    public class AddTodoCommandHandlerTests
    {
        private CloudStorageAccount StorageAccount;
        private const string UserName = "bob";

        [SetUp]
        public void SetUp()
        {
            StorageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
        }

        [Test]
        public void HandleCommand()
        {
            var command = new AddTodoCommand(UserName, "Description");
            var handler = new AddTodoCommandHandler();

            handler.Handle(command);

            var tableClient = StorageAccount.GetTable<TableTodo>();
            var retrieveOperation = TableOperation.Retrieve<TableTodo>(command.UserName, command.Id.ToString());
            var entity = tableClient.Execute(retrieveOperation).Result as TableTodo;

            Assert.That(entity, Is.Not.Null);
            Assert.That(entity.UserId, Is.EqualTo(UserName));
            Assert.That(entity.Id, Is.EqualTo(command.Id));
        }
    }
}
