'use strict';

(function () {
    function HomeController($scope, $http) {
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
                var item = { Description: this.description };

                $http.post('/HomeJson/AddTodo', item)
                    .success(function (newItem) {
                        // TODO: Add display order and filter by it?
                        $scope.todos.splice(0, 0, newItem); // Put it at the top

                        //$scope.description = '';
                    })
                    .error(function (x) {
                        console.log('fail');
                        console.log(x);
                    });

                this.description = '';
            }
        };

        $scope.archive = function (item) {
            item.hide = true;

            // TODO: Move to service
            $http.post('/HomeJson/Archive', item)
                .success(function (updatedItem) {
                    var indexToRemove = 0, indexToInsert = 0;
                    console.log("returned");

                    // Reorder the list
                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].Id == item.Id) {
                            indexToRemove = i;
                        }

                        if ($scope.todos[i].Archived === true) {
                            indexToInsert = i;
                        }

                        if (indexToRemove >= 0 && indexToInsert > 0) {
                            $scope.todos.splice(indexToRemove, 1);

                            $scope.todos.splice(indexToInsert - 1, 0, updatedItem);

                            return;
                        }
                    }
                })
                .error(function () {
                    item.hide = false;
                    // do stuff
                    console.log("error");
                });
        };

        $scope.delete = function (item) {
            item.hide = true;

            // pop-up to verify delete somehow...

            // TODO: Move to service
            $http.post('/HomeJson/Delete', item)
                .success(function () {
                    var indexToRemove = 0;

                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].Id == item.Id) {
                            indexToRemove = i;
                        }

                        if (indexToRemove > 0) {
                            $scope.todos.splice(indexToRemove, 1);

                            return;
                        }
                    }
                })
                .error(function () {
                    item.hide = false;
                    // do stuff
                    console.log("error");
                });
        };
    }

    angular.module("KeepTrack")
        .controller("HomeController", ['$scope', '$http', '$filter', HomeController]);
}());