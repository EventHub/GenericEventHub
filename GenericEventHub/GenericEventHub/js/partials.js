angular.module('partials', [])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/Dashboard.html', [
'',
'<div ng-repeat="event in events" ng-show="events" class="panel panel-default">',
'  <div class="panel-heading">',
'    <div class="panel-title"><a href="#/Dashboard/Events/{{event.EventID}}" ng-bind="event.Name"></a></div>',
'  </div>',
'  <div class="panel-body">',
'    <div><span>When: &nbsp;</span><span ng-bind="event.DateTime | date:\'EEEE, MMMM d, yyyy\'">Time</span></div>',
'    <div><span>Time: &nbsp;</span><span ng-bind="event.DateTime | date:\'h:mm a\'"></span></div>',
'    <div><span>Where:&nbsp;</span><span ng-bind="event.Activity.Location.Name">Name</span></div>',
'    <div><span>Address:&nbsp;</span><span ng-bind="event.Activity.Location.Address">Address</span></div>',
'    <div><span># Attendees:&nbsp;</span><span ng-bind="event.UsersInEvent.length + event.GuestsInEvent.length"></span></div>',
'  </div>',
'</div>',
'<div ng-if="events.length == 0" class="panel panel-default">',
'  <div class="panel-body">',
'    <div>Currently, there are no planned events. Check back later!</div>',
'  </div>',
'</div>',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/AdminDashboard.html', [
'',
'<div class="container-fluid">',
'  <div class="row">',
'    <div class="col-sm-3 col-md-2 sidebar">',
'      <ul class="nav nav-sidebar">',
'        <li><a href="#/Admin">Metrics</a></li>',
'        <li><a href="#/Admin">Activities</a></li>',
'        <li> <a href="#/Admin">Locations</a></li>',
'      </ul>',
'    </div>',
'    <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main"> ',
'      <h1 class="page-header">Dashboard</h1>',
'    </div>',
'  </div>',
'</div>',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/Event.html', [
'',
'<div style="margin-top: 0px;" class="page-header">',
'  <h3 style="margin: 0px;" ng-bind="event.Activity.Name">Name</h3>',
'</div>',
'<div><span>When: &nbsp;</span><span ng-bind="event.DateTime | date:\'EEEE, MMMM d, yyyy\'">Time</span></div>',
'<div><span>Time: &nbsp;</span><span ng-bind="event.DateTime | date:\'h:mm a\'"></span></div>',
'<div><span>Where:&nbsp;</span><a href="{{locationLink}}" target="_blank" ng-bind="event.Activity.Location.Name">Name</a></div>',
'<div><span>Address:&nbsp;</span><span ng-bind="event.Activity.Location.Address">Address</span></div>',
'<div><span># Attendees:&nbsp;</span><span ng-bind="event.UsersInEvent.length + event.GuestsInEvent.length"></span></div><br>',
'<button type="button" ng-click="addUser()" class="btn btn-primary">Join</button>',
'<button type="button" ng-click="removeUser()" class="btn btn-primary">Leave</button>',
'<button type="button" ng-click="addGuest()" class="btn btn-primary">+1</button><br><br>',
'<div class="panel panel-default">',
'  <table class="table">',
'    <thead>',
'      <tr class="success">',
'        <th>Attending</th>',
'      </tr>',
'    </thead>',
'    <tbody>',
'      <tr ng-repeat="attendee in attendees">',
'        <td ng-bind="attendee.name"></td>',
'      </tr>',
'    </tbody>',
'  </table>',
'</div>',
'<script id="guestModal.html" type="text/ng-template">',
'  <div class="modal-header">',
'    <h3>Add a guest</h3>',
'  </div>',
'  <div class="modal-body">',
'    <form>',
'      <input id="guestNameInput" placeholder="Enter your guest\'s name" ng-model="guestName" class="form-control">',
'    </form>',
'  </div>',
'  <div class="modal-footer">',
'    <button ng-click="add(guestName)" class="btn btn-primary">Add</button>',
'    <button ng-click="cancel()" class="btn btn-warning">Cancel</button>',
'  </div>',
'</script>',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/partial1.html', [
'',
'<p>This is the partial for view 1.</p>',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/nav.html', [
'<!--ul.nav',
'<li ng-class="getClass(\'/todo\')"><a ng-href="#/todo">todo</a></li>',
'<li ng-class="getClass(\'/view1\')"><a ng-href="#/view1">view1</a></li>',
'<li ng-class="getClass(\'/view2\')"><a ng-href="#/view2">view2</a></li>-->',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/User.html', [
'',
'<form class="form-inline">',
'  <div class="form-group">',
'    <label>Display name: </label>',
'    <input placeholder="Enter your display name" ng-model="Name" class="form-control">',
'  </div>',
'</form><br>',
'<button ng-click="update(Name)" class="btn btn-success">Update my info</button>',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/partial2.html', [
'',
'<p>This is the partial for view 2.</p>',
'<p>',
'  Showing of \'interpolate\' filter:',
'  {{ \'Current version is v%VERSION%.\' | interpolate }}',
'</p>',''].join("\n"));
}])
.run(['$templateCache', function($templateCache) {
  return $templateCache.put('/partials/todo.html', [
'',
'<div ng-app="ng-app">',
'  <h2>Todo</h2>',
'  <div ng-controller="TodoCtrl"><span>{{remaining()}} of {{todos.length}} remaining</span> [<a href="" ng-click="archive()">archive</a>]',
'    <ul class="unstyled">',
'      <li ng-repeat="todo in todos">',
'        <label class="checkbox inline">',
'          <input type="checkbox" ng-model="todo.done"><span class="done{{todo.done}}">{{todo.text}}</span>',
'        </label>',
'      </li>',
'    </ul>',
'    <form ng-submit="addTodo()" class="form-inline">',
'      <p>',
'        <input type="text" ng-model="todoText" size="30" placeholder="add new todo here">',
'        <input type="submit" value="add" class="btn btn-primary">',
'      </p>',
'    </form>',
'  </div>',
'</div>',''].join("\n"));
}]);