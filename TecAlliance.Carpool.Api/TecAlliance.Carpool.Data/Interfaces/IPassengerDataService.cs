using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Interfaces
{
    /// <summary>
    /// Creating an Interface to use Dependeny Injection
    /// Advantages: - enables to work on the business and Data level at the same time
    ///             - less  dependencies where you do not need them
    ///             - higher modularity when you change a database       
    /// </summary>
    public interface IPassengerDataService
    {
        /// <summary>
        /// Creates a new Passenger
        /// </summary>
        /// <param name="driver"></param>
        void AddNewPassenger(Passenger driver);

        /// <summary>
        /// Display a specific passenger based on its individual ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Passenger SearchForSpecificPassengerInCsvAndReadIt(int Id);

        /// <summary>
        /// displays every existing Passenger
        /// </summary>
        /// <returns></returns>
        List<Passenger> DisplayEveryPassenger();

        /// <summary>
        /// deletes every existing Passenger
        /// </summary>
        void DeleteAllPassengers();

        /// <summary>
        /// deletes a specific Passenger based on the given ID
        /// </summary>
        /// <param name="Id"></param>
        void DeleteSpecificPassenegrById(int Id);
    }
}
