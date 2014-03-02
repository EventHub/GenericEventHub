'use strict'

# Declare app level module which depends on filters, and services
App = angular.module('app', [
  'ngCookies'
  'ngResource'
  'ngRoute'
  'app.controllers'
  'app.directives'
  'app.filters'
  'app.services'
  'partials'
  'restangular'
])

App.config([
  '$routeProvider'
  '$locationProvider'
  'RestangularProvider'

($routeProvider, $locationProvider, RestangularProvider, config) ->

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
    .when('/Admin', {
      templateUrl: '/partials/AdminDashboard.html',
      controller: 'AdminDashboardCtrl'
    })
    # Catch all
    .otherwise({redirectTo: '/Dashboard'})

  # Without server side support html5 must be disabled.
  $locationProvider.html5Mode(false)
])
