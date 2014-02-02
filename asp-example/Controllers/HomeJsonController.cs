using asp_example.Controllers.ViewModels;
using asp_example.models.Models;
using asp_example.Models.Context;
using asp_example.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_example.Controllers
{
    public class HomeJsonController : Controller
    {
        private ITodoesRepository _todoesRepository;

        public HomeJsonController(ITodoesRepository todoesRepository)
        {
            _todoesRepository = todoesRepository;
        }

        public JsonResult Index()
        {
            var vm = new HomeViewModel();
            var items = _todoesRepository.GetAllTodoes();
            vm.AddItems(items);

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTodo(string description)
        {
            var item = new Todo
            {
                Description = description
            };
            _todoesRepository.Save(item);

            return Json(item); // TODO: Should return next display order number
        }

        [HttpPost]
        public JsonResult Archive(int id)
        {
            var item = _todoesRepository.ArchiveTodo(id);

            return Json(new TodoItemViewModel(item));
        }

        [HttpPost]
        public void Delete(int id)
        {
            _todoesRepository.DeleteTodo(id);
        }
    }
}