using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Interfaces;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{
    public class PassengerDataService : IPassengerDataService
    {
        private string driversPath = TecAlliance.Carpool.Data.Properties.Resources.DriverCsvPath;
        private int baseId = 0;
        public CarpoolDataService carpoolDataService;
        string connectionString = @"Data Source =localhost; Initial Catalog = Carpool;Integrated Security=True;";
        public PassengerDataService()
        {
            carpoolDataService = new CarpoolDataService();
        }

        public void AddNewPassenger(Passenger passenger)
        //public void AddNewDriverToCsv(string firstName, string lastName, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"INSERT INTO Users (Surname,Name,Password) VALUES('{passenger.FirstName}', '{passenger.LastName}', '{passenger.Password}')";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Passenger SearchForSpecificPassengerInCsvAndReadIt(int Id)
        {
            var passengerToReturn = new Passenger(); ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SQL-Injection 
                string queryString = $"SELECT * FROM Users WHERE Users.UserId = @Id";
                SqlCommand command = new SqlCommand(queryString, connection);         
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                command.Parameters["@Id"].Value = Id;   
                //SQL-Injection End
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        passengerToReturn.Id = Convert.ToInt32(reader["UserId"]);
                        passengerToReturn.FirstName = reader["Surname"].ToString();
                        passengerToReturn.LastName = reader["Name"].ToString();
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return passengerToReturn;
        }

        public List<Passenger> DisplayEveryPassenger()
        {
            List<Passenger> listOfPassengers = new List<Passenger>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var foo = new Passenger();
                        foo.Id = Convert.ToInt32(reader["UserId"]);
                        foo.FirstName = reader["Surname"].ToString();
                        foo.LastName = reader["Name"].ToString();
                        listOfPassengers.Add(foo);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return listOfPassengers;
        }
        public void DeleteAllPassengers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "DELETE FROM Users";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }
        public void DeleteSpecificPassenegrById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = $"DELETE FROM Users WHERE Users.UserId = @Id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                command.Parameters["@Id"].Value = Id;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}


