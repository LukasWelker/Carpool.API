using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Buisness.Models
{
    public class CarpoolDto
    {
        public string CarpoolName { get; set; }
        public string Start { get; set; }
        public string Destination {get; set; }
        public string Time { get; set; }
        public int Seatcount { get; set; }
        public string ExistenceOfDriver { get; set; }
        public string UserName  { get; set; }

        public CarpoolDto(string carpoolName, string start, string destination, string time, int seatcount, string existenceOfDriver, string userName)
        {
            CarpoolName = carpoolName;
            Start = start;
            Destination = destination;
            Time = time;
            Seatcount = seatcount;
            ExistenceOfDriver = existenceOfDriver;
            UserName = userName;
        }
    }
}
