'use strict';

var NsCommerceApp = angular.module('NsCommerceApp', ['ngRoute','toaster', 'ngAnimate', 'ngFileUpload', 'controllers'])

.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $routeProvider
          .when('/', {
              templateUrl: '/SPA/Home/Index.html',
              controller: 'ProductController'
          })
      .when('/product', {
          templateUrl: '/SPA/Product/Index.html',
          controller: 'ProductController' 
      })
     .when('/product/list', {
         templateUrl: '/SPA/Product/List.html',
         controller: 'ProductController'
     })

     .when('/product/create', {
         templateUrl: '/SPA/Product/Create.html',
         controller: 'ProductController'
     })
    .when('/product/update/:id', {
        templateUrl: '/SPA/Product/Update.html',
        controller: 'ProductController'
    })    
      .otherwise({
         redirect: '/'
     });
    
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });

}])

.factory("AuthService", ['$http', '$window', function ($http, $window) {
   
    var Service = {};
    Service.AuthStatus = true;//validate with web API

    if (Service.AuthStatus==false) {
           $window.location.href = '/Account';
    }

    return Service;
   

}]);





