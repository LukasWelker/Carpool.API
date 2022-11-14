using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;


namespace TecAlliance.Carpool.Business.Interfaces
{
    public interface IPassengerBusinessService
    {
        /// <summary>
        /// Method is used to check the Userinput and forwards the correct input to the Datalayer
        /// </summary>
        /// <param name="driverDto"></param>
        /// <returns> PassengerDto</returns>
        PassengerDto AddDriver(PassengerDto driverDto);

        /// <summary>
        /// Method is used to convert a Passenger into PassengerDto and is a connection to the Datalayer
        /// </summary>
        /// <returns> List of PassengerDto</returns>
        List<PassengerDto> GetAllPassengers();

        /// <summary>
        /// Method is used to convert a Passenger into PassengerDto and is a connection to the Datalayer
        /// </summary>
        /// <param name="passengerId"></param>
        /// <returns> PassengerDto</returns>
        PassengerDto GetSpecificPassenger(int passengerId);

        /// <summary>
        ///  Method is a connection to the Datalayer (needed for the Deletion of a Passenger)
        /// </summary>
        void ConnectionToDeleteAllPassengers();

        /// <summary>
        ///  Method is a connection to the Datalayer (needed for the Deletion of a specific Passenger)
        /// </summary>
        /// <param name="passengerId"></param>
        void ConnectionToDeleteSpecificPassenger(int passengerId);

    }
}
