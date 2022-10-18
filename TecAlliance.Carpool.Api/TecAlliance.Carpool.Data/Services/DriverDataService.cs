using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{
    public class DriverDataService
    {
        public void AddNewDriverToCsv(Driver driver)
        //public void AddNewDriverToCsv(string firstName, string lastName, string password)
        {
            var driverscsv = $"{driver.FirstName};{driver.LastName};{driver.Password}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", driverscsv, Encoding.UTF8);
        }
       
    }
}
