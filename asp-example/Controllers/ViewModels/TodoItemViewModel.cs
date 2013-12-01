using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Controllers.ViewModels
{
    public class TodoItemViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public string Created { get; set; }
    }
}