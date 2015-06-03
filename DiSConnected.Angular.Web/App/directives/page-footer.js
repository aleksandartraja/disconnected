(function () {
    'use strict';

    function PageFooterCtrl($scope) {
        // TODO:
    }

    PageFooterCtrl.$inject = ['$scope'];

    function pageFooter() {
        return {
            scope: {
                message: "=",
                footer: "=",
            },
            templateUrl: '/App/directives/page-footer.html',
            controller: PageFooterCtrl,
        };
    }

    angular
        .module('app')
        .directive('pageFooter', pageFooter)
        .controller('PageFooterCtrl', PageFooterCtrl);
})();