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


.factory('Participants', ['$rootScope', 'Restangular', ($rootScope, Restangular) ->
	Participants = this
	Participants.all = []
	# This needs to be set
	Participants.eventID = 0
	eventRoute = {}

	proxy = $.connection.participantsHub

	proxy.client.addUser = (name, id, eventID) ->
		if eventID.toString() != Participants.eventID.toString()
			console.log "ids dont match"
			return
		createAndAddUser(name, id)
		$rootScope.$apply()
		return
	proxy.client.removeUser = (name, id, eventID) ->
	  	if eventID.toString() != Participants.eventID.toString()
	    	console.log "ids dont match"
	    	return
	  	removeParticipant(id)
	  	$rootScope.$apply()
	  	return
	proxy.client.addGuest = (guestName, guestId, userName, userId, eventID) ->
	  	if eventID.toString() != Participants.eventID.toString()
	    	console.log "ids dont match"
	    	return
	  	createAndAddGuest(guestName, guestId, userName, userId)
	  	$rootScope.$apply()
	  	return
	proxy.client.removeGuest = (guestName, guestId, userName, userId, eventID) ->
		if eventID.toString() != Participants.eventID.toString()
			console.log "ids dont match"
			return
		removeParticipant(guestId)
		$rootScope.$apply()
		return

	createAndAddUser = (name, id) ->
		userObj = 
			name: name
			type: 'user'
			id: id
		console.log userObj
		Participants.all.push(userObj)
		return
	createAndAddGuest = (guestName, guestId, userName, userId) ->
        guestObj = 
        	name: guestName + " (" + userName + ")"
        	type: 'guest'
        	id: guestId
        	host: userId
        Participants.all.push(guestObj)
        return
    removeParticipant = (id) ->
        for attendee, key in Participants.all
            console.log attendee
            console.log attendee['id'] == id
            if attendee['id'].toString() is id.toString()
            	Participants.all.splice(key, 1)

	Participants.addUser = ->
		eventRoute.post('AddUser')

	Participants.removeUser = ->
		eventRoute.post('RemoveUser')

	Participants.addGuest = (guestName) ->
		guest = 
			Name: guestName
			EventID: Participants.eventID
		eventRoute.post('AddGuest', guest)

	Participants.removeGuest = (guest) ->
		eventRoute.post('RemoveGuest/' + guest.id)

	Participants.start = ->
		$.connection.hub.start()
	    .done(() ->
	    	console.log 'connected!'
	    )
	    .fail(() ->
	    	console.log 'failed to connect!'
		)
		Participants.all = []
		eventRoute = Restangular.one('Events', Participants.eventID)
		eventRoute.get().then((data) ->
			for user in data.UsersInEvent
				createAndAddUser(user.Name, user.UserID)
			for guest in data.GuestsInEvent
				createAndAddGuest(guest.Name, guest.GuestID, guest.Host.Name, guest.Host.UserID)
		)

	return Participants
])