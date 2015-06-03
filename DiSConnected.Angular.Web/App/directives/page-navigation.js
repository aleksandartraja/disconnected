(function () {
    'use strict';

    function PageNavigationCtrl($scope) {
        // TODO:
    }

    PageNavigationCtrl.$inject = ['$scope'];

    function pageNavigation() {
        return {
            scope: {
                message: "=",
                navigation: "=",
            },
            templateUrl: '/App/directives/page-navigation.html',
            controller: PageNavigationCtrl,
        };
    }

    angular
        .module('app')
        .directive('pageNavigation', pageNavigation)
        .controller('PageNavigationCtrl', PageNavigationCtrl);
})();