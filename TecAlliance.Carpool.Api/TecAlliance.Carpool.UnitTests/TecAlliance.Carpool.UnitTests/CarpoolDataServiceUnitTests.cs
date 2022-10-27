using System.Reflection;
using TecAlliance.Carpool.Data.Services;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.UnitTests
{
    [TestClass]
    public class CarpoolDataServiceUnitTests
    {
        [TestMethod]
        public CarpoolDataService PrepareCarpoolUnitDataServicesTestObject(List<Carpools> carpoolsList)
        {
            var carpoolDataServices = new CarpoolDataService();
            var originalPath = Assembly.GetExecutingAssembly().Location;
            string filteredoriginalpath = Path.GetDirectoryName(originalPath);
            carpoolDataServices.normalPath = Path.Combine(filteredoriginalpath, @"..\..\..\..\..\",
                "TecAlliance.Carpool.Api\\TecAlliance.Carpool.Data.UnitTests\\CSV-Files-UnitTests\\CarpoolUnitList.csv");
            using (File.Create(carpoolDataServices.normalPath));
            foreach(var carpool in carpoolsList)
            {
                carpoolDataServices.PrintObjectIntoCsv(carpool);
            }
            return carpoolDataServices;
        }
    }
}