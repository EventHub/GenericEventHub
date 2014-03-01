using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using GenericEventHub.Repositories;
using GenericEventHub.Models;

namespace EventGenerator.Tests
{
    [TestClass]
    public class EventGeneratorTests
    {
        private EventGenerator _sut;
        private Mock<IActivityRepository> _activtyRepo;
        private Mock<IEventRepository> _eventRepo;

        [TestInitialize]
        public void SetUp()
        {
            _activtyRepo = new Mock<IActivityRepository>();
            _eventRepo = new Mock<IEventRepository>();

            _sut = new EventGenerator(_eventRepo.Object, _activtyRepo.Object);
        }

        #region DatesInRange Tests
        [TestMethod]
        public void DatesInRangeTest()
        {
            // Arrange
            var numOfDays = 12;
            var start = DateTime.Today;
            var end = start.AddDays(numOfDays);

            // Act
            List<DateTime> dates = new List<DateTime>(_sut.DatesInRange(start, end));

            // Assert 
            Assert.AreEqual(numOfDays + 1, dates.Count);
        }

        [TestMethod]
        public void DatesInRangeTest_2()
        {
            // Arrange
            var start = DateTime.Today;
            var end = start.AddDays(-1);

            // Act
            List<DateTime> dates = new List<DateTime>(_sut.DatesInRange(start, end));

            // Assert 
            Assert.AreEqual(0, dates.Count);
        }

        [TestMethod]
        public void DatesInRangeTest_3()
        {
            // Arrange
            var numOfDays = 0;
            var start = DateTime.Today;
            var end = start.AddDays(numOfDays);

            // Act
            List<DateTime> dates = new List<DateTime>(_sut.DatesInRange(start, end));

            // Assert 
            Assert.AreEqual(numOfDays + 1, dates.Count);
        }
        #endregion

        #region DoesEventExist Tests
        [TestMethod]
        public void DoesEventExistTrueTest()
        {
            var selectedDate = DateTime.Today;

            // Arrange
            var activity = new Activity {
                Name = "Test",
            };

            var activity2 = new Activity {
                Name = "Test2",
            };

            var ev1 = new Event {
                Name = activity.Name,
                Activity = activity,
                DateTime = selectedDate
            };

            var ev2 = new Event {
                Name = activity.Name,
                Activity = activity2,
                DateTime = selectedDate
            };

            IEnumerable<Event> events = new List<Event> {
                ev1, ev2
            };

            _eventRepo.Setup(x => x.GetEventsOn(selectedDate)).Returns(events);

            // Act
            var does = _sut.DoesEventExistForActivityAndDate(activity, selectedDate);

            // Assert
            Assert.IsTrue(does);
        }

        [TestMethod]
        public void DoesEventExistFalseTest()
        {
            var selectedDate = DateTime.Today;

            // Arrange
            var activity = new Activity
            {
                Name = "Test",
                ActivityID = 1
            };

            var activity2 = new Activity
            {
                Name = "Test2",
                ActivityID = 2
            };

            var activity3 = new Activity
            {
                Name = "Test3",
                ActivityID = 3
            };

            var ev1 = new Event
            {
                Activity = activity
            };

            var ev2 = new Event
            {
                Activity = activity2
            };

            IEnumerable<Event> events = new List<Event> {
                ev1, ev2
            };

            _eventRepo.Setup(x => x.GetEventsOn(selectedDate)).Returns(events);

            // Act
            var does = _sut.DoesEventExistForActivityAndDate(activity3, selectedDate);

            // Assert
            Assert.IsFalse(does);
        }
        #endregion

        #region GenerateEventsForActivitiesAndDateRange Tests

        [TestMethod]
        public void GenerateEventsForActivitiesAndDateRange_NoDates_Test()
        {
            // Arrange
            var start = DateTime.Today;
            var end = start.AddDays(-1);

            var events = _sut.GenerateEventsForActivitiesAndDateRange(new List<Activity>(),
                start, end);

            Assert.AreEqual(0, events.Count);
        }

        [TestMethod]
        public void GenerateEventsForActivitiesAndDateRange_NoActivities_Test()
        {
            // Arrange
            var start = DateTime.Today;
            var end = start.AddDays(7);

            // Act
            var events = _sut.GenerateEventsForActivitiesAndDateRange(new List<Activity>(),
                start, end);

            // Assert
            Assert.AreEqual(0, events.Count);
        }

        [TestMethod]
        public void GenerateEventsForActivitiesAndDateRange_Week_Test()
        {
            // Arrange
            _eventRepo.Setup(x => x.GetEventsOn(It.IsAny<DateTime>())).Returns(new List<Event>());
            var start = DateTime.Today;
            var end = start.AddDays(6);

            var basketball = new Activity {
                DayOfWeek = "Tuesday",
                Name = "Basketball"
            };

            var frisbee = new Activity {
                DayOfWeek = "Thursday",
                Name = "Frisbee"
            };

            var activites = new List<Activity> {
                basketball, frisbee
            };

            // Act
            var events = _sut.GenerateEventsForActivitiesAndDateRange(activites, start, end);

            // Assert
            Assert.AreEqual(2, events.Count);
        }

