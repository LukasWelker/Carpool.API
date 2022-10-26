using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.SampleProvider
{
    public class CarpoolDtoProvider :IExamplesProvider<CarpoolDto>
    {
        //Replaces the example in swagger with the statements inside the following method
        public CarpoolDto GetExamples()
        {
            return new CarpoolDto()
            {
                CarpoolId = 1,
                CarpoolName = "Akatsuki",
                Start = "Hidden Leaf Village",
                Destination = "Sunagakure",
                Time = "09:00",
                Seatcount = 5,
                ExistenceOfDriver = "Yes",
                PassengerInfoDto = new List<PassengerInfoDto>() 
                { 
                    new PassengerInfoDto() 
                    { 
                        PassengerId = 1, 
                        PassengerName = "Itachi"
                    }
                }


            };
        }
    }
}
