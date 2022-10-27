using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.SampleProvider
{
    //Replaces the example in swagger with the statements inside the following method
    public class PassengerDtoProvider : IExamplesProvider<PassengerDto>
    {
        /// <summary>
        /// Replaces the example in swagger with the statements inside the following method
        /// </summary>
        /// <returns>Returns an object CarpoolDto with all its properties</returns>
        public PassengerDto GetExamples()
        {
            
            return new PassengerDto()
            {
                Id = 1,
                FirstName = "Itachi",
                LastName = "Uchiha",
                Password = "Akatasuki",

            };
        }
    }
}
