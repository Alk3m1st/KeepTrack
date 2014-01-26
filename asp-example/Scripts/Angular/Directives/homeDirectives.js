'use strict';

window.app.directive('itemActions', function () {
    return {
        restrict: 'A',
        replace: true,
        template: '<form action="#" method="post">' +
            '<input type="hidden" value="{{item.Id}}" name="id">' +
            '<input type="submit" value="Done" name="Archive" data-ng-click="">' +
            '</form>',
        controller: 'homeController'
    }
});