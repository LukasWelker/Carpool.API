using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Interfaces;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{

    public class CarpoolDataService : ICarpoolDataService
    {/*      private string carpoolPath = TecAlliance.Carpool.Data.Properties.Resources.CarpoolCsvPath;*/
        private int baseId = 0;
        private string? path;
        string connectionString = @"Data Source =localhost; Initial Catalog = Carpool;Integrated Security=True;";
        public string FilePath //TODO Andre fragen was das ist
        {
            get
            {
                if (this.path is null)
                    this.path = this.DynamicPath();
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }
        public void CreateNewCarpool(CarpoolEntity carpools, int userId)
        {
            //carpools.CarpoolId = GetNextHigherId();
            //PrintObjectIntoCsv(carpools);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Carpool (CarpoolName,Startlocation, Destination,Seatcount) VALUES('{carpools.CarpoolName}', '{carpools.Start}', '{carpools.Destination}', '{carpools.Seatcount}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        #region Get Methods
        public List<CarpoolEntity> DisplayEveryCarpool()
        {
            List<CarpoolEntity> listOfCarpools = new List<CarpoolEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Carpool";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var carpools = new CarpoolEntity();
                        carpools.CarpoolId = Convert.ToInt32(reader["CarpoolId"]);
                        carpools.CarpoolName = reader["CarpoolName"].ToString();
                        carpools.Start = reader["Startlocation"].ToString();
                        carpools.Destination = reader["Destination"].ToString();
                        carpools.Seatcount = Convert.ToInt32(reader["Seatcount"]);
                        listOfCarpools.Add(carpools);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return listOfCarpools;
        }
        public CarpoolEntity SearchForSpecificCarpoolInCsvAndReadIt(int Id)
        {
            var carpoolToReturn = new CarpoolEntity(); ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"SELECT * FROM Carpool WHERE Carpool.CarpoolId= @Id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                command.Parameters["@Id"].Value = Id;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        carpoolToReturn.CarpoolId = Convert.ToInt32(reader["CarpoolId"]);
                        carpoolToReturn.CarpoolName = reader["CarpoolName"].ToString();
                        carpoolToReturn.Start = reader["Startlocation"].ToString();
                        carpoolToReturn.Destination = reader["Destination"].ToString();
                        carpoolToReturn.Seatcount = Convert.ToInt32(reader["Seatcount"]);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return carpoolToReturn;
        }
        #endregion

        #region Delete Methods
        public void DeleteAllCarpools()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "DELETE FROM Carpool";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteSpecificCarpool(int Id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM Carpool WHERE Carpool.CarpoolId = @Id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                command.Parameters["@Id"].Value = Id;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void InstantDeletionOfCarPoolIfEmpty(int carpoolId)
        {
            string[] carpoolList = File.ReadAllLines(this.FilePath, Encoding.UTF8);
            var id = Convert.ToInt32(carpoolId);
            //Uwandlung des einzelnen Strings in Array 
            //string[] singleCarPool = CarPoolList[id].Split(';');
            string[] singleCarPool;
            for (int i = 0; i < carpoolList.Count(); i++)
            {
                singleCarPool = carpoolList[i].Split(';');
                if (singleCarPool.Length <= 8 && singleCarPool[7].Trim(' ') == string.Empty)
                {
                    List<string> updatedList = carpoolList.ToList();
                    updatedList.Remove(carpoolList[i]);
                    var CarPoolOriginal = updatedList
                       .Where(x => x
                           .Split(';')[0] != carpoolId.ToString())
                       .ToList();
                    File.Delete(this.FilePath);
                    File.AppendAllLines(this.FilePath, CarPoolOriginal);
                }
            }
        }
        #endregion

        #region Put Methods
        public void AddUserToCarpool(int carpoolId, int userId)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO CarpoolUsers (CarpoolId, UserId) VALUES ({carpoolId},{userId})";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            if (CheckIfCarpoolAndPathExists(carpoolId.ToString(), this.FilePath))
            {
                string[] CarPoolList = File.ReadAllLines(this.FilePath, Encoding.UTF8);
                List<string> readList = CarPoolList.ToList();
                var MatchingCarPool = readList.FirstOrDefault(x => x.Split(';')[0] == carpoolId.ToString()) + "," + userId;
                var CarPool = readList.Where(x => x.Split(';')[0] != carpoolId.ToString()).ToList();
                CarPool.Add(MatchingCarPool);
                var orderdCarpool = CarPool;
                File.Delete(this.FilePath);
                File.AppendAllLines(this.FilePath, orderdCarpool);
            }
            else
            {
                ExecptionThatFileOrCarpoolDoesNotExist();
            }
        }

        public void LeaveCarpool(int carpoolId, int userId)
        {
            if (CheckIfCarpoolAndPathExists(carpoolId.ToString(), this.FilePath))
            {
                List<string> readList = ReadCarPoolList(this.FilePath);
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
                var trimmedMatchingCarpool = MatchingCarPool.Trim(' ');
                //Splitet die passende Zeile in einzelne in strings
                var splitedMatchingCarPool = trimmedMatchingCarpool.Split(';');
                //Splitted die gewünschte Zeile intern nach ',' um einen einzelnen Eintrag zu removen und um nur den einen Eintrag in der passenden Zeile zu bearbeiten
                var SplitSearchedLine = splitedMatchingCarPool[7].Split(',').ToList();
                //Suche alle Einträge raus, die nicht der UserId entsprechen
                var differntiateListInput = SplitSearchedLine.Where(x => !x.Equals(userId.ToString()));
                //Ersellt einen String (mit der JoinMethod) ohne die UserId, da  DifferntiateListInput alle Ids außer die userid beinhaltet
                var recreateLine = string.Join(",", differntiateListInput);
                //Schreibt die Zeile neu , wie man sie braucht
                var wishResultSplitedMatchingCarPool = $"{splitedMatchingCarPool[0]};{splitedMatchingCarPool[1]};{splitedMatchingCarPool[2]};{splitedMatchingCarPool[3]};{splitedMatchingCarPool[4]};{splitedMatchingCarPool[5]};" +
                    $"{splitedMatchingCarPool[6]};{recreateLine}";
                //Fügt alle Zeilen, die man aus der Liste nicht braucht mit der einen veränderten zusammen in eine Liste
                carPoolOriginal.Add(wishResultSplitedMatchingCarPool);
                //Löscht die ganze Liste um in Zeile 395 die Liste wie in Zeile 392 zusammengefügt in eine Csv Datei zu schreiben
                File.Delete(this.FilePath);
                File.AppendAllLines(this.FilePath, carPoolOriginal);
                InstantDeletionOfCarPoolIfEmpty(carpoolId);
            }
            else
            {
                ExecptionThatFileOrCarpoolDoesNotExist();
            }
        }

        //Carpool is nullable because if it does not exists it returns an exception
        public CarpoolEntity? ChangeCarpoolName(string carpoolName, int carpoolId)
        {
            if (CheckIfCarpoolNameExists(carpoolName, this.FilePath))
            {
                // create new object of Carpoolclass
                CarpoolEntity newCarpool = new CarpoolEntity();
                // Read the CSV-File
                List<string> readList = ReadCarPoolList(this.FilePath);
                // create a new List 
                List<string> updatedList = new List<string>();
                //loop trough every line in readlist 
                foreach (string line in readList)
                {
                    //splits each part 
                    var splittedCarpool = line.Split(';');
                    var splittedPassengerIds = splittedCarpool[8].Split(',');
                    var passengerIds = new List<int>();
                    foreach (var splittedPassengerId in splittedPassengerIds)
                    {
                        passengerIds.Add(Convert.ToInt32(splittedPassengerId));
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
                    File.AppendAllLines(this.FilePath, updatedList);
                }
                return newCarpool;
            }
            else
            {
                return null;
            }

        }
        #endregion


        #region Helper Methods

        public int GetNextHigherId()
        {
            if (File.Exists(this.FilePath))
            {
                var readText = File.ReadAllLines(this.FilePath, Encoding.UTF8);
                if (!String.IsNullOrEmpty(readText[0]) && !String.IsNullOrWhiteSpace(readText[0])/*readText != null && readText.Length > 0*/)
                {
                    return baseId = Convert.ToInt32(readText.Last().Split(';').First()) + 1;
                }
            }
            return 0;
        }

        public void PrintObjectIntoCsv(CarpoolEntity carpools)
        {
            // effektivere Lösung
            string finalCarpool = $"{carpools.CarpoolId};{carpools.CarpoolName};{carpools.Start};{carpools.Destination};" +
                $"{carpools.Time};{carpools.Seatcount};{carpools.ExistenceOfDriver};";
            foreach (var passengerId in carpools.PassengerIds)
            {
                finalCarpool += $"{passengerId},";
            }
            finalCarpool = finalCarpool.Trim(',');
            finalCarpool += "\n";
            File.AppendAllText(this.FilePath, finalCarpool);
        }


        private string DynamicPath()
        {
            var originalpath = Assembly.GetExecutingAssembly().Location;

            var path = Path.GetDirectoryName(originalpath);

            string newPath = Path.Combine(path, @"..\..\..\..\..\", "TecAlliance.Carpool.Api\\TecAlliance.Carpool.Data\\CSV-Files\\Carpool.csv");

            return newPath.ToString();
        }
        private static bool CheckIfCarpoolAndPathExists(string value, string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
            List<string> readList = readText.ToList();
            // untersucht jede zeile für zeile bis etwas erstes gefunden wurde, das gesplittet also, in diesem Fall der Id entspricht.
            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').First() == value);
            if (filteredmeml != null)
                return true;
            return false;
        }
        private static bool CheckIfCarpoolNameExists(string carpoolName, string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            string[] readText = File.ReadAllLines(path, Encoding.UTF8);
            List<string> readList = readText.ToList();
            // untersucht jede zeile für zeile bis etwas erstes gefunden wurde, das gesplittet ajlso, in diesem Fall der Id entspricht.
            var filteredmeml = readText.FirstOrDefault(x => x.Split(';').Skip(1).First() == carpoolName);
            if (filteredmeml != null)
                return true;
            return false;
        }
        private void ExecptionThatFileOrCarpoolDoesNotExist()
        {
            throw new Exception("Diese Datei oder die Fahrgemeinschaft exisitiert leider nicht");
        }
        public List<string> ReadCarPoolList(string path)
        {
            var CarPoolList = File.ReadAllLines(path, Encoding.UTF8);
            List<string> readList = CarPoolList.ToList();
            return readList;
        }
        #endregion
    }
}
