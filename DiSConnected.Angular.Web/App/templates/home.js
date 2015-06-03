(function () {
    'use strict';

    function HomeCtrl($scope, $state, $rootScope, Config, Article, $timeout) {
        $scope.message = 'Hello from HomeCtrl';
        $scope.exampleMessage = 'Hello from HomeCtrl (via Example directive)';
        $scope.logLevel = Config.logLevel;
        Article.query(function (articles) {
            $scope.articles = articles;
            $timeout(function () {
                $scope.$apply();
            });
        }, function (error) {
            console.log('Article endpoint returned an error.', error);
        });

    }

    HomeCtrl.$inject = ['$scope', '$state', '$rootScope', 'Config', 'Article', '$timeout'];

    angular
        .module('app')
        .controller('HomeCtrl', HomeCtrl);

})();