using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Buisness.Models
{
    public class PassengerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        

        public PassengerDto(string firstName, string lastName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            
        }
    }
}
