using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Buisness.Services
{
    public class CarpoolBusinessService
    {
        private CarpoolDataService carpoolDataService;
        public CarpoolBusinessService()
        {
            carpoolDataService = new CarpoolDataService();
        }

    }
}
