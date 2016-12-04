//'use strict'

angular.
    module('compressorApp').
    config(['$locationProvider', '$routeProvider', function config($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider.            
            when('/compress', {
                template: '<url-compress></url-compress>'
            }).
            when('/urls', {
                template: '<url-list></url-list>'
            }).
                otherwise('/compress');
    }
    ]);