using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
{
    public class CarpoolEntity
    {

        public int CarpoolId { get; set; }
        public string CarpoolName { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public string Time { get; set; }
        public int Seatcount { get; set; }
        public int ExistenceOfDriver { get; set; }
        public List<int> PassengerIds{ get; set; }

        //Ovverride standard constructor
        public CarpoolEntity(int carpoolId, string carpoolName, string start, string destination, string time, int seatcount,
            int existenceOfDriver, List<int> passengerIds)
        {
            CarpoolId = carpoolId;
            CarpoolName = carpoolName;
            Start = start;
            Destination = destination;
            Time = time;
            Seatcount = seatcount;
            ExistenceOfDriver = existenceOfDriver;
            PassengerIds = passengerIds;
        }
        //Standardconstructor for creating new object without specific values
        public CarpoolEntity()
        { 
        }
    }
}
