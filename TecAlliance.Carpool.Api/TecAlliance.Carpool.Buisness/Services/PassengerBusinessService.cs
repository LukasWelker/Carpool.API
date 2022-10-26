using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;


namespace TecAlliance.Carpool.Business.Services
{
    public class PassengerBusinessService :IPassengerBusinessService
    {
        IPassengerDataService driverDataService;
        private int count = 0;
       
        public static Regex nameRegex = new Regex("[A-Za-zÀ-ȕ0-9]");
        public PassengerBusinessService(IPassengerDataService driverDataService)
        {
            this.driverDataService = driverDataService;
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
                driverDataService.AddNewPassenger(driver);
                PassengerDto passengerDto = ConvertDriverToDriverDto(driver);
                return passengerDto;
            }
        }

        public List<PassengerDto> GetAllPassengers()
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

        public PassengerDto GetSpecificPassenger(int passengerId)
        {
                //var driver = new Driver();
                //Um auf einen Rückgabewert der Method zuzugreifen muss ich eine neue Variable in diesem Fall foo intialisieren
                Passenger returnedPassenger = driverDataService.SearchForSpecificPassengerInCsvAndReadIt(passengerId);
                PassengerDto passenger = ConvertDriverToDriverDto(returnedPassenger);

                return passenger;
        }


        public void ConnectionToDeleteAllPassengers()
        {
                driverDataService.DeleteAllPassengers();
        }

        public void ConnectionToDeleteSpecificPassenger(int passengerId)
        {
                driverDataService.DeleteSpecificPassenegrById(passengerId);
        }

      
        private Passenger ConvertDriverDtoToDriver(PassengerDto driverDto)
        {
            var convertedDriver = new Passenger(driverDto.Id, driverDto.FirstName, driverDto.LastName, driverDto.Password);
            return convertedDriver;
        }

        private PassengerDto ConvertDriverToDriverDto(Passenger driver)
        {
            var convertedDriverDto = new PassengerDto(driver.Id, driver.FirstName, driver.LastName, driver.Password);
            return convertedDriverDto;
        }

        private List<PassengerDto> ConvertDriverListToPassengerList(List<Passenger> fo)
        {
            var convertedDriverList = new List<PassengerDto>();
            return convertedDriverList;
        }
    }
}
