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
    public class HomeController : Controller
    {
        private ITodoesRepository _todoesRepository;

        public HomeController(ITodoesRepository todoesRepository)
        {
            _todoesRepository = todoesRepository;
        }

        public ActionResult Index()
        {
            var vm = new HomeViewModel();
            var items = _todoesRepository.GetAllTodoes();
            vm.AddItems(items);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _todoesRepository.AddTodoFromDescription(vm.Description);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Archive(int id)
        {
            _todoesRepository.ArchiveTodo(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _todoesRepository.DeleteTodo(id);

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
