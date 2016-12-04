'use strict'

angular.
    module('urlList').
        component('urlList', {
            templateUrl: 'app/url-list/url-list.template.html',
            controller: ['$http',
                function UrlListController($http) {
                    var self = this;
                    $http.get('urlapi/all').then(function (response) {
                        self.urls = response.data;
                    });
                }
            ]
        });