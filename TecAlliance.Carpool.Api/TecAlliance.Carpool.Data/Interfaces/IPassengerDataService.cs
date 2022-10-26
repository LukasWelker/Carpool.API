using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Data.Services
{
    public interface IPassengerDataService
    {
        void AddNewPassenger(Passenger driver);

        Passenger SearchForSpecificPassengerInCsvAndReadIt(int Id);

        List<Passenger> DisplayEveryPassenger();

        void DeleteAllPassengers();

        void DeleteSpecificPassenegrById(int Id);
    }
}
