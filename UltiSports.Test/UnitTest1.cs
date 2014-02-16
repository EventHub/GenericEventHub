using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UltiSports.Infrastructure;
using UltiSports.Models;

namespace UltiSports.Test
{
    [TestClass]
    public class UnitTest1
    {
        private Activity activityOne;

        [TestMethod]
        public void TestMethod1()
        {
            activityOne = new Activity()
            {
                Name = "TestActivity",
                DayOfTheWeek = DateTime.Today.DayOfWeek.ToString(),
                IsActive = true,
                PreferredLocation = null,
                PreferredTime = new TimeSpan(10,10,10),
                Managers = null
            };

            var mockRepo= new Mock<IActivityRepository>();
            var mockGeneric = new Mock<IGenericRepository<Activity>>();


            mockGeneric.Setup(x => x.GetByID(It.IsAny<string>())).Returns(activityOne);
            mockRepo.Setup(x => x.GetAll());

            var activityService = new ActivityRepository(mockGeneric.Object);

            activityService.GetAll();
        }
    }
}
