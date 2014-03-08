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
  '$filter'

($scope, Restangular, $filter) ->
  $scope.loading = true
  today = $filter('date')(new Date(), 'MM-dd-yyyy')
  Restangular.one('Events', today).getList().then((data) ->
    $scope.events = data
    loading = false
  , (response) ->
    loading = false)
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
  '$scope', '$routeParams', 'Restangular', '$modal', '$timeout', '$route'

($scope, $routeParams, Restangular, $modal, $timeout, $route) ->
  eventID = $routeParams.eventID
  $scope.attendees = []
  $scope.user = Restangular.one('Users', 'Current').get().$object;
  eventRoute = Restangular.one('Events', eventID)
  eventRoute.get().then((data) ->
    $scope.event = data
    $scope.locationLink = "http://maps.google.com?q=" + $scope.event.Activity.Location.Name.split(' ').join('+')
    if ($scope.event.UsersInEvent.length + $scope.event.GuestsInEvent.length == 0)
      $scope.attendees.push({name:"No one :(", id: -1})
    else
      for user in $scope.event.UsersInEvent
        userObj = {}
        userObj['name'] = user.Name
        userObj['type'] = 'user'
        userObj['id'] = user.UserID
        $scope.attendees.push(userObj)
      for guest in $scope.event.GuestsInEvent
        guestObj = {}
        hostName = '?'
        if guest.Host?
          hostName = guest.Host.Name
        guestObj['name'] = guest.Name + " (" + hostName + ")"
        guestObj['type'] = 'guest'
        guestObj['id'] = guest.GuestID
        $scope.attendees.push(guestObj)
  )

  connection = $.connection.ParticipantsHub
  console.log connection

  $scope.addUser = ->
    eventRoute.post('AddUser')
    $timeout($route.reload(), 2000)

  $scope.removeUser = -> 
    eventRoute.post('RemoveUser')
    $timeout($route.reload(), 2000)

  $scope.addGuest = ->
    modalInstance = $modal.open(
      templateUrl: 'guestModal.html'
      controller: 'GuestModalCtrl',
      resolve:
        eventID: ->
          eventID
    )
])

.controller('GuestModalCtrl', ['$scope','$modalInstance', 'Restangular', 'eventID', '$timeout', '$route'
  ($scope, $modalInstance, Restangular, eventID, $timeout, $route) ->
    $scope.add = (guestName) ->
      guest = 
        Name: guestName
        EventID: eventID
      Restangular.one('Events',eventID).post('AddGuest', guest)
      $timeout($route.reload(), 2000)
      close()
    $scope.cancel = ->
      close()

    close = ->
      $modalInstance.dismiss('cancel')
])

.controller('UserCtrl', ['$scope', 'Restangular'
  ($scope, Restangular) ->
    $scope.Name = ""
    Restangular.one('Users', 'Current').get().then((data) ->
      $scope.user = data
      $scope.Name = $scope.user.Name
    )

    $scope.update = (name) ->
      $scope.user.Name = name
      Restangular.one('Users', 'Name').post({'id': $scope.user.UserID, 'name': name})
])