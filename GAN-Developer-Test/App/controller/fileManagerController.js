(function () {

    'use strict';
    angular.module('GANAppControllers').controller('fileManagerController', ['$scope', '$http', function ($scope, $http) {

        $scope.filesList = [];
        $scope.getAll = function () {
            $scope.filesList = [];
            $http({ method: 'GET', url: 'api/file/getAll' }).success(function (response, status) {
                $scope.filesList = response;
            });
        };
        $scope.getAll();

        $scope.group = 1;
        $scope.postModel = {};
        $scope.save = function () {
            if ($scope.populatePostModel()) {
                $http({ method: 'POST', url: 'api/file/upload', data: $scope.postModel }).success(function (response, status) {
                    if (response) {
                        console.log('inside response ' + response);
                        noty({ timeout: 3500, layout: 'topCenter', text: $scope.file.filename + ' uploaded successfully', type: 'success' });
                        $scope.resetPostModel();
                        $scope.getAll();
                    }
                });
            }
        };

        $scope.populatePostModel = function () {
            if (!$scope.file || !$scope.file.filename || !$scope.file.base64) {
                noty({ timeout: 3500, layout: 'topCenter', text: 'Please upload a file first', type: 'warning' });
                return false;
            }
            $scope.postModel.group = $scope.group;
            $scope.postModel.contentType = $scope.file.filetype;
            $scope.postModel.fileBase64String = $scope.file.base64;
            $scope.postModel.fileName = $scope.file.filename;
            return true;
        };
        $scope.resetPostModel = function () {
            $scope.file = {};
            $scope.postModel.group = 1;
            $scope.postModel.contentType = '';
            $scope.postModel.fileBase64String = '';
            $scope.postModel.fileName = '';
        };

        //mark end of controller
    }]);
}());