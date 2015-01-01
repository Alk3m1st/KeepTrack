using asp_example.interfaces.Queries;
using asp_example.models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Queries
{
    public class GetItemsByUserNameQuery : IQuery<IEnumerable<TableTodo>>
    {
        public string UserName { get; set; }
    }
}