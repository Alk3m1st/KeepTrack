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

        private string getUserName()
        {
            return ControllerContext.HttpContext.User.Identity.Name;
        }

        public HomeController(ITableTodoRepository tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public ActionResult Index()
        {
            return View();
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

        public ActionResult Migration()
        {
            // Grose service locator but only for the one off migration
            var todoRepo = DependencyResolver.Current.GetService<ITodoesRepository>();

            var allOldTodos = todoRepo.GetAllTodoesByUsername(getUserName());
            var allTableTodos = _tableStorageRepository.Get<TableTodo>(getUserName());

            var todosForMigration = allOldTodos
                .Where(t => !allTableTodos.Any(att => att.Created == t.Created))
                .Select(t => new TableTodo(t.Archived,
                    t.Completed,
                    t.Created,
                    t.Description,
                    t.DisplayOrder,
                    getUserName()))
                .ToList();

            foreach (var todo in todosForMigration)
            {
                _tableStorageRepository.InsertOrReplace<TableTodo>(todo);
            }

            return (View(todosForMigration));
        }
    }
}
