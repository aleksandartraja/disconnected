(function () {
    'use strict';

    function Subsection1PageCtrl($scope, $state, $rootScope) {
        $scope.message = 'Hello from Subsection1PageCtrl';
    }

    Subsection1PageCtrl.$inject = ['$scope', '$state', '$rootScope'];

    angular
        .module('app')
        .controller('Subsection1PageCtrl', Subsection1PageCtrl);

})();