(function() {
    'use strict';

    function PageCtrl($scope, $state, $rootScope, Site, $timeout) {
        // TODO:
        Site.get({}, function (site) {
            $scope.site = site;
            $timeout(function () {
                $scope.$apply();
            });
        }, function (error) {
            console.log('Site endpoint returned an error.', error);
        });
    }

    PageCtrl.$inject = ['$scope', '$state', '$rootScope', 'Site', '$timeout'];

    angular
        .module('app')
        .controller('PageCtrl', PageCtrl);

})();