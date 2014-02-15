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
        IList<Todo> GetAllTodoesByUsername(string userName);
        Todo AddTodoFromDescription(string description, string userName);
        Todo ArchiveTodo(int id);
        void DeleteTodo(int id);
        int Save(Todo item);
    }

    // TODO: Add tests
    public class TodoesRepository : ITodoesRepository
    {
        public IList<Todo> GetAllTodoesByUsername(string userName)
        {
            var items = new List<Todo>();

            using (var db = new TodoContext())
            {
                items = db.Todos
                    .Where(t => t.User.UserName == userName)
                    .OrderBy(t => t.Id)
                    .OrderByDescending(t => t.DisplayOrder)
                    .ToList();
            }

            return items;
        }
        
        public Todo AddTodoFromDescription(string description, string userName)
        {
            var item = new Todo();

            using (var db = new TodoContext())
            {
                var user = db.UserProfiles.Where(u => u.UserName == userName).FirstOrDefault();

                if (user == null)
                    return item; // TODO: return something more meaningful perhaps

                item.Description = description;
                item.User = user;
                db.Todos.Add(item);
                db.SaveChanges();
            }

            return item;
        }


        public Todo ArchiveTodo(int id)
        {
            var todo = new Todo();

            using (var db = new TodoContext())
            {
                todo = db.Todos.Where(t => t.Id == id)
                    .SingleOrDefault();

                if (todo != null)
                {
                    todo.Archived = true;
                    todo.Completed = DateTime.UtcNow;
                    db.SaveChanges();
                }
            }

            return todo;
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

        public int Save(Todo item)
        {
            using (var db = new TodoContext())
            {
                var todo = db.Todos.Add(item);
                db.SaveChanges();
            }

            return item.Id;
        }
    }
}