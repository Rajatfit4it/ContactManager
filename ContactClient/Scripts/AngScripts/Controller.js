/// <reference path="../angular.js" /> 
/// <reference path="../angular.min.js" /> 
/// <reference path="Modules.js" />  

app.controller("AngularJs_ContactController", function ($scope, $timeout, $rootScope, $window, $http) {

    $scope.date = new Date();
    
    $scope.PrepareEdit = function (item) {

    }

    $http.get('/api/ContactSPA/').success(function (data) {

        $scope.Contacts = data;

    })

});