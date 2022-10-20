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
        private string driversPath = TecAlliance.Carpool.Data.Properties.Resources.DriverCsvPath;
        private  int baseId = 0;
        public void AddNewDriverToCsv(Passenger driver)
        //public void AddNewDriverToCsv(string firstName, string lastName, string password)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
                var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
                if (readText != null && readText.Length > 0)
                {
                    baseId = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
                    driver.Id = baseId;
                }
            }
            var driverscsv = $"{driver.Id};{driver.FirstName};{driver.LastName};{driver.Password}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", driverscsv, Encoding.UTF8);
        }
        public Passenger SearchForSpecificPassengerInCsvAndReadIt(int Id)
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
            var passengerToReturn = new Passenger();
            if (readText != null && readText.Length > 0)
            {
                readText.ToList();
                var filteredPassenger = readText.Where(x => x.Contains(Id.ToString()));
                foreach(var passenger in filteredPassenger)
                {
                    var splittedPassenger = passenger.Split(';');
                    passengerToReturn.Id = Convert.ToInt32(splittedPassenger[0]);
                    passengerToReturn.FirstName = splittedPassenger[1];
                    passengerToReturn.LastName = splittedPassenger[2];
                    passengerToReturn.Password = splittedPassenger[3];
                }

            }
            return passengerToReturn;
        }
        public List<Passenger> DisplayEveryPassenger()
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv", Encoding.UTF8);
            List<Passenger> listOfPassengers = new List<Passenger>();
            foreach(var passenger in readText)
            {
                var splittedPassenger = passenger.Split(";");

                var finalPassenger = new Passenger(Convert.ToInt32(splittedPassenger[0]), splittedPassenger[1], splittedPassenger[2], splittedPassenger[3]);
                listOfPassengers.Add(finalPassenger);
            }
            return listOfPassengers;
        }

    }
}