        [TestMethod]
        public void GenerateEventsForActivitiesAndDateRange_TwoWeek_Test()
        {
            // Arrange
            _eventRepo.Setup(x => x.GetEventsOn(It.IsAny<DateTime>())).Returns(new List<Event>());
            var start = DateTime.Today;
            var end = start.AddDays(13);

            var basketball = new Activity
            {
                DayOfWeek = "Tuesday",
                Name = "Basketball"
            };

            var frisbee = new Activity
            {
                DayOfWeek = "Thursday",
                Name = "Frisbee"
            };

            var activites = new List<Activity> {
                basketball, frisbee
            };

            // Act
            var events = _sut.GenerateEventsForActivitiesAndDateRange(activites, start, end);

            // Assert
            Assert.AreEqual(4, events.Count);
        }

        [TestMethod]
        public void GenerateEventsForActivitiesAndDateRange_SingleDay_Test()
        {
            // Arrange
            _eventRepo.Setup(x => x.GetEventsOn(It.IsAny<DateTime>())).Returns(new List<Event>());
            var start = new DateTime(2014, 2, 16);
            // start.DayOfWeek = Sunday
            var end = start;

            var basketball = new Activity
            {
                DayOfWeek = "Sunday",
                Name = "Basketball"
            };

            var frisbee = new Activity
            {
                DayOfWeek = "Sunday",
                Name = "Frisbee"
            };

            var activites = new List<Activity> {
                basketball, frisbee
            };

            // Act
            var events = _sut.GenerateEventsForActivitiesAndDateRange(activites, start, end);

            // Assert
            Assert.AreEqual(2, events.Count);
        }

        [TestMethod]
        public void GenerateEventsForActivitiesAndDateRange_SingleDayAndEventExists_Test()
        {
            // Arrange
            var start = new DateTime(2014, 2, 16);
            // start.DayOfWeek = Sunday
            var end = start;

            var basketball = new Activity
            {
                DayOfWeek = "Sunday",
                Name = "Basketball",
                ActivityID = 1
            };

            var frisbee = new Activity
            {
                DayOfWeek = "Sunday",
                Name = "Frisbee",
                ActivityID = 2
            };

            var existingEvents = new List<Event> {
                new Event {
                    Activity = frisbee,
                    ActivityID = frisbee.ActivityID
                }
            };

            _eventRepo.Setup(x => x.GetEventsOn(It.IsAny<DateTime>())).Returns(existingEvents);

            var activites = new List<Activity> {
                basketball, frisbee
            };

            // Act
            var events = _sut.GenerateEventsForActivitiesAndDateRange(activites, start, end);

            // Assert
            Assert.AreEqual(1, events.Count);
        }

        #endregion

        [TestMethod]
        public void CreateEventsFor_Test()
        {
            // Arrange
            _eventRepo.Setup(x => x.GetEventsOn(It.IsAny<DateTime>())).Returns(new List<Event>());
            var start = new DateTime(2014, 2, 16);
            // start.DayOfWeek = Sunday
            var end = start.AddDays(6);

            var basketball = new Activity
            {
                DayOfWeek = "Saturday",
                Name = "Basketball"
            };

            var frisbee = new Activity
            {
                DayOfWeek = "Sunday",
                Name = "Frisbee"
            };

            var activities = new List<Activity> {
                basketball, frisbee
            };

            var count = 0;
            _eventRepo.Setup(x => x.Insert(It.IsAny<Event>())).Callback((Event ev) => { count++; });
            _activtyRepo.Setup(x => x.GetAll()).Returns(activities);

            // Act
            _sut.CreateEventsFor(start, end);

            // Assert
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void CreateEventsForNextWeek_Test()
        {
            // Arrange
            _eventRepo.Setup(x => x.GetEventsOn(It.IsAny<DateTime>())).Returns(new List<Event>());

            var basketball = new Activity
            {
                DayOfWeek = "Saturday",
                Name = "Basketball"
            };

            var frisbee = new Activity
            {
                DayOfWeek = "Sunday",
                Name = "Frisbee"
            };

            var activities = new List<Activity> {
                basketball, frisbee
            };

            var count = 0;
            _eventRepo.Setup(x => x.Insert(It.IsAny<Event>())).Callback((Event ev) => { count++; });
            _activtyRepo.Setup(x => x.GetAll()).Returns(activities);

            // Act
            _sut.CreateEventsForNextWeek();

            // Assert
            Assert.AreEqual(2, count);
        }
    }
}
