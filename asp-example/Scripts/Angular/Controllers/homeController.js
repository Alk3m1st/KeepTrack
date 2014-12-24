'use strict';

(function () {
    function HomeController($scope, $http) {
        // Init
        var self = this;
        this.todos = [];
        this.description = '';

        // TODO: Add caching with cacheFactory
        $http.get('/HomeJson')
            .success(function (data) {
                self.todos = data.TodoItems;
            });

        this.itemsClass = "showItems";  // needed?

        // Functions
        this.addTodo = function () {

            // Put in service
            if (this.description) {
                var item = { Description: this.description };

                $http.post('/HomeJson/AddTodo', item)
                    .success(function (newItem) {
                        // TODO: Add display order and filter by it?
                        self.todos.splice(0, 0, newItem); // Put it at the top
                    })
                    .error(function (x) {
                        console.log('fail');
                        console.log(x);
                    });

                self.description = '';
            }
        };

        this.archive = function (item) {
            item.hide = true;

            // TODO: Move to service
            $http.post('/HomeJson/Archive', item)
                .success(function (updatedItem) {
                    var indexToRemove = 0, indexToInsert = 0;
                    console.log("returned");

                    // Reorder the list
                    for (var i = 0; i < self.todos.length; i++) {
                        if (self.todos[i].Id == item.Id) {
                            indexToRemove = i;
                        }

                        if (self.todos[i].Archived === true) {
                            indexToInsert = i;
                        }

                        if (indexToRemove >= 0 && indexToInsert > 0) {
                            self.todos.splice(indexToRemove, 1);

                            self.todos.splice(indexToInsert - 1, 0, updatedItem);

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

        this.delete = function (item) {
            item.hide = true;

            // pop-up to verify delete ?...

            // TODO: Move to service
            $http.post('/HomeJson/Delete', item)
                .success(function () {
                    var indexToRemove = 0;

                    for (var i = 0; i < self.todos.length; i++) {
                        if (self.todos[i].Id === item.Id) {
                            indexToRemove = i;
                        }

                        if (indexToRemove >= 0) {
                            self.todos.splice(indexToRemove, 1);

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