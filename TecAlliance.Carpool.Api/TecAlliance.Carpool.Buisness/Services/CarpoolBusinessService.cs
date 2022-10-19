//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TecAlliance.Carpool.Buisness.Models;
//using TecAlliance.Carpool.Data.Services;
//using TecAlliance.Carpool.Data.Models;
//using System.Text.RegularExpressions;
//using System.Reflection;

//namespace TecAlliance.Carpool.Buisness.Services
//{
//    public class CarpoolBusinessService
//    {
//        private readonly CarpoolDataService carpoolDataService;
//        // reggex durchläfut eine Liste und checkt ob eine bedinung aus chars erfüllt ist
//        public static Regex regex = new Regex("^\\d+$");
//        public CarpoolBusinessService()
//        {
//            carpoolDataService = new CarpoolDataService();
//        }
//        public void CreateNewCarpool(CarpoolDto carpoolDtos)
//        {
//            //basic Errorhandling
//            if (string.IsNullOrEmpty(carpoolDtos.CarpoolName) || string.IsNullOrEmpty(carpoolDtos.Start) || string.IsNullOrEmpty(carpoolDtos.Destination) || string.IsNullOrEmpty(carpoolDtos.Time) ||
//                 string.IsNullOrEmpty(carpoolDtos.ExistenceOfDriver) || string.IsNullOrEmpty(carpoolDtos.UserName))
//            {
//                throw new ArgumentNullException("Ungültige Eingabe");
//            }
//            // check if Seatcount is correct
//            else if (carpoolDtos.Seatcount.ToString().Length > 1 && !regex.IsMatch(carpoolDtos.Seatcount.ToString())) 
//            {
//                throw new ArgumentOutOfRangeException("Ungültige Eingabe, Sitzplatzangabe muss eine Zahl sein");
//            }
//            else
//            {
//                Carpools carpools = ConvertCarpoolDtosToCarpools(carpoolDtos);
//                carpoolDataService.CreateNewCarpool(carpools);
//            }
           
//        }
//        public CarpoolDto GetSpecificCarpool(int Id)
//        {
//            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
//            {
//               return carpoolDataService.SearchForSpecificCarpoolInCsvAndReadIt(Id);
//            }
//            else
//            {
//                throw new ArgumentException("Die Datei ist leider nicht vorhanden");
//            }
//        }
//        public CarpoolDto DeleteSpecificName(string FirstName)
//        {
//            if (CheckIfUsersNameExistDM(FirstName, "C:\\Projects001\\FahrgemeinschaftProject\\Drivers.csv")
//                       || CheckIfUsersNameExistDM(FirstName, "C:\\Projects001\\FahrgemeinschaftProject\\Members.csv"))
//            {

//            }
//        }
//        //change void to Carpools
//        public List<CarpoolDto> GetAllCarpools()
//        {
//            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
//            {
//                return carpoolDataService.DisplayEveryCarpool();
//            }
//            else
//            {
//                throw new ArgumentException("Die Datei ist leider nicht vorhanden");
//            }
//        }

//        public CarpoolDto ConvertCarpoolDtosToCarpools(CarpoolDto carpoolDtos)
//        {
//            var convertedCarpools = new Carpools(carpoolDtos.CarpoolName, carpoolDtos.Start, carpoolDtos.Destination, carpoolDtos.Time, carpoolDtos.Seatcount, carpoolDtos.ExistenceOfDriver, carpoolDtos.UserName);
//            return convertedCarpools;
//        }
//        public static bool CheckIfUsersNameExistDM(string FirstName, string path)
//        {
//            if (!File.Exists(path))
//            {
//                return false;
//            }
//            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
//            List<string> readList = readText.ToList();
//            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').First() == FirstName);
//            if (filteredmeml != null)
//                return true;
//            return false;
//        }
//    }
//}
