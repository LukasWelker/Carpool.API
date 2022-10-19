using System.Text.RegularExpressions;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Buisness.Services
{
    public class DriverBusinessService
    {
        private readonly DriverDataService driverDataService;
        public static Regex nameRegex = new Regex("[A-Za-zÀ-ȕ0-9]");
        public DriverBusinessService()
        {
            driverDataService = new DriverDataService();
        }
        public void AddDriver(PassengerDto driverDto)
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
            else if (driverDto.Password.Length >= 5)
            {
                Driver driver = ConvertDriverDtoToDriver(driverDto);
                driverDataService.AddNewDriverToCsv(driver);
            }
        }
        public Driver ConvertDriverDtoToDriver(PassengerDto driverDto)
        {
            var convertedDriver = new Driver(driverDto.FirstName, driverDto.LastName, driverDto.Password);
            return convertedDriver;
        }
        public PassengerDto GetSpecificPassenger(int Id)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
                //var driver = new Driver();
                //Um auf einen Rückgabewert der Method zuzugreifen muss ich eine neue Variable in diesem Fall foo intialisieren
                Driver foo = driverDataService.SearchForSpecificPassengerInCsvAndReadIt(Id);
                PassengerDto passenger = ConvertDriverToDriverDto(foo);
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
                List<Driver> everyPassenger = driverDataService.DisplayEveryPassenger();
                List<PassengerDto> passes = new List<PassengerDto>();
                foreach (Driver driver in everyPassenger)
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
        public PassengerDto ConvertDriverToDriverDto(Driver driver)
        {
            var convertedDriverDto = new PassengerDto(driver.FirstName, driver.LastName, driver.Password);
            return convertedDriverDto;
        }
        public List<PassengerDto> ConvertDriverListToPassengerList(List<Driver> fo)
        {
            var convertedDriverList = new List<PassengerDto>();
            return convertedDriverList;
        }
    }
}
