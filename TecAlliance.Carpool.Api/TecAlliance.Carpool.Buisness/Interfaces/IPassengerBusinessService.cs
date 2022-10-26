using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;


namespace TecAlliance.Carpool.Business.Services
{
    public interface IPassengerBusinessService
    {
        PassengerDto AddDriver(PassengerDto driverDto);

        List<PassengerDto> GetAllPassengers();
        PassengerDto GetSpecificPassenger(int passengerId);

        void ConnectionToDeleteAllPassengers();

        void ConnectionToDeleteSpecificPassenger(int passengerId);
      
    }
}
