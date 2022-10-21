using System.Text.RegularExpressions;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Buisness.Services
{
    public class PassengerBusinessService
    {
        private int count = 0;
        private readonly DriverDataService driverDataService;
        public static Regex nameRegex = new Regex("[A-Za-zÀ-ȕ0-9]");
        public PassengerBusinessService()
        {
            driverDataService = new DriverDataService();
        }
        public PassengerDto AddDriver(PassengerDto driverDto)
        {
            //Basic Errorhandling
            if (string.IsNullOrEmpty(driverDto.FirstName) || string.IsNullOrEmpty(driverDto.LastName) || string.IsNullOrEmpty(driverDto.Password))
            {
                throw new InvalidDataException();
            }
            else if (!nameRegex.IsMatch(driverDto.FirstName))
            {
                throw new ArgumentException("Ungültige Eingabe");
            }
            else if (driverDto.Password.Length < 5)
            {
                throw new Exception("Passwort zu kurz");
            }
            else
            {
                Passenger driver = ConvertDriverDtoToDriver(driverDto);
                driverDataService.AddNewDriverToCsv(driver);
                PassengerDto passengerDto = ConvertDriverToDriverDto(driver);
                return passengerDto;
            }
        }
        public Passenger ConvertDriverDtoToDriver(PassengerDto driverDto)
        {
            var convertedDriver = new Passenger(driverDto.Id, driverDto.FirstName, driverDto.LastName, driverDto.Password);
            return convertedDriver;
        }

        public PassengerDto GetSpecificPassenger(int Id)
        {
           
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
               
                //var driver = new Driver();
                //Um auf einen Rückgabewert der Method zuzugreifen muss ich eine neue Variable in diesem Fall foo intialisieren
                Passenger returnedPassenger = driverDataService.SearchForSpecificPassengerInCsvAndReadIt(Id);
                PassengerDto passenger = ConvertDriverToDriverDto(returnedPassenger);
               
                return passenger;
            }
            else 
            {
                throw new ArgumentException("Datei existoert nicht");
            }
        }
        public List<PassengerDto> GetAllPassengers()
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
                List<Passenger> everyPassenger = driverDataService.DisplayEveryPassenger();
                List<PassengerDto> passes = new List<PassengerDto>();
                foreach (Passenger driver in everyPassenger)
                {
                    PassengerDto passenger = ConvertDriverToDriverDto(driver);
                    passes.Add(passenger);
                }
                return passes;
            }
            else
            {
                throw new ArgumentException("Die Datei ist leider nicht vorhanden");
            }
        }
        public PassengerDto ConvertDriverToDriverDto(Passenger driver)
        {
            var convertedDriverDto = new PassengerDto(driver.Id,driver.FirstName, driver.LastName, driver.Password);
            return convertedDriverDto;
        }
        public List<PassengerDto> ConvertDriverListToPassengerList(List<Passenger> fo)
        {
            var convertedDriverList = new List<PassengerDto>();
            return convertedDriverList;
        }
        public void ConnectionToDeletePassengers()
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
                driverDataService.DeleteAllPassengers();
            }
            else
            {
                throw new Exception("Diese Datei wurde bereits gelöscht oder sie existiert nicht.");
            }
        }
    }
}
