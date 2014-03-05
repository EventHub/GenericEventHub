using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericEventHub.Models;
using GenericEventHub.DTOs;
using System.Reflection;

namespace GenericEventHub.Tests
{
    // Test reflection of DTO class
    [TestClass]
    public class DTOTests
    {
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

                Assert.IsNotNull(objValue);
                Assert.IsNotNull(dtoValue);
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
