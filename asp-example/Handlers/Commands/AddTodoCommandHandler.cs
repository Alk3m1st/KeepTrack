using asp_example.Commands;
using asp_example.interfaces.Commands;
using asp_example.models.TableModels;
using asp_example.Utils;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace asp_example.Handlers.Commands
{
    public class AddTodoCommandHandler : ICommandHandler<AddTodoCommand>
    {
        protected CloudStorageAccount StorageAccount;

        public AddTodoCommandHandler()
        {
            StorageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
        }

        public void Handle(AddTodoCommand command)
        {
            var todo = new TableTodo(command.UserName, command.Description, command.Id);
            var tableClient = StorageAccount.GetTable<TableTodo>();

            var insertOperation = TableOperation.Insert(todo);
            tableClient.Execute(insertOperation);
        }
    }
}