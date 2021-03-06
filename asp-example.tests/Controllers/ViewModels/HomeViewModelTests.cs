﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using asp_example.Controllers.ViewModels;
using asp_example.models.Models;
using asp_example.models.TableModels;

namespace asp_example.tests.Controllers.ViewModels
{
    [TestFixture]
    public class HomeViewModelShould
    {
        [Test]
        public void Add_item_from_todo_entities()
        {
            var tableTodo = Builder<TableTodo>.CreateNew().Build();
            var todo = new TodoItemViewModel(tableTodo);

            var vm = new HomeViewModel();
            vm.AddItem(todo);

            Assert.That(vm.TodoItems.Count, Is.EqualTo(1));
            Assert.That(vm.TodoItems[0].Id, Is.EqualTo(todo.Id));
        }

        [Test]
        public void Add_items_from_list_of_todo_entities()
        {
            var todoes = Builder<Todo>.CreateListOfSize(4).Build();

            var vm = new HomeViewModel();
            vm.AddItems(todoes);

            Assert.That(vm.TodoItems.Count, Is.EqualTo(4));
        }
    }
}
