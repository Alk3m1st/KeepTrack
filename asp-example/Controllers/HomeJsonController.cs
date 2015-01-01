using asp_example.Controllers.ViewModels;
using asp_example.Handlers.Queries;
using asp_example.interfaces.Queries;
using asp_example.models.Models;
using asp_example.models.TableModels;
using asp_example.Models.Context;
using asp_example.Queries;
using asp_example.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_example.Controllers
{
    [Authorize]
    public class HomeJsonController : Controller    // TODO swap to Web ApiController
    {
        private ITableTodoRepository _tableTodoRepository;
        private IQueryHandler<GetItemsByUserNameQuery, IEnumerable<TableTodo>> _itemsByUserNameQueryHandler;

        public HomeJsonController(ITableTodoRepository tableTodoRepository,
            IQueryHandler<GetItemsByUserNameQuery, IEnumerable<TableTodo>> itemsByUserNameQueryHandler)
        {
            _tableTodoRepository = tableTodoRepository;
            _itemsByUserNameQueryHandler = itemsByUserNameQueryHandler;
        }

        public JsonResult Index()
        {
            var userName = ControllerContext.HttpContext.User.Identity.Name;

            var vm = new HomeViewModel();
            var query = new GetItemsByUserNameQuery(userName);
            var items = _itemsByUserNameQueryHandler.Handle(query);
            vm.AddItems(items.ToList());

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTodo(string description)
        {
            var userName = ControllerContext.HttpContext.User.Identity.Name;
            var todo = new TableTodo(userName, description);
            _tableTodoRepository.Insert<TableTodo>(todo);

            return Json(_tableTodoRepository.Get<TableTodo>(todo.Id, userName)); // TODO: Should just return next display order number? Or get updated by another mechanism like SignalR
        }

        [HttpPost]
        public JsonResult Archive(string id)
        {
            var guidId = new Guid(id);
            var userName = ControllerContext.HttpContext.User.Identity.Name;
            _tableTodoRepository.Archive(guidId, userName); // TODO: Make it async/await?

            return Json(new TodoItemViewModel(_tableTodoRepository.Get<TableTodo>(guidId, userName)));
        }

        [HttpPost]
        //[HttpDelete]
        public void Delete(string id)
        {
            var guidId = new Guid(id);
            var userName = ControllerContext.HttpContext.User.Identity.Name;

            _tableTodoRepository.Delete<TableTodo>(guidId, userName);
        }
    }
}