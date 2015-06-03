(function () {
    'use strict';

    function Site($resource, Config) {
        return $resource(Config.siteEndpoint);
    }

    Site.$inject = ['$resource', 'Config'];

    angular
        .module('app')
        .factory('Site', Site);
})();