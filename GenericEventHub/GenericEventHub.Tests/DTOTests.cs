using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericEventHub.Models;
using GenericEventHub.DTOs;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace GenericEventHub.Tests
{
    // Test reflection of DTO class
    [TestClass]
    public class DTOTests
    {
        private DTOMapper mapper;

        [TestInitialize]
        public void Setup()
        {
            mapper = new DTOMapper();
        }

        [TestMethod]
        public void DTOMapperEntityToDTOTest()
        {
            Location location = new Location()
            {
                Name = "asdf",
                Address = "zxvc",
                LocationID = 1
            };

            var dto = mapper.GetDTOForEntity<Location, LocationDTO>(location);

            AssertLocationDTO(location, dto);
        }

        [TestMethod]
        public void DTOMapperEntitiesToDTOTest()
        {
            Location location1 = new Location()
            {
                Name = "asdf",
                Address = "zxvc",
                LocationID = 1
            };

            Location location2 = new Location()
            {
                Name = "asdf",
                Address = "zxvc",
                LocationID = 2
            };

            IEnumerable<Location> locations = new List<Location>() {
                location1, location2
            };

            var dtos = mapper.GetDTOsForEntities<Location, LocationDTO>(locations);

            AssertLocationDTO(location1, dtos.First());
            AssertLocationDTO(location2, dtos.Last());

        }

        [TestMethod]
        public void LocationDTOTest()
        {
            Location location = new Location()
            {
                Name = "asdf",
                Address = "zxvc",
                LocationID = 1
            };

            LocationDTO dto = new LocationDTO(location);

            AssertLocationDTO(location, dto);
        }

        private void AssertLocationDTO(Location location, LocationDTO dto)
        {
            AssertDTO(location, dto, new [] {
                "Name", "Address", "LocationID"
            });
        }

        [TestMethod]
        public void ActivityDTOTest()
        {
            Location location = new Location()
            {
                Name = "asdf",
                Address = "zxvc",
                LocationID = 1
            };

            Activity activity = new Activity()
            {
                ActivityID = 1,
                Name = "asdf",
                DayOfWeek = "Thursday",
                PreferredTime = new TimeSpan(1),
                Location = location
            };

            var dto = new ActivityDTO(activity);

            AssertActivityDTO(activity, dto);
        }

        [TestMethod]
        public void EventDTOTest()
        {
            Location location = new Location()
            {
                Name = "asdf",
                Address = "zxvc",
                LocationID = 1
            };

            Activity activity = new Activity()
            {
                ActivityID = 1,
                Name = "asdf",
                DayOfWeek = "Thursday",
                PreferredTime = new TimeSpan(1),
                Location = location
            };

            User user = new User()
            {
                Name = "kevin"
            };

            Guest guest = new Guest()
            {
                Name = "kevin",
                Host = user
            };

            Event ev = new Event()
            {
                Activity = activity,
                ActivityID = activity.ActivityID,
                UsersInEvent = new List<User>()
                {
                    user
                },
                GuestsInEvent = new List<Guest>()
                {
                    guest
                },
                DateTime = new DateTime(),
                EventID = 1,
                Name = "asdf"
            };

            var dto = new EventDTO(ev);

            AssertEventDTO(ev, dto);

            Assert.AreEqual(1, dto.UsersInEvent.Count());
            Assert.AreEqual(1, dto.GuestsInEvent.Count());
        }

        private void AssertEventDTO(Event ev, EventDTO dto)
        {
            AssertDTO(ev, dto, new[] { "DateTime", "EventID", "Name" });
            AssertActivityDTO(ev.Activity, new ActivityDTO(ev.Activity));
        }

        private void AssertActivityDTO(Activity activity, ActivityDTO dto)
        {
            AssertDTO(activity, dto, new[] { "ActivityID", "Name", "DayOfWeek", "PreferredTime" });
            AssertLocationDTO(activity.Location, new LocationDTO(activity.Location));
        }

        // hehe
        private void AssertDTO(Object obj, Object dto, string[] properties)
        {
            foreach (var property in properties)
            {
                var objValue = GetValue(obj, property);
                var dtoValue = GetValue(dto, property);

                Assert.IsNotNull(objValue, "obj " + property);
                Assert.IsNotNull(dtoValue, "dto " + property);
                Assert.AreEqual(objValue, dtoValue);
            }
        }

        private Object GetValue(Object obj, string property)
        {
            var objProperty = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            Object objValue = null;
            if (objProperty != null && objProperty.CanRead)
            {
                objValue = objProperty.GetValue(obj, null);
            }

            return objValue;
        }
    }
}
