(function () {
    'use strict';

    function ExampleCtrl($scope) {
        
    }

    ExampleCtrl.$inject = ['$scope'];

    function example() {
        return {
            scope: {
                message: "=",
            },
            templateUrl: '/App/directives/example.html',
            controller: ExampleCtrl,
        };
    }

    angular
        .module('app')
        .directive('example', example)
        .controller('ExampleCtrl', ExampleCtrl);
})();