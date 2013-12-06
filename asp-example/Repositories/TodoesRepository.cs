using asp_example.models.Models;
using asp_example.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Repositories
{
    public interface ITodoesRepository
    {
        IList<Todo> GetAllTodoes();
        void AddTodoFromDescription(string description);
        void ArchiveTodo(int id);
        void DeleteTodo(int id);
    }

    // TODO: Add tests
    public class TodoesRepository : ITodoesRepository
    {
        public IList<Todo> GetAllTodoes()
        {
            var items = new List<Todo>();

            using (var db = new TodoContext())
            {
                items = db.Todos.ToList();
            }

            return items;
        }
        
        public void AddTodoFromDescription(string description)
        {
            using (var db = new TodoContext())
            {
                db.Todos.Add(new Todo { Description = description });
                db.SaveChanges();
            }
        }


        public void ArchiveTodo(int id)
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
        }

        public void DeleteTodo(int id)
        {
            using (var db = new TodoContext())
            {
                var todo = db.Todos.Where(t => t.Id == id).SingleOrDefault();

                if (todo != null)
                {
                    db.Todos.Remove(todo);
                    db.SaveChanges();
                }
            }
        }
    }
}