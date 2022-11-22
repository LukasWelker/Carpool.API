using System.Data;
using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Interfaces
{
    /// <summary>
    /// Creating an Interface to use Dependeny Injection
    /// Advantages: - enables to work on the business and Data level at the same time
    ///             - less  dependencies where you do not need them
    ///             - higher modularity when you change a database       
    /// </summary>
    public interface ICarpoolDataService
    {

        //dependency-injection
        /// <summary>
        /// method that writes the to be created dataset in the existing CSV-File
        /// </summary>
        /// <param name="carpools"></param>
        /// <param name="userId"></param>
        void CreateNewCarpool(CarpoolEntity carpools, int userId);

        /// <summary>
        /// displays every existing carpool
        /// </summary>
        /// <returns></returns>
        List<CarpoolEntity> DisplayEveryCarpool();

        /// <summary>
        /// displays a specific Carpool based on the given ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        CarpoolEntity SearchForSpecificCarpoolInCsvAndReadIt(int Id);

        /// <summary>
        /// deletes every existing Carpool
        /// </summary>
        void DeleteAllCarpools();

        /// <summary>
        /// deletes a specific Carpool based on the given ID
        /// </summary>
        /// <param name="Id"></param>
        void DeleteSpecificCarpool(int Id);

        /// <summary>
        /// Instantdeletion of a Carpool if the last existing member left the specific Carpool
        /// </summary>
        /// <param name="carpoolId"></param>
        void InstantDeletionOfCarPoolIfEmpty(int carpoolId);

        /// <summary>
        /// Adds a User based on its specific passenger, to a specific Carpool based on its specific ID
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <param name="userId"></param>
        void AddUserToCarpool(int carpoolId, int userId);

        /// <summary>
        /// Removes a User based on its specific passenger, from a specific Carpool based on its specific ID
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <param name="userId"></param>
        void LeaveCarpool(int carpoolId, int userId);

        /// <summary>
        /// Changes the Carpoolname of a specific Carpool based on its individual ID
        /// </summary>
        /// <param name="carpoolName"></param>
        /// <param name="carpoolId"></param>
        /// <returns></returns>
        CarpoolEntity? ChangeCarpoolName(string carpoolName, int carpoolId);

        List<int> GetPassengerIds(int carpoolId);

    }
}
