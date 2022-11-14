using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Interfaces;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Interfaces;
using TecAlliance.Carpool.Data.Models;


namespace TecAlliance.Carpool.Business.Services
{
    public class PassengerBusinessService :IPassengerBusinessService
    {

        IPassengerDataService driverDataService;
        public static Regex nameRegex = new Regex("[A-Za-zÀ-ȕ0-9]");
        public PassengerBusinessService(IPassengerDataService driverDataService)
        {
            this.driverDataService = driverDataService;
        }
        /// <summary>
        /// Connection between Data and Controller level, needed to create a new passenger
        /// </summary>
        /// <param name="driverDto"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"> input is invalid</exception>
        /// <exception cref="ArgumentException"> input is invalid</exception>
        /// <exception cref="Exception"> Password is to short</exception>
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
                //Converts a passengerDto to a normal passenger
                Passenger driver = ConvertDriverDtoToDriver(driverDto);
                //AddNewPassenger returns a new passenger but we need a passengerdto to return the PassengerDto to the Controller level
                driverDataService.AddNewPassenger(driver);
                //Converts the Passenger to a DTO in order to return it to the controller level
                PassengerDto passengerDto = ConvertDriverToDriverDto(driver);
                //returns a PassengerDto to the controller level
                return passengerDto;
            }
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to display every Passenger ever created
        /// </summary>
        /// <returns></returns>
        public List<PassengerDto> GetAllPassengers()
        {
                List<Passenger> everyPassenger = driverDataService.DisplayEveryPassenger();
            // creating a new List to return this List which contains Objects from the PassengerDto- Type
                List<PassengerDto> passes = new List<PassengerDto>();
                foreach (Passenger driver in everyPassenger)
                {
                //Converting each Passenger to a PassengerDto
                    PassengerDto passenger = ConvertDriverToDriverDto(driver);
                //Add the converted PassengerDTos to a List, that we can return them properly
                    passes.Add(passenger);
                }
                return passes;
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to display a specific Passenger
        /// </summary>
        /// <param name="passengerId">uniqe Id to identify a specifoc passenger</param>
        /// <returns></returns>
        public PassengerDto GetSpecificPassenger(int passengerId)
        {
                //var driver = new Driver();
                //Um auf einen Rückgabewert der Method zuzugreifen muss ich eine neue Variable in diesem Fall foo intialisieren
                Passenger returnedPassenger = driverDataService.SearchForSpecificPassengerInCsvAndReadIt(passengerId);
                PassengerDto passenger = ConvertDriverToDriverDto(returnedPassenger);

                return passenger;
        }

        /// <summary>
        ///  Connection between Data and Controller level, needed to delete every passenger ever created
        /// </summary>
        public void ConnectionToDeleteAllPassengers()
        {
                driverDataService.DeleteAllPassengers();
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to delete a specifc passenger
        /// </summary>
        /// <param name="passengerId"> uniqe id to identify a specific passenger</param>
        public void ConnectionToDeleteSpecificPassenger(int passengerId)
        {
                driverDataService.DeleteSpecificPassenegrById(passengerId);
        }

      
        private Passenger ConvertDriverDtoToDriver(PassengerDto driverDto)
        {
            //creating a new Passengerobject with its properties and then saving it in a variable, the properties are from the passed PassengerDto object
            var convertedDriver = new Passenger(driverDto.Id, driverDto.FirstName, driverDto.LastName, driverDto.Password);
            return convertedDriver;
        }

        private PassengerDto ConvertDriverToDriverDto(Passenger driver)
        {
            // creating a new PassengerDTOobject with its properties and then saving it in a variable, the propertiy informations are from the passed Passengerobject
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
