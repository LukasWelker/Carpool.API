using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{
    public class CarpoolDataService
    {
        private int Id = 0;
        public void CreateNewCarpool(Carpools carpools)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
                if (readText != null && readText.Length > 0)
                {
                    Id = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
                }
            }
            var finalCarpool = $"{Id};{carpools.CarpoolName};{carpools.Start};{carpools.Destination};{carpools.Time};{carpools.Seatcount};{carpools.ExistenceOfDriver};{carpools.Passengers}\n";
            File.AppendAllText($"C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", finalCarpool);
        }
        public Carpools SearchForSpecificCarpoolInCsvAndReadIt(int Id)
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            var carpoolToReturn = new Carpools();
            if (readText != null && readText.Length > 0)
            {
                List<string> readList = ReadCarPoolList();
                var filteredUserCarPools = readList.Where(x => x.Contains(Id.ToString()));
                foreach (var carpool in filteredUserCarPools)
                {
                    var splittedCarpool = carpool.Split(';');
                    carpoolToReturn.CarpoolName = splittedCarpool[1];
                    carpoolToReturn.Start = splittedCarpool[2];
                    carpoolToReturn.Destination = splittedCarpool[3];
                    carpoolToReturn.Time = splittedCarpool[4];
                    carpoolToReturn.Seatcount = Convert.ToInt32(splittedCarpool[5]);
                    carpoolToReturn.ExistenceOfDriver = splittedCarpool[6];
                    carpoolToReturn.Passengers = splittedCarpool[7];
                }
            }
            return carpoolToReturn;
        } 
        public List<Carpools> DisplayEveryCarpool()
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<Carpools> listOfCarpools = new List<Carpools>();
            
            foreach(var line in readText)
            {
                string[] splittedCarPoolList = line.Split(';');
                //hiermit "baue" ich das Objekt 
                var carpool = new Carpools(splittedCarPoolList[1], splittedCarPoolList[2], splittedCarPoolList[3], splittedCarPoolList[4], Convert.ToInt32(splittedCarPoolList[5]), splittedCarPoolList[6], splittedCarPoolList[7]);
                //
                listOfCarpools.Add(carpool);
            }
            return listOfCarpools;
        }
        private static List<string> ReadCarPoolList()
        {

            var CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            return readList;
        }
       
    }
}
