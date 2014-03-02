'use strict'

### Controllers ###

angular.module('app.controllers', [])

.controller('NavCtrl', [
  '$scope'
  '$location'
  '$resource'
  'Restangular'

($scope, $location, $resource, Restangular) ->

  # Uses the url to determine if the selected
  # menu item should have the class active.
  $scope.$location = $location
  $scope.$watch('$location.path()', (path) ->
    $scope.activeNavId = path || '/'
  )

  # getClass compares the current url with the id.
  # If the current url starts with the id it returns 'active'
  # otherwise it will return '' an empty string. E.g.
  #
  #   # current url = '/products/1'
  #   getClass('/products') # returns 'active'
  #   getClass('/orders') # returns ''
  #
  $scope.getClass = (id) ->
    if $scope.activeNavId.substring(0, id.length) == id
      return 'active'
    else
      return ''

  $scope.user = Restangular.one('Users', 'Current').get().$object;
])

.controller('DashboardCtrl', [
  '$scope'
  'Restangular'

($scope, Restangular) ->
  $scope.events = Restangular.all('Events').getList().$object;
])

.controller('AdminDashboardCtrl', [
  '$scope'

($scope) ->
  $scope.events = [
    title: "One"
    location: "Here"
  ,
    title: "Two"
    location: "There"
  ]
])

.controller('EventCtrl', [
  '$scope', '$routeParams', 'Restangular'

($scope, $routeParams, Restangular) ->
  eventID = $routeParams.eventID
  console.log(eventID)

  $scope.event = Restangular.one('Events', eventID).get().$object
])