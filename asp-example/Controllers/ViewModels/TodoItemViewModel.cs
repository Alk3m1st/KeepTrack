using asp_example.models.Models;
using asp_example.models.TableModels;
using asp_example.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Controllers.ViewModels
{
    public class TodoItemViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public string Created { get; set; }
        public string Completed { get; set; }
        public string ElapsedDaysClass { get; set; }

        // TODO: say bye bye
        public TodoItemViewModel(Todo item)
        {
            Archived = item.Archived;
            Created = item.Created.GetNiceDateFormat();
            Completed = item.Completed.HasValue ? item.Completed.Value.AddHours(8).GetNiceDateFormat() : string.Empty;
            ElapsedDaysClass = item.Completed.GetElapsedDaysClass();
            Description = item.Description;
            Id = item.Id.ToString();
        }

        public TodoItemViewModel(TableTodo item)
        {
            Archived = item.Archived;
            Created = item.Created.GetNiceDateFormat();
            Completed = item.Completed.HasValue ? item.Completed.Value.GetNiceDateFormat() : string.Empty;
            ElapsedDaysClass = item.Completed.GetElapsedDaysClass();
            Description = item.Description;
            Id = item.Id.ToString();
        }
    }
}