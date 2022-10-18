using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Buisness.Services
{
    public class DriverBusinessService
    {
        private DriverDataService driverDataService;
        public DriverBusinessService()
        {
            driverDataService = new DriverDataService();
        }
        public void AddDriver( DriverDto driverDto)
        {
           
                if (string.IsNullOrEmpty(driverDto.FirstName) || string.IsNullOrEmpty(driverDto.LastName) || string.IsNullOrEmpty(driverDto.Password))
                {
                    throw new InvalidDataException();
                }
                else if(driverDto.Password.Length < 5)
                {
                    throw new Exception("Passwort zu kurz");
                }
                else if (driverDto.Password.Length >= 5)
                {
                    driverDataService.AddNewDriverToCsv(driver);
                }
            
           
           
        } 
       
    }
}
