using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
{
    public class Carpools
    {
        public string CarpoolName { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public string Time { get; set; }
        public int Seatcount { get; set; }
        public string ExistenceOfDriver { get; set; }
        
        //public string Passengers { get; set; }
        public string Passengers { get; set; }

        //Ovverride standard constructor
        public Carpools(string carpoolName, string start, string destination, string time, int seatcount, string existenceOfDriver, string userName)
        {
            CarpoolName = carpoolName;
            Start = start;
            Destination = destination;
            Time = time;
            Seatcount = seatcount;
            ExistenceOfDriver = existenceOfDriver;
            Passengers = userName;
        }
        //Standardconstructor for creating new object without specific values
        public Carpools()
        { 
        }
    }
}
