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
            AddUserToCarpool(carpools.CarpoolId, userId);
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
                string queryString = $"INSERT INTO CarpoolUsers (CarpoolId, UserId) VALUES (@carpoolId, @userId)";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@carpoolId", System.Data.SqlDbType.Int);
                command.Parameters["@carpoolId"].Value = carpoolId;
                command.Parameters.Add("@userId", System.Data.SqlDbType.Int);
                command.Parameters["@userId"].Value = userId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void LeaveCarpool(int carpoolId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM CarpoolUsers WHERE CarpoolUsers.CarpoolId = @carpoolId AND CarpoolUsers.UserId = @UserId";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@carpoolId", System.Data.SqlDbType.Int);
                command.Parameters["@carpoolId"].Value = carpoolId;
                command.Parameters.Add("@UserId", System.Data.SqlDbType.Int);
                command.Parameters["@UserId"].Value = userId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //Carpool is nullable because if it does not exists it returns an exception
        public CarpoolEntity? ChangeCarpoolName(string carpoolName, int carpoolId)
        {
           
                // create new object of Carpoolclass
                CarpoolEntity newCarpool = new CarpoolEntity();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                //SQL-Injection 
                string queryString = $" UPDATE Carpool SET CarpoolName = '{carpoolName}' WHERE CarpoolId = @carpoolId";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@carpoolId", System.Data.SqlDbType.Int);
                    command.Parameters["@carpoolId"].Value = carpoolId;
                    //SQL-Injection End
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            newCarpool.CarpoolId = Convert.ToInt32(reader["CarpoolId"]);
                            newCarpool.CarpoolName = reader["CarpoolName"].ToString();
                            newCarpool.Start = reader["Startlocation"].ToString();
                            newCarpool.Destination = reader["Destination"].ToString();
                            newCarpool.Seatcount = Convert.ToInt32(reader["Seatcount"]);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                return newCarpool;
              
           

        }
        #endregion


        #region Helper Methods
        
        public List<int> GetPassengerIds(int carpoolId)
        {
            List<int> passengerIds = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SQL-Injection 
                string queryString = $"SELECT * FROM CarpoolUsers WHERE CarpoolId = @carpoolId";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@carpoolId", System.Data.SqlDbType.Int);
                command.Parameters["@carpoolId"].Value = carpoolId;
                //SQL-Injection End
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        passengerIds.Add(Convert.ToInt32(reader["UserId"]));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return passengerIds;
        }
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
