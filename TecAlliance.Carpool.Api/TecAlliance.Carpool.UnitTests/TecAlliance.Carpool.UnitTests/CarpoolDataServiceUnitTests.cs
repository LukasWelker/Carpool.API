using System.Reflection;
using TecAlliance.Carpool.Data.Services;
using TecAlliance.Carpool.Data.Models;
using FluentAssertions;

namespace TecAlliance.Carpool.Data.UnitTests
{
    [TestClass]
    public class CarpoolDataServiceUnitTests
    {

        public CarpoolDataService PrepareCarpoolDataServicesTestObject(List<CarpoolEntity> carpoolsList)
        {
            var carpoolDataServices = new CarpoolDataService();
            var originalPath = Assembly.GetExecutingAssembly().Location;
            string filteredoriginalpath = Path.GetDirectoryName(originalPath);
            string newPath = Path.Combine(filteredoriginalpath, @"..\..\..\..\..\",
                "TecAlliance.Carpool.UnitTests\\TecAlliance.Carpool.UnitTests\\CSV-Files-UnitTests\\CarpoolList.csv");
            carpoolDataServices.FilePath = newPath;
            using (File.Create(carpoolDataServices.FilePath)) { }
            foreach (var carpool in carpoolsList)
            {
                carpoolDataServices.PrintObjectIntoCsv(carpool);
            }
            return carpoolDataServices;
        }
        private List<CarpoolEntity> PrepareCarpoolsList()
        {
            var passengers = new List<int>() { 1, 2 };
            var carpool1 = new CarpoolEntity(0, "LastWish", "Moon", "Earth", "09:00", 5, "yes", passengers);
            var carpool2 = new CarpoolEntity(1, "Finalstand", "Jupiter", "Pluto", "18:00", 5, "no", passengers);
            var carpoolsList = new List<CarpoolEntity>() { carpool1, carpool2 };
            return carpoolsList;
        }
        [TestMethod]
        public void ShouldGetNextHigherCarpollId()
        {
            //Arrange
            // Erstellt ein CarpoolDataServicesTestObjekt mit der inder PrepareCarpoolList festgelegten Daten
            var carpoolaDataServices = PrepareCarpoolDataServicesTestObject(PrepareCarpoolsList());
            int expected = 2;

            //Act
            var actual = carpoolaDataServices.GetNextHigherId();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ShouldGetCarpoolWithMatchingId()
        {
            //Arrange
            var carpoolaDataServices = PrepareCarpoolDataServicesTestObject(PrepareCarpoolsList());
            var passengers = new List<int>() { 1, 2 };

            var expected = new CarpoolEntity(1, "Finalstand", "Jupiter", "Pluto", "18:00", 5, "no", passengers);
            var carpool1 = new CarpoolEntity(0, "LastWish", "Moon", "Earth", "09:00", 5, "yes", passengers);
            //Act
            var actual = carpoolaDataServices.SearchForSpecificCarpoolInCsvAndReadIt(0);

            //Arrange
            //Assert.AreEqual(actual, expected);
            //using FluentAssertion
            actual.Should().BeEquivalentTo(carpool1);
        }
    }
}