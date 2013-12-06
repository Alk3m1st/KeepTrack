using asp_example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using asp_example.Utils;

namespace asp_example.Controllers.ViewModels
{
    public class HomeViewModel
    {
        public string Description { get; set; }

        public IList<TodoItemViewModel> TodoItems { get; private set; }

        public HomeViewModel()
        {
            TodoItems = new List<TodoItemViewModel>();
        }

        public void AddItem(TodoItemViewModel item)
        {
            TodoItems.Add(item);
        }

        public void AddItems(IList<Todo> items)
        {
            foreach (var item in items
                .OrderByDescending(i => i.Created)
                .OrderByDescending(i => i.Completed)
                .OrderBy(i => i.Archived))
            {
                // TODO: Use injector?
                this.AddItem(new TodoItemViewModel
                {
                    Archived = item.Archived,
                    Created = item.Created.GetNiceDateFormat(),
                    Completed = item.Completed.HasValue ? item.Completed.Value.GetNiceDateFormat() : string.Empty,
                    ElapsedDaysClass = item.Completed.GetElapsedDaysClass(),
                    Description = item.Description,
                    Id = item.Id
                });
            }
        }
    }
}