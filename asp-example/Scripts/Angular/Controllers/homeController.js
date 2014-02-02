'use strict';

angular.module("KeepTrack").controller("HomeController", ['$scope', '$http', '$filter' ,   /* This format allows minification without variable naming issues */
    function ($scope, $http) {
        // Init
        $scope.todos = [];
        $scope.description = '';

        // TODO: Add caching with cacheFactory
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
                    .success(function (newItem) {
                        // TODO: Add display order and filter by it?
                        $scope.todos.splice(0, 0, newItem); // Put it at the top

                        $scope.description = '';
                    })
                    .error(function (x) {
                        console.log('fail');
                        console.log(x);
                    });
            }
        };

        $scope.archive = function (item) {
            
            // TODO: Move to service
            $http.post('/HomeJson/Archive', item)
                .success(function (updatedItem) {
                    var indexToRemove = 0, indexToInsert = 0;

                    console.log(item.Id);

                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].Id == item.Id) {
                            indexToRemove = i;
                        }

                        if ($scope.todos[i].Archived === true) {
                            indexToInsert = i;
                        }

                        if (indexToRemove > 0 && indexToInsert > 0) {
                            $scope.todos.splice(indexToRemove, 1);

                            $scope.todos.splice(indexToInsert, 0, updatedItem);

                            return;
                        }
                    }
                })
                .error(function () {
                    console.log("error");
                });
        };

        $scope.delete = function (item) {
            console.log(item);
        };
    }]
);