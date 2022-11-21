//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using TecAlliance.Carpool.Data.Interfaces;
//using TecAlliance.Carpool.Data.Models;

//namespace TecAlliance.Carpool.Data.Services
//{
//    public class PassengerDataService : IPassengerDataService
//    {
//        private string driversPath = TecAlliance.Carpool.Data.Properties.Resources.DriverCsvPath;
//        private  int baseId = 0;
//        public CarpoolDataService carpoolDataService;
//        public PassengerDataService()
//        {
//            carpoolDataService = new CarpoolDataService();
//        }
       
//        public void AddNewPassenger(Passenger driver)
//        //public void AddNewDriverToCsv(string firstName, string lastName, string password)
//        {
//            if (File.Exists(DynamicPath()))
//            {
//                var readText = File.ReadAllLines(DynamicPath(), Encoding.UTF8);
//                if (readText != null && readText.Length > 0)
//                {
//                    baseId = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
//                    driver.Id = baseId;
//                }
//            }
//            var driverscsv = $"{driver.Id};{driver.FirstName};{driver.LastName};{driver.Password}\n";
//            File.AppendAllText(DynamicPath(), driverscsv, Encoding.UTF8);
//        }
//        public Passenger SearchForSpecificPassengerInCsvAndReadIt(int Id)
//        {
//            var readText = File.ReadAllLines(DynamicPath(), Encoding.UTF8);
//            var passengerToReturn = new Passenger();
//            if (readText != null && readText.Length > 0)
//            {
//                readText.ToList();
//                var filteredPassenger = readText.Where(x => x.Contains(Id.ToString()));
//                foreach(var passenger in filteredPassenger)
//                {
//                    var splittedPassenger = passenger.Split(';');
//                    passengerToReturn.Id = Convert.ToInt32(splittedPassenger[0]);
//                    passengerToReturn.FirstName = splittedPassenger[1];
//                    passengerToReturn.LastName = splittedPassenger[2];
//                    passengerToReturn.Password = splittedPassenger[3];
//                }
//                return passengerToReturn;
//            }
//            else
//            {
//                throw new Exception("Diese Datei existiert leider nicht");
//            }
//        }

//        public List<Passenger> DisplayEveryPassenger()
//        {
//            if (File.Exists(DynamicPath()))
//            {
//                var readText = File.ReadAllLines(DynamicPath(), Encoding.UTF8);
//                List<Passenger> listOfPassengers = new List<Passenger>();
//                foreach (var passenger in readText)
//                {
//                    var splittedPassenger = passenger.Split(";");

//                    var finalPassenger = new Passenger(Convert.ToInt32(splittedPassenger[0]), splittedPassenger[1], splittedPassenger[2], splittedPassenger[3]);
//                    listOfPassengers.Add(finalPassenger);
//                }
//                return listOfPassengers;
//            }
//            else
//            {
//                throw new Exception("Diese Datei ecistiert leider nicht");
//            }
           
//        }
//        public void DeleteAllPassengers()
//        {
//            File.Delete(DynamicPath());
//        }
//        public void DeleteSpecificPassenegrById(int Id)
//        {
//            if (File.Exists(DynamicPath()))
//            {
//                int passengerId = Id;
//                var readText = File.ReadAllLines(DynamicPath(), Encoding.UTF8);
//                List<string> readList = carpoolDataService.ReadCarPoolList(DynamicPath());
//                var matchingPassenger = readList.FirstOrDefault(x => x.Split(';')[0] == passengerId.ToString());
//                var carPool = readList.Where(x => x.Split(';')[0] != passengerId.ToString()).ToList();
//                carPool.Add(matchingPassenger);
//                carPool.Remove(matchingPassenger);
//                var orderdPassengerList = carPool.OrderBy(x => x.Split(';')[0]);
//                // File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv");
//                File.WriteAllLines(DynamicPath(), orderdPassengerList);
//            }
//            else
//            {
//                ExecptionThatFileOrPassengerDoesNotExist();
//            }
            
//        }
//        private string DynamicPath()
//        {
//            var originalPath = Assembly.GetExecutingAssembly().Location;
//            string path = Path.GetDirectoryName(originalPath);
//            string newPath = Path.Combine(path, @"..\..\..\..\..\", "TecAlliance.Carpool.Api\\TecAlliance.Carpool.Data\\CSV-Files\\Passenger.csv");
//            return newPath.ToString();
//        }

//        public static bool CheckIfPassengerAndPathExists(int passengerId, string path)
//        {
//            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
//            List<string> readList = readText.ToList();
//            // untersucht jede zeile für zeile bis etwas erstes gefunden wurde, das gesplittet also, in diesem Fall der Id entspricht.
//            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').First() == passengerId.ToString());
//            if (filteredmeml != null)
//                return true;
//            return false;
//        }

//        private void ExecptionThatFileOrPassengerDoesNotExist()
//        {
//            new Exception("Diese Datei wurde bereits gelöscht oder sie existiert nicht.");

//        }

//    }
//}
