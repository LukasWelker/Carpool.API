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
        private int Id = 0;
        public void AddNewDriverToCsv(Driver driver)
        //public void AddNewDriverToCsv(string firstName, string lastName, string password)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
                var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
                if (readText != null && readText.Length > 0)
                {
                    Id = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
                }
            }
            var driverscsv = $"{Id};{driver.FirstName};{driver.LastName};{driver.Password}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", driverscsv, Encoding.UTF8);
        }
        public Driver SearchForSpecificPassengerInCsvAndReadIt(int Id)
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
            var passengerToReturn = new Driver();
            if (readText != null && readText.Length > 0)
            {
                readText.ToList();
                var filteredPassenger = readText.Where(x => x.Contains(Id.ToString()));
                foreach(var passenger in filteredPassenger)
                {
                    var splittedPassenger = passenger.Split(';');
                    passengerToReturn.FirstName = splittedPassenger[1];
                    passengerToReturn.LastName = splittedPassenger[2];
                    passengerToReturn.Password = splittedPassenger[3];
                }

            }
            return passengerToReturn;
        }
        public List<Driver> DisplayEveryPassenger()
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
            List<Driver> listOfPassengers = new List<Driver>();
            foreach(var passenger in readText)
            {
                var splittedPassenger = passenger.Split(";");

                var finalPassenger = new Driver(splittedPassenger[1], splittedPassenger[2], splittedPassenger[3]);
                listOfPassengers.Add(finalPassenger);
            }
            return listOfPassengers;
        }

    }
}
