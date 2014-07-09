using asp_example.models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asp_example.azure.Helpers
{
    public static class TableTodoHelper
    {
        public static TableTodo CreateTableTodo(string description, string userId)
        {
            var isInt = false;
            int uId;
            isInt = Int32.TryParse(userId, out uId);

            if (!isInt)
                return null;

            var todo = new TableTodo(uId, description);

            return todo;
        }
    }
}
