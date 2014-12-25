'use strict';

(function () {
    function TodoService($http) {
        var TodoService = {};

        TodoService.addTodo = function (description, todos) {
            var item = { Description: description };

            $http.post('/HomeJson/AddTodo', item)
                .success(function (newItem) {
                    todos.splice(0, 0, newItem); // Put it at the top
                })
                .error(function (x) {
                    console.log('fail');
                    console.log(x);
                });
        }

        TodoService.archiveTodo = function (item, todos) {
            if (item.Archived === true) {
                item.hide = false;
                return;
            }

            $http.post('/HomeJson/Archive', item)
                .success(function (updatedItem) {
                    var indexToRemove = 0, indexToInsert = 0;

                    // Reorder the list
                    for (var i = 0; i < todos.length; i++) {
                        if (todos[i].Id == item.Id) {
                            indexToRemove = i;
                        }

                        if (todos[i].Archived === true) {
                            indexToInsert = i;
                        }

                        if (indexToRemove >= 0 && indexToInsert > 0) {
                            todos.splice(indexToRemove, 1);
                            todos.splice(indexToInsert - 1, 0, updatedItem);

                            return;
                        }
                    }
                })
                .error(function () {
                    item.hide = false;
                    // TODO: Publish archive failed event
                });
        }

        TodoService.deleteTodo = function (item, todos) {
            $http.post('/HomeJson/Delete', item)    // TODO: Use correct HTTPverb
                .success(function () {
                    var i, indexToRemove = -1;

                    for (i = 0; i < todos.length; i++) {
                        if (todos[i].Id === item.Id) {
                            indexToRemove = i;
                        }

                        if (indexToRemove >= 0) {
                            todos.splice(indexToRemove, 1);

                            return;
                        }
                    }
                })
                .error(function () {
                    item.hide = false;
                    // TODO: Publish delete failed event
                });
        }

        return TodoService;
    }

    angular.module("KeepTrack")
        .factory('TodoService', ['$http', TodoService]);
}());