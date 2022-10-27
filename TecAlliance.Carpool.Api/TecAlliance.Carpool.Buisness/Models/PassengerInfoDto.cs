using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class PassengerInfoDto
    {
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }

        public PassengerInfoDto(int passengerId, string passengerName)
        {
            PassengerId = passengerId;
            PassengerName = passengerName;
        }
        //Default constructor
        public PassengerInfoDto()
        {

        }
    }
   
}
