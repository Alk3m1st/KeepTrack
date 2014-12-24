'use strict';

angular.module("KeepTrack").directive('itemActions', function () {
    return {
        restrict: 'A',
        replace: false,
        templateUrl: 'Scripts/Angular/Directives/Templates/itemActions.html',
        scope: {
            'todoItem': '=',
            'delete': '&onDelete',
            'archive': '&onArchive'
        }
    };
});