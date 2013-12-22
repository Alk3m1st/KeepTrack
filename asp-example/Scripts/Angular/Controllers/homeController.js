'use strict';

window.app.controller("HomeController", ['$scope', '$http',   /* This format allows minification without variable naming issues */
    function ($scope, $http) {
        $http.get('/HomeJson')
            .success(function (data) {
                $scope.todos = data.TodoItems;
            });

        $scope.itemsClass = "showItems";
    }]
);