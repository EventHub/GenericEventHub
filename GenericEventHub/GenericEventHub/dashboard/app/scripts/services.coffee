'use strict'

### Sevices ###

angular.module('app.services', [])

.factory('myHttpInterceptor',
	['$q', '$window', ($q, $window) ->
		return (promise) ->
			return promise.then((response) ->
				$('#loading').hide()
				return response
			,
			(response) ->
				$('#loading').hide()
				return $q.reject(response)
			)
	])