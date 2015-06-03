(function () {
    'use strict';

    function PageHeaderCtrl($scope) {
        // TODO:
    }

    PageHeaderCtrl.$inject = ['$scope'];

    function pageHeader() {
        return {
            scope: {
                message: "=",
                header: "=",
            },
            templateUrl: '/App/directives/page-header.html',
            controller: PageHeaderCtrl,
        };
    }

    angular
        .module('app')
        .directive('pageHeader', pageHeader)
        .controller('PageHeaderCtrl', PageHeaderCtrl);
})();