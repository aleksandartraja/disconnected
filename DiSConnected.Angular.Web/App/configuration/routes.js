(function () {
    'use strict';

    // app route configuration
    function routes($locationProvider, $stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");

        // Rewrite the url without hashes and use the history API
        $locationProvider.html5Mode(true);

        // Nested state-based routes via Angular-UI-Router
        $stateProvider
            // wrapper route
            .state('page', {
                abstract: true,
                templateUrl: '/App/templates/page.html',
                controller: 'PageCtrl'
            })
            // home route
            .state('home', {
                url: '/',
                templateUrl: '/App/templates/home.html',
                controller: 'HomeCtrl',
                parent: 'page'
            })
            // article route
            .state('article', {
                url: '/article/:id',
                templateUrl: '/App/templates/article.html',
                controller: 'ArticleCtrl',
                parent: 'page'
            })
            // subection 1 routes
            .state('subsection1', {
                abstract: true,
                url: '/subsection1',
                templateUrl: '/App/templates/subsection1/subsection1.html',
                parent: 'page'
            })
            .state('subsection1.page', {
                url: '/',
                templateUrl: '/App/templates/subsection1/subsection1-page.html',
                controller: 'Subsection1PageCtrl',
                parent: 'subsection1'
            })
            // subection 2 routes
            .state('subsection2', {
                abstract: true,
                url: '/subsection2',
                templateUrl: '/App/templates/subsection2/subsection2.html',
                parent: 'page'
            })
            .state('subsection2.page', {
                url: '/',
                templateUrl: '/App/templates/subsection2/subsection2-page.html',
                controller: 'Subsection2PageCtrl',
                parent: 'subsection2'
            });

        $stateProvider.debug = true;
    }

    routes.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider'];

    angular
        .module('app')
        .config(routes);
})();

