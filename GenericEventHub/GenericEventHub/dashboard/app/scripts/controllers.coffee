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
  '$scope', '$routeParams', 'Restangular', '$modal', '$route', '$timeout', 'Participants'

($scope, $routeParams, Restangular, $modal, $route, $timeout, Participants) ->
  $scope.eventID = $routeParams.eventID.trim()
  
  Participants.eventID = $routeParams.eventID
  Participants.start()

  $scope.attendees = []
  $scope.attendees = Participants.all

  # proxy = $.connection.participantsHub
  # proxy.client.addUser = (name, id, eventID) ->
  #   if eventID.toString() != $scope.eventID.toString()
  #     console.log "ids dont match"
  #     return
  #   $scope.createAndAddUser(name, id)
  # proxy.client.removeUser = (name, id, eventID) ->
  #   if eventID.toString() != $scope.eventID.toString()
  #     console.log "ids dont match"
  #     return
  #   $scope.removeParticipant(id)
  # proxy.client.addGuest = (guestName, guestId, userName, userId, eventID) ->
  #   if eventID.toString() != $scope.eventID.toString()
  #     console.log "ids dont match"
  #     return
  #   $scope.createAndAddGuest(guestName, guestId, userName, userId)
  # proxy.client.removeGuest = (guestName, guestId, userName, userId, eventID) ->
  #   if eventID.toString() != $scope.eventID.toString()
  #     console.log "ids dont match"
  #     return
  #   $scope.removeParticipant(guestId)
  # $.connection.hub.start()
  #   .done(() ->
  #     console.log 'connected!'
  #     )
  #   .fail(() ->
  #     console.log 'failed to connect!'
  #     )


  # $scope.attendees = []  
  # $scope.createAndAddUser = (name, id) ->
  #   console.log(name + " " + id)
  #   userObj = {}
  #   userObj['name'] = name
  #   userObj['type'] = 'user'
  #   userObj['id'] = id
  #   $timeout((() -> $scope.attendees.push(userObj)), 50)
  # $scope.createAndAddGuest = (guestName, guestId, userName, userId) ->
  #   guestObj = {}
  #   guestObj['name'] = guestName + " (" + userName + ")"
  #   guestObj['type'] = 'guest'
  #   guestObj['id'] = guestId
  #   guestObj['host'] = userId
  #   $timeout((() -> $scope.attendees.push(guestObj)), 50)

  # $scope.removeParticipant = (id) ->
  #   console.log "removing"
  #   console.log id
  #   console.log $scope.attendees
  #   for attendee, key in $scope.attendees
  #     console.log attendee
  #     console.log attendee['id'] == id
  #     if attendee['id'].toString() is id.toString()
  #       $timeout((() -> $scope.attendees.splice(key, 1)), 1000)
  #       break

  $scope.user = Restangular.one('Users', 'Current').get().$object;
  eventRoute = Restangular.one('Events', $scope.eventID)
  eventRoute.get().then((data) ->
    $scope.event = data
    $scope.locationLink = "http://maps.google.com?q=" + $scope.event.Activity.Location.Name.split(' ').join('+')
    return
  )

  $scope.addUser = ->
    Participants.addUser()

  $scope.removeUser = -> 
    Participants.removeUser()

  $scope.addGuest = ->
    modalInstance = $modal.open(
      templateUrl: 'guestModal.html'
      controller: 'GuestModalCtrl',
      resolve:
        add: ->
          Participants.addGuest
    )

  $scope.removeGuest = (guest) ->
    Participants.removeGuest(guest)
])

.controller('GuestModalCtrl', ['$scope','$modalInstance', 'Restangular', 'add', '$timeout', '$route'
  ($scope, $modalInstance, Restangular, add, $timeout, $route) ->
    $scope.add = (guestName) ->
      add(guestName)
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
      $scope.Email = $scope.user.Email
    )

    $scope.update = (name, email) ->
      Restangular.one('Users').post('Current', {'UserID': $scope.user.UserID, 'Name': name, 'Email', email})
])