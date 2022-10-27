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
    /// <summary>
    /// Creating an Interface to use Dependeny Injection
    /// Advantages: - enables to work on the business and Data level at the same time
    ///             - less  dependencies where you do not need them
    ///             - higher modularity when you change a database       
    /// </summary>
    public interface IPassengerDataService
    {
        void AddNewPassenger(Passenger driver);

        Passenger SearchForSpecificPassengerInCsvAndReadIt(int Id);

        List<Passenger> DisplayEveryPassenger();

        void DeleteAllPassengers();

        void DeleteSpecificPassenegrById(int Id);
    }
}
