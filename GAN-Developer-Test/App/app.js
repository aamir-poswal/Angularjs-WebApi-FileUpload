(function () {
    'use strict';

    /* App Module */

    var GANApp = angular.module('GANApp', [
     , 'ngRoute'
     , 'ngResource'
     , 'ngSanitize'
     , 'GANAppControllers'
    , 'naif.base64'
    ]);

    angular.module('GANAppControllers', []);

    GANApp.config(['$routeProvider', '$httpProvider', '$locationProvider',
            function ($routeProvider, $httpProvider, $locationProvider) {

                $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

                $routeProvider.
               when('/', {
                   templateUrl: 'app/view/fileManager.html',
                   controller: 'fileManagerController'
               })
                .otherwise({
                    redirectTo: '/'
                });

            }]);


}());