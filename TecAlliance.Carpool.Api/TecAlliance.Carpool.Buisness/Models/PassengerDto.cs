using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        public PassengerDto(int id, string firstName, string lastName, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
        //Default constructor
        public PassengerDto()
        {

        }
    }
}
