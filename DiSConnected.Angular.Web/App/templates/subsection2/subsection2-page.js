(function () {
    'use strict';

    function Subsection2PageCtrl($scope, $state, $rootScope) {
        $scope.message = 'Hello from Subsection2PageCtrl';
    }

    Subsection2PageCtrl.$inject = ['$scope', '$state', '$rootScope'];

    angular
        .module('app')
        .controller('Subsection2PageCtrl', Subsection2PageCtrl);

})();