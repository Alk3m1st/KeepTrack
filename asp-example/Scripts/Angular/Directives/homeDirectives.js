'use strict';

angular.module("KeepTrack").directive('itemActions', function () {
    return {
        restrict: 'A',
        replace: false,
        templateUrl: 'Scripts/Angular/Directives/Templates/itemActions.html'
        //link: function(scope, iElement, iAttrs) {}
    };
});