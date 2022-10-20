using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        public Passenger(int id, string firstName, string lastName, string password)
        {
            Id = id;    
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
        public Passenger()
        {
                
        }
    }
}
