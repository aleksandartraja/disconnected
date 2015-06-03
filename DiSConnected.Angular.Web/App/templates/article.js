(function () {
    'use strict';

    function ArticleCtrl($scope, $state, $rootScope, Config, Article, $timeout, $stateParams) {
        $scope.message = 'Hello from ArticleCtrl';
        $scope.exampleMessage = 'Hello from ArticleCtrl (via Example directive)';
        $scope.logLevel = Config.logLevel;
        Article.get({ id: $stateParams.id }, function (article) {
            $scope.article = article;
            $timeout(function () {
                $scope.$apply();
            });
        }, function (error) {
            console.log('Article endpoint returned an error.', error);
        });

    }

    ArticleCtrl.$inject = ['$scope', '$state', '$rootScope', 'Config', 'Article', '$timeout', '$stateParams'];

    angular
        .module('app')
        .controller('ArticleCtrl', ArticleCtrl);

})();