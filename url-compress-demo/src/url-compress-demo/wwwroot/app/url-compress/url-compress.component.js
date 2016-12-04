'use strict'

angular.
    module('urlCompress').
        component('urlCompress', {
            templateUrl: 'app/url-compress/url-compress.template.html',
            controller: ['$http',
                function UrlCompressController($http) {
                    var self = this;
                    this.success = true;
                    this.compress = function () {
                        $http.post('urlapi/compress?url=' + self.url).then(function (response) {
                            self.compressed = response.data;
                            self.error = undefined;
                        }, function (error) {
                            self.error = error.data;
                            self.url = undefined;
                        });
                    };
                }
            ]
        });