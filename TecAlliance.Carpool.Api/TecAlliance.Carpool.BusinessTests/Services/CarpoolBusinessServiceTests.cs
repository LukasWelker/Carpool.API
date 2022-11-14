using Microsoft.VisualStudio.TestTools.UnitTesting;
using TecAlliance.Carpool.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;
using Moq;
using TecAlliance.Carpool.Business.Interfaces;
using TecAlliance.Carpool.Data.Interfaces;

namespace TecAlliance.Carpool.Business.Services.Tests
{
    [TestClass()]
    public class CarpoolBusinessServiceTests
    {
        private readonly CarpoolBusinessService _carpoolBusinessService;
        private readonly Mock<ICarpoolDataService> _carpoolDataServiceMock = new Mock<ICarpoolDataService>();
        private readonly Mock<IPassengerBusinessService> _passengerBusinessServiceMock = new Mock<IPassengerBusinessService>();
        public CarpoolBusinessServiceTests()
        {
            _carpoolBusinessService = new CarpoolBusinessService(_passengerBusinessServiceMock.Object,_carpoolDataServiceMock.Object);
        }
        [TestMethod()]
        public void TestCreateNewCarpool()
        {
            var passengers = new PassengerInfoDto(1, "Lukas");
            var passengersList = new List<PassengerInfoDto>();
            passengersList.Add(passengers);
            var expected = new CarpoolDto(1, "FallenAngel", "Moon", "Earth", "09:00", 5, "yes",passengersList);

            Assert.Fail();
        }

    }
}