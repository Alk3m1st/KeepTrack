'use strict';

(function () {
    function HomeController($scope, $http, TodoService) {
        // Init
        var self = this;
        this.todos = [];
        this.description = '';

        // TODO: Add caching with cacheFactory
        $http.get('/HomeJson')
            .success(function (data) {
                self.todos = data.TodoItems;
            })
            .error(function () {
                self.description = "Error loading Todos...";
            });

        this.itemsClass = "showItems";  // Used currently to prevent FoUC

        // Functions
        this.addTodo = function () {
            if (this.description) {
                TodoService.addTodo(this.description, self.todos);
                self.description = '';
            }
        };

        this.archive = function (item) {
            item.hide = true;

            TodoService.archiveTodo(item, self.todos);
        };

        this.delete = function (item) {
            if (confirm("Are you sure you want to delete this todo?")) {
                item.hide = true;
                TodoService.deleteTodo(item, self.todos);
            }
        };
    }

    angular.module("KeepTrack")
        .controller("HomeController", ['$scope', '$http', 'TodoService', HomeController]);
}());