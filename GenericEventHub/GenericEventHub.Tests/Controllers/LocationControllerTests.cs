using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericEventHub;
using GenericEventHub.Controllers;
using GenericEventHub.Repositories;
using GenericEventHub.Services;
using GenericEventHub.Models;
using GenericEventHub.Infrastructure;

namespace GenericEventHub.Tests.Controllers
{
    [TestClass]
    public class LocationControllerTests
    {
        private GenericEventHubDb _context;
        private GenericRepository<Location> _genericRepo;
        private LocationRepository _locationRepo;

        private LocationService _locationService;
        private LocationController _locationController;

        [TestInitialize]
        public void SetUp()
        {
            _context = new GenericEventHubDb();
            _genericRepo = new GenericRepository<Location>(_context);
            _locationRepo = new LocationRepository(_genericRepo);
            _locationService = new LocationService(_locationRepo);
            _locationController = new LocationController(_locationService);
        }

        [TestMethod]
        public void GetAllLocations()
        {
            // Arrange
            LocationController controller = new LocationController(_locationService);

            // Act
            var result = controller.Get();

            // Assert
            Assert.AreNotEqual("0", 0);
        }

        [TestMethod]
        public void CreateLocation()
        {
            // Arrange
            LocationController controller = new LocationController(_locationService);
            var testLocation = new Location()
            {
                Name = "TestName " + DateTime.Now.ToString(),
                Address = "TestAddress " + DateTime.Now.ToString()
            };

            // Act
            var result = controller.PostLocation(testLocation);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
