using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{
    public class CarpoolDataService
    {/*      private string carpoolPath = TecAlliance.Carpool.Data.Properties.Resources.CarpoolCsvPath;*/

        private int baseId = 0;
        public void CreateNewCarpool(Carpools carpools)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
                if (readText != null && readText.Length > 0)
                {
                    baseId = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
                    carpools.CarpoolId = baseId;
                }
            }
            //Foreach durch list (passengerid) in Carpool
            // nehme jede id =?> speicher in einen string
            //wenn liste fertig nimm string oben und schreibe das am enbde in den string {carpools.PassengerIds}\n";
            StringBuilder finalString = new StringBuilder();
            string eachPassengerId = "";
            foreach (int passengerId in carpools.PassengerIds)
            {
                //Wenn der Stringbuilder nicht funktionirt einfach += schreiben
                eachPassengerId = passengerId.ToString();
                finalString.Append($"{passengerId},");
            }
            var finalCarpool = $"{carpools.CarpoolId};{carpools.CarpoolName};{carpools.Start};{carpools.Destination};" +
                $"{carpools.Time};{carpools.Seatcount};{carpools.ExistenceOfDriver};{eachPassengerId}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", finalCarpool);
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
                    var foo = new List<int>();
                    foo.Add(Convert.ToInt32(splittedCarpool[7]));
                    carpoolToReturn.CarpoolId = Convert.ToInt32(splittedCarpool[0]);
                    carpoolToReturn.CarpoolName = splittedCarpool[1];
                    carpoolToReturn.Start = splittedCarpool[2];
                    carpoolToReturn.Destination = splittedCarpool[3];
                    carpoolToReturn.Time = splittedCarpool[4];
                    carpoolToReturn.Seatcount = Convert.ToInt32(splittedCarpool[5]);
                    carpoolToReturn.ExistenceOfDriver = splittedCarpool[6];
                    carpoolToReturn.PassengerIds = foo;
                }
            }
            return carpoolToReturn;
        }
        public List<Carpools> DisplayEveryCarpool()
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<Carpools> listOfCarpools = new List<Carpools>();

            foreach (var line in readText)
            {
                string[] splittedCarPoolList = line.Split(';');
                var foo = new List<int>();
                foo.Add(Convert.ToInt32(splittedCarPoolList[7]));
                //hiermit "baue" ich das Objekt 
                var carpool = new Carpools(Convert.ToInt32(splittedCarPoolList[0]), splittedCarPoolList[1], splittedCarPoolList[2], splittedCarPoolList[3],
                    splittedCarPoolList[4], Convert.ToInt32(splittedCarPoolList[5]), splittedCarPoolList[6], foo);
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
        public void DeleteAllCarpools()
        {
            File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
        }
        public void DeleteSpecificCarpool(int Id)
        {
            //Delete Carpool nach der Pause machen
            int IdOfCarpool = Id;
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = ReadCarPoolList();
            var MatchingCarPool = readList.FirstOrDefault(x => x.Split(';')[0] == IdOfCarpool.ToString());
            var carPool = readList.Where(x => x.Split(';')[0] != IdOfCarpool.ToString()).ToList();
           carPool.Add(MatchingCarPool);
            carPool.Remove(MatchingCarPool);
            var orderdCarpool = carPool.OrderBy(x => x.Split(';')[0]);
            File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", orderdCarpool);
        }
    }
}
