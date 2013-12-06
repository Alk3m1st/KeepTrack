using asp_example.Controllers.ViewModels;
using asp_example.models.Models;
using asp_example.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomeViewModel();

            // TODO: Move to repo and use autofac instead
            using (var db = new TodoContext())
            {
                var items = db.Todos;

                vm.AddItems(items.ToList());
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var newVm = new HomeViewModel();
            // Move to repo and use injector to map from vm to model
            using (var db = new TodoContext())
            {
                db.Todos.Add(new Todo { Description = vm.Description });
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Archive(int id)
        {
            using (var db = new TodoContext())
            {
                var todo = db.Todos.Where(t => t.Id == id)
                    .SingleOrDefault();

                if (todo != null)
                {
                    todo.Archived = true;
                    todo.Completed = DateTime.UtcNow;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new TodoContext())
            {
                var todo = db.Todos.Where(t => t.Id == id).SingleOrDefault();

                if(todo != null)
                {
                    db.Todos.Remove(todo);
                    db.SaveChanges();
                }
            }

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
