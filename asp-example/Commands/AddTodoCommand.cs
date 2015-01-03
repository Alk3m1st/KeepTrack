using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Commands
{
    public class AddTodoCommand
    {
        public string UserName { get; private set; }
        public string Description { get; private set; }
        public Guid Id { get; private set; }

        public AddTodoCommand(string userName, string description)
        {
            UserName = userName;
            Description = description;
            Id = Guid.NewGuid();
        }
    }
}