(function () {
    'use strict';

    function Article($resource, Config) {
        return $resource(Config.articleEndpoint);
    }

    Article.$inject = ['$resource', 'Config'];

    angular
        .module('app')
        .factory('Article', Article);
})();