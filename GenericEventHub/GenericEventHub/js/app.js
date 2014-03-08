'use strict';
var App;

App = angular.module('app', ['ngCookies', 'ngResource', 'ngRoute', 'app.controllers', 'app.directives', 'app.filters', 'app.services', 'partials', 'restangular', 'ui.bootstrap']);

App.config([
  '$routeProvider', '$locationProvider', 'RestangularProvider', function($routeProvider, $locationProvider, RestangularProvider, config) {
    RestangularProvider.setBaseUrl("/api");
    $routeProvider.when('/Dashboard', {
      templateUrl: '/partials/Dashboard.html',
      controller: 'DashboardCtrl'
    }).when('/Dashboard/Events/:eventID', {
      templateUrl: '/partials/Event.html',
      controller: 'EventCtrl'
    }).when('/Settings', {
      templateUrl: '/partials/User.html',
      controller: 'UserCtrl'
    }).when('/Admin', {
      templateUrl: '/partials/AdminDashboard.html',
      controller: 'AdminDashboardCtrl'
    }).otherwise({
      redirectTo: '/Dashboard'
    });
    return $locationProvider.html5Mode(false);
  }
]);
;'use strict';
/* Controllers*/

angular.module('app.controllers', []).controller('NavCtrl', [
  '$scope', '$location', '$resource', 'Restangular', function($scope, $location, $resource, Restangular) {
    $scope.$location = $location;
    $scope.$watch('$location.path()', function(path) {
      return $scope.activeNavId = path || '/';
    });
    $scope.getClass = function(id) {
      if ($scope.activeNavId.substring(0, id.length) === id) {
        return 'active';
      } else {
        return '';
      }
    };
    return $scope.user = Restangular.one('Users', 'Current').get().$object;
  }
]).controller('DashboardCtrl', [
  '$scope', 'Restangular', '$filter', function($scope, Restangular, $filter) {
    var today;
    $scope.loading = true;
    $scope.events = [];
    today = $filter('date')(new Date(), 'MM-dd-yyyy');
    return Restangular.one('Events', today).getList().then(function(data) {
      var loading;
      $scope.events = data;
      return loading = false;
    }, function(response) {
      var loading;
      return loading = false;
    });
  }
]).controller('AdminDashboardCtrl', [
  '$scope', function($scope) {
    return $scope.events = [
      {
        title: "One",
        location: "Here"
      }, {
        title: "Two",
        location: "There"
      }
    ];
  }
]).controller('EventCtrl', [
  '$scope', '$routeParams', 'Restangular', '$modal', '$timeout', '$route', function($scope, $routeParams, Restangular, $modal, $timeout, $route) {
    var connection, eventID, eventRoute;
    eventID = $routeParams.eventID;
    $scope.attendees = [];
    $scope.user = Restangular.one('Users', 'Current').get().$object;
    eventRoute = Restangular.one('Events', eventID);
    eventRoute.get().then(function(data) {
      var guest, guestObj, hostName, user, userObj, _i, _j, _len, _len1, _ref, _ref1, _results;
      $scope.event = data;
      $scope.locationLink = "http://maps.google.com?q=" + $scope.event.Activity.Location.Name.split(' ').join('+');
      if ($scope.event.UsersInEvent.length + $scope.event.GuestsInEvent.length === 0) {
        return $scope.attendees.push({
          name: "No one :(",
          id: -1
        });
      } else {
        _ref = $scope.event.UsersInEvent;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          user = _ref[_i];
          userObj = {};
          userObj['name'] = user.Name;
          userObj['type'] = 'user';
          userObj['id'] = user.UserID;
          $scope.attendees.push(userObj);
        }
        _ref1 = $scope.event.GuestsInEvent;
        _results = [];
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
          guest = _ref1[_j];
          guestObj = {};
          hostName = '?';
          if (guest.Host != null) {
            hostName = guest.Host.Name;
          }
          guestObj['name'] = guest.Name + " (" + hostName + ")";
          guestObj['type'] = 'guest';
          guestObj['id'] = guest.GuestID;
          _results.push($scope.attendees.push(guestObj));
        }
        return _results;
      }
    });
    connection = $.connection.ParticipantsHub;
    console.log(connection);
    $scope.addUser = function() {
      eventRoute.post('AddUser');
      return $timeout($route.reload(), 2000);
    };
    $scope.removeUser = function() {
      eventRoute.post('RemoveUser');
      return $timeout($route.reload(), 2000);
    };
    return $scope.addGuest = function() {
      var modalInstance;
      return modalInstance = $modal.open({
        templateUrl: 'guestModal.html',
        controller: 'GuestModalCtrl',
        resolve: {
          eventID: function() {
            return eventID;
          }
        }
      });
    };
  }
]).controller('GuestModalCtrl', [
  '$scope', '$modalInstance', 'Restangular', 'eventID', '$timeout', '$route', function($scope, $modalInstance, Restangular, eventID, $timeout, $route) {
    var close;
    $scope.add = function(guestName) {
      var guest;
      guest = {
        Name: guestName,
        EventID: eventID
      };
      Restangular.one('Events', eventID).post('AddGuest', guest);
      $timeout($route.reload(), 2000);
      return close();
    };
    $scope.cancel = function() {
      return close();
    };
    return close = function() {
      return $modalInstance.dismiss('cancel');
    };
  }
]).controller('UserCtrl', [
  '$scope', 'Restangular', function($scope, Restangular) {
    $scope.Name = "";
    Restangular.one('Users', 'Current').get().then(function(data) {
      $scope.user = data;
      return $scope.Name = $scope.user.Name;
    });
    return $scope.update = function(name) {
      $scope.user.Name = name;
      return Restangular.one('Users', 'Name').post({
        'id': $scope.user.UserID,
        'name': name
      });
    };
  }
]);
;'use strict';
/* Directives*/

angular.module('app.directives', ['app.services']).directive('appVersion', [
  'version', function(version) {
    return function(scope, elm, attrs) {
      return elm.text(version);
    };
  }
]);
;'use strict';
/* Filters*/

angular.module('app.filters', []).filter('interpolate', [
  'version', function(version) {
    return function(text) {
      return String(text).replace(/\%VERSION\%/mg, version);
    };
  }
]);
;'use strict';
/* Sevices*/

angular.module('app.services', []).factory('version', function() {
  return "0.1";
});
;
//# sourceMappingURL=app.js.map