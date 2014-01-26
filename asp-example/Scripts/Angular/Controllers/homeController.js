'use strict';

window.app.controller("HomeController", ['$scope', '$http',   /* This format allows minification without variable naming issues */
    function ($scope, $http) {
        // Init
        $scope.todos = [];
        $scope.description = '';

        $http.get('/HomeJson')
            .success(function (data) {
                $scope.todos = data.TodoItems;
            });

        $scope.itemsClass = "showItems";

        $scope.addTodo = function () {
            // Put in service
            if (this.description) {
                console.log(this.description);
                var item = { Description: this.description };

                $http.post('/HomeJson/AddTodo', item)
                    .success(function (id) {
                        console.log('success');
                        console.log(id);
                        item.Id = id;

                        // TODO: Add display order and filter by it
                        $scope.todos.push(item);

                        $scope.description = '';
                    })
                    .error(function (x) {
                        console.log('fail');
                        console.log(x);
                    });
            }
        };
    }]
);