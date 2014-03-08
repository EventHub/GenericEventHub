'use strict'

### Sevices ###

angular.module('app.services', [])

angular.module "loadingService", [], ($provide) ->
  $provide.factory "myHttpInterceptor", ($q, $window) ->
    (promise) ->
      promise.then ((response) ->
        $("#loading").hide()
        response
      ), (response) ->
        $("#loading").hide()
        $q.reject response


  return
