using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        public PassengerDto GetSpecificPassenger(int passengerId)
        {
            if (CheckIfPassengerAndPathExists(passengerId, "C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {

                //var driver = new Driver();
                //Um auf einen Rückgabewert der Method zuzugreifen muss ich eine neue Variable in diesem Fall foo intialisieren
                Passenger returnedPassenger = driverDataService.SearchForSpecificPassengerInCsvAndReadIt(passengerId);
                PassengerDto passenger = ConvertDriverToDriverDto(returnedPassenger);

                return passenger;
            }
            else
            {
                throw new Exception("Hallo");
            }
        }
       
      
        public void ConnectionToDeleteAllPassengers()
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv"))
            {
                driverDataService.DeleteAllPassengers();
            }
            else
            {
                ExecptionThatFileOrPassengerDoesNotExist();
            }
        }

        public void ConnectionToDeleteSpecificPassenger(int passengerId)
        {

            if(CheckIfPassengerAndPathExists(passengerId, "C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                driverDataService.DeleteSpecificPassenegrById(passengerId);
            }
            else
            {
                ExecptionThatFileOrPassengerDoesNotExist();
            }
        }

        public static bool CheckIfPassengerAndPathExists(int passengerId, string path)
        {
            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
            List<string> readList = readText.ToList();
            // untersucht jede zeile für zeile bis etwas erstes gefunden wurde, das gesplittet also, in diesem Fall der Id entspricht.
            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').First() == passengerId.ToString());
            if (filteredmeml != null)
                return true;
            return false;
        }

        private void ExecptionThatFileOrPassengerDoesNotExist()
        {
            throw new Exception("Diese Datei wurde bereits gelöscht oder sie existiert nicht.");
        }

        public Passenger ConvertDriverDtoToDriver(PassengerDto driverDto)
        {
            var convertedDriver = new Passenger(driverDto.Id, driverDto.FirstName, driverDto.LastName, driverDto.Password);
            return convertedDriver;
        }

        public PassengerDto ConvertDriverToDriverDto(Passenger driver)
        {
            var convertedDriverDto = new PassengerDto(driver.Id, driver.FirstName, driver.LastName, driver.Password);
            return convertedDriverDto;
        }

        public List<PassengerDto> ConvertDriverListToPassengerList(List<Passenger> fo)
        {
            var convertedDriverList = new List<PassengerDto>();
            return convertedDriverList;
        }
    }
}
