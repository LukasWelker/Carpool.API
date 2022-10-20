using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Buisness.Models
{
    public class CarpoolDto
    {

        public int CarpoolId { get; set; }
        public string CarpoolName { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public string Time { get; set; }
        public int Seatcount { get; set; }
        public string ExistenceOfDriver { get; set; }
       public List<PassengerInfoDto> PassengerInfoDto { get; set; }
       

        //public string UserName  { get; set; }

        public CarpoolDto(int carpolId, string carpoolName, string start, string destination, string time, int seatcount, string existenceOfDriver, List<PassengerInfoDto> passengerInfoDto)
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
        public CarpoolDto()
        {

        }
      
    }
}
