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
    [Authorize]
    public class HomeController : Controller
    {
        private ITodoesRepository _todoesRepository;

        public HomeController(ITodoesRepository todoesRepository)
        {
            _todoesRepository = todoesRepository;
        }

        public ActionResult Index()
        {
            var userName = ControllerContext.HttpContext.User.Identity.Name;

            var vm = new HomeViewModel();
            var items = _todoesRepository.GetAllTodoesByUsername(userName);
            vm.AddItems(items);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var userName = ControllerContext.HttpContext.User.Identity.Name;

            _todoesRepository.AddTodoFromDescription(vm.Description, userName);

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

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "A|lk3my About Page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "A|k3my Contact Page.";

            return View();
        }
    }
}
