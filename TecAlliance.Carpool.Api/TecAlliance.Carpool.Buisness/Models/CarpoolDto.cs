using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class CarpoolDto
    {

        public int CarpoolId { get; set; }
        public string CarpoolName { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public string Time { get; set; }
        public int Seatcount { get; set; }
        public int ExistenceOfDriver { get; set; }
        // Property is a List of PassengerInfoDto Objects
        public List<PassengerInfoDto> PassengerInfoDto { get; set; }


        //public string UserName  { get; set; }

        public CarpoolDto(int carpolId, string carpoolName, string start, string destination, string time, int seatcount, int existenceOfDriver, List<PassengerInfoDto> passengerInfoDto)
        {
            CarpoolId = carpolId;
            CarpoolName = carpoolName;
            Start = start;
            Destination = destination;
            Time = time;
            Seatcount = seatcount;
            ExistenceOfDriver = existenceOfDriver;
            PassengerInfoDto = passengerInfoDto;
        }
        //Default constructor is needed if i have to build a new object CarpoolDTo
        public CarpoolDto()
        {

        }

    }
}
