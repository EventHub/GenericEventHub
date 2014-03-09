'use strict'

# Declare app level module which depends on filters, and services
App = angular.module('app', [
  'ngCookies'
  'ngResource'
  'ngRoute'
  'app.controllers'
  'app.directives'
  'app.services'
  'app.filters'
  'partials'
  'restangular'
  'ui.bootstrap'
])

App.config([
  '$routeProvider'
  '$locationProvider'
  'RestangularProvider'
  '$httpProvider'

($routeProvider, $locationProvider, RestangularProvider, $httpProvider, config) ->

  RestangularProvider.setBaseUrl("/api")

  $routeProvider

    .when('/Dashboard', {
      templateUrl: '/partials/Dashboard.html',
      controller: 'DashboardCtrl'
    })
    .when('/Dashboard/Events/:eventID', {
      templateUrl: '/partials/Event.html',
      controller: 'EventCtrl'
    })
    .when('/Profile', {
      templateUrl: '/partials/User.html',
      controller: 'UserCtrl'
    })
    .when('/Admin', {
      templateUrl: '/partials/AdminDashboard.html',
      controller: 'AdminDashboardCtrl'
    })
    # Catch all
    .otherwise({redirectTo: '/Dashboard'})

  # Without server side support html5 must be disabled.
  $locationProvider.html5Mode(false)
  
  $httpProvider.responseInterceptors.push 'myHttpInterceptor'
  spinnerFunction = (data, headers) ->
    $('#loading').show()
    data
  $httpProvider.defaults.transformRequest.push(spinnerFunction)
])
