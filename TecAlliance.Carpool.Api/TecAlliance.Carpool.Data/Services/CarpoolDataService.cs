using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{
    public class CarpoolDataService
    {/*      private string carpoolPath = TecAlliance.Carpool.Data.Properties.Resources.CarpoolCsvPath;*/


        private int baseId = 0;
        public void CreateNewCarpool(Carpools carpools, int userId)
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
            string eachPassengerId = $"{userId}";
            int passengerId2 = userId;
            foreach (int passengerId in carpools.PassengerIds)
            {
                //Wenn der Stringbuilder nicht funktionirt einfach += schreiben
                eachPassengerId = passengerId2.ToString();
                finalString.Append($"{passengerId2},");
            }
            string finalCarpool = $"{carpools.CarpoolId};{carpools.CarpoolName};{carpools.Start};{carpools.Destination};" +
                $"{carpools.Time};{carpools.Seatcount};{carpools.ExistenceOfDriver};{eachPassengerId}\n";
            File.AppendAllText("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", finalCarpool);
        }
        public Carpools SearchForSpecificCarpoolInCsvAndReadIt(int Id)
        {
            var readText = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            //create new object of Carpool class to build the object afterwards new
            var carpoolToReturn = new Carpools();
            if (readText != null && readText.Length > 0)
            {
                //create new List to go trough the List with Linq
                List<string> readList = ReadCarPoolList("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
                //search in the List for line which contains the Id
                var filteredUserCarPools = readList.Where(x => x.Contains(Id.ToString()));
                // build the object foreach matching line in the List
                foreach (var carpool in filteredUserCarPools)
                {
                    //splitting carpool in each "part" in given example Id, Name, Time etc
                    var splittedCarpool = carpool.Split(';');
                    var foo = new List<int>();
                    //building the new Carpool object
                    carpoolToReturn.CarpoolId = Convert.ToInt32(splittedCarpool[0]);
                    carpoolToReturn.CarpoolName = splittedCarpool[1];
                    carpoolToReturn.Start = splittedCarpool[2];
                    carpoolToReturn.Destination = splittedCarpool[3];
                    carpoolToReturn.Time = splittedCarpool[4];
                    carpoolToReturn.Seatcount = Convert.ToInt32(splittedCarpool[5]);
                    carpoolToReturn.ExistenceOfDriver = splittedCarpool[6];
                    carpoolToReturn.PassengerIds = foo;
                    foo.Add(Convert.ToInt32(splittedCarpool[7]));
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
        public List<string> ReadCarPoolList(string path)
        {
            var CarPoolList = File.ReadAllLines(path, Encoding.UTF8);
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
            List<string> readList = ReadCarPoolList("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            var MatchingCarPool = readList.FirstOrDefault(x => x.Split(';')[0] == IdOfCarpool.ToString());
            List<string> carPool = readList.Where(x => x.Split(';')[0] != IdOfCarpool.ToString()).ToList();
            carPool.Add(MatchingCarPool);
            carPool.Remove(MatchingCarPool);
            var orderdCarpool = carPool.OrderBy(x => x.Split(';')[0]);
            File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", orderdCarpool);
        }

        public void AddUserToCarpool(int carpoolId, int userId)
        {
            string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            var MatchingCarPool = readList.FirstOrDefault(x => x.Split(';')[0] == carpoolId.ToString()) + "," + userId;
            var CarPool = readList.Where(x => x.Split(';')[0] != carpoolId.ToString()).ToList();
            CarPool.Add(MatchingCarPool);
            var orderdCarpool = CarPool;
            File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", orderdCarpool);
        }

        public void LeaveCarpool(int carpoolId, int userId)
        {
            List<string> readList = ReadCarPoolList("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            //So kann man was entfernen und hinzufügen in einer CSV Datei
            //Man sucht in der Csv Datei nach der Zeile mit der passenden Carpool Id
            var MatchingCarPool = readList
                .FirstOrDefault(x => x
                    .Split(';')[0] == carpoolId.ToString());
            //Sucht/Liest alle anderen Zeilen, die nicht gesucht sind
            var carPoolOriginal = readList
                .Where(x => x
                    .Split(';')[0] != carpoolId.ToString())
                .ToList();
            //Splitet die passende Zeile in einzelne in strings
            var splitedMatchingCarPool = MatchingCarPool.Split(';');
            //Splitted die gewünschte Zeile intern nach ',' um einen einzelnen Eintrag zu removen und um nur den einen Eintrag in der passenden Zeile zu bearbeiten
            var SplitSearchedLine = splitedMatchingCarPool[7].Split(',').ToList();
            //Suche alle Einträge raus, die nicht der UserId entsprechen
            var differntiateListInput = SplitSearchedLine.Where(x => !x.Equals(userId.ToString()));
            //Ersellt einen String (mit der JoinMethod) ohne die UserId, da  DifferntiateListInput alle Ids außer die userid beinhaltet
            var recreateLine = string.Join(",", differntiateListInput);
            //Schreibt die Zeile neu , wie man sie braucht
            var wishResultSplitedMatchingCarPool = $"{splitedMatchingCarPool[0]};{splitedMatchingCarPool[1]};{splitedMatchingCarPool[2]};{splitedMatchingCarPool[3]};{splitedMatchingCarPool[4]};{splitedMatchingCarPool[5]};" +
                $"{splitedMatchingCarPool[6]}; {recreateLine}";
            //Fügt alle Zeilen, die man aus der Liste nicht braucht mit der einen veränderten zusammen in eine Liste
            carPoolOriginal.Add(wishResultSplitedMatchingCarPool);
            //Löscht die ganze Liste um in Zeile 395 die Liste wie in Zeile 392 zusammengefügt in eine Csv Datei zu schreiben
            File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", carPoolOriginal);
            InstantDeletionOfCarPoolIfEmpty(carpoolId);
        }

        public Carpools? ChangeCarpoolName(string carpoolName, int carpoolId)
        {

            Carpools newCarpool = new Carpools();
            List<string> readList = ReadCarPoolList("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
            List<string> updatedList = new List<string>();
            foreach (string line in readList)
            {
                var splittedCarpool = line.Split(';');
                var splittedPassengerIds = splittedCarpool[8].Split(',');
                var passengerIds = new List<int>();
                foreach (var splittedPassengerId in splittedPassengerIds)
                {
                    passengerIds.Add(int.Parse(splittedPassengerId));
                }
                if (carpoolId == Convert.ToInt32(splittedCarpool[0]))
                {
                    newCarpool.CarpoolName = splittedCarpool[1];
                    newCarpool.Start = splittedCarpool[2];
                    newCarpool.Destination = splittedCarpool[3];
                    newCarpool.Time = splittedCarpool[4];
                    newCarpool.Time = splittedCarpool[5];
                    newCarpool.Seatcount = Convert.ToInt32(splittedCarpool[6]);
                    newCarpool.ExistenceOfDriver = splittedCarpool[7];
                    newCarpool.PassengerIds = passengerIds;

                    splittedCarpool[1] = carpoolName;
                    updatedList.Add($"{splittedCarpool[0]};{splittedCarpool[1]};{splittedCarpool[2]};" +
                        $"{splittedCarpool[3]};{splittedCarpool[4]};{splittedCarpool[5]};{splittedCarpool[5]};{splittedCarpool[6]};{splittedCarpool[7]};{splittedCarpool[8]}");
                }
                else
                {
                    updatedList.Add(line);
                }
                File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", updatedList);
            }
            return newCarpool;
        }
        public void InstantDeletionOfCarPoolIfEmpty(int carpoolId)
        {
            string[] CarPoolList = File.ReadAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", Encoding.UTF8);
            var id = Convert.ToInt32(carpoolId);
            //Uwandlung des einzelnen Strings in Array 
            //string[] singleCarPool = CarPoolList[id].Split(';');
            string[] singleCarPool;
            for (int i = 0; i < CarPoolList.Count(); i++)
            {
                singleCarPool = CarPoolList[i].Split(';');
                if (singleCarPool.Length <= 8 && singleCarPool[7].Trim(' ') == string.Empty)
                {
                    //Ersetzt den String mit einem leeren String wenn das Array kleiner gleich 8 ist
                    CarPoolList[id] = string.Empty;
                    CarPoolList[id].ToList();
                    List<string> readList = ReadCarPoolList(("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"));
                    var CarPoolOriginal = readList
                       .Where(x => x
                           .Split(';')[0] != carpoolId.ToString())
                       .ToList();
                    CarPoolOriginal.Add(CarPoolList[id]);
                    File.Delete("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv");
                    File.AppendAllLines("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv", CarPoolOriginal);

                }
            }
        }
        public void DynamicPath()
        {
            var originalpath = Assembly.GetExecutingAssembly().Location;
            string path = Path.GetDirectoryName(originalpath);
            string[] splittedPath = path.Split('\\');
            string NewPath = Path.Combine(path, @"..\..\..\..\..\..\");
            var finalPath = File.Create($"{NewPath}\\Carpool.csv");
            finalPath.ToString();

        }

    }
}
