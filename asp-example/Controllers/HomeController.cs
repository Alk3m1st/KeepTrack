using asp_example.Controllers.ViewModels;
using asp_example.models.Models;
using asp_example.models.TableModels;
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
        // TODO: Hook up a service and some queues
        private ITableTodoRepository _tableStorageRepository;

        public HomeController(ITableTodoRepository tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public ActionResult Index()
        {
            var userName = ControllerContext.HttpContext.User.Identity.Name;

            var vm = new HomeViewModel();
            var items = _tableStorageRepository.Get<TableTodo>(userName);
            vm.AddItems(items);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var userName = ControllerContext.HttpContext.User.Identity.Name;
            _tableStorageRepository.Insert<TableTodo>(new TableTodo(userName, vm.Description));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Archive(string id)
        {
            _tableStorageRepository.Archive(new Guid(id), ControllerContext.HttpContext.User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            _tableStorageRepository.Delete<TableTodo>(new Guid(id), ControllerContext.HttpContext.User.Identity.Name);

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
