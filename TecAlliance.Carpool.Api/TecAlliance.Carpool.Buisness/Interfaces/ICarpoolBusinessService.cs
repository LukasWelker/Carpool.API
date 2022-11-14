using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Business.Interfaces
{
    public interface ICarpoolBusinessService
    {
        /// <summary>
        /// Method which is used to check if the input is correct and forwards the input to Datalayer
        /// </summary>
        /// <param name="carpoolDtos"></param>
        /// <param name="userId"></param>
        /// <returns> CarpoolDto</returns>
        CarpoolDto CreateNewCarpool(CarpoolDto carpoolDtos, int userId);

        /// <summary>
        /// Method which is used to check if the input is correct and forwards the input to Datalayer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> CarpoolDto</returns>
        CarpoolDto GetSpecificCarpool(int Id);

        /// <summary>
        /// Method which is used to convert Carpoolobject ito CarpoolDtoobject and is a connection to the Datalayer
        /// </summary>
        /// <returns> List of CarpoolDto</returns>
        List<CarpoolDto> GetAllCarpools();

        /// <summary>
        /// Method is used to create a new PassengerDtoobject
        /// </summary>
        /// <param name="passengerDto"></param>
        /// <returns> PassengerDto</returns>
        PassengerDto GetPassengerDto(PassengerDto passengerDto);

        /// <summary>
        /// Method is a connection to the Datalayer (needed for the Deletion of a Carpool)
        /// </summary>
        void ConnectionToDeleteAllcarpools();

        /// <summary>
        /// Method is used to check the input is correct and forwards the correct input to the Datalayer
        /// </summary>
        /// <param name="carpoolId"></param>
        void ConnectionToDeleteSpecificCarpool(int carpoolId);

        /// <summary>
        /// Method is used as aconnection between the Controller-and Datalayer (needed for the Deletion of a Carpool)
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <param name="userId"></param>
        void ConnectionToAddUserToCarpool(int carpoolId, int userId);

        /// <summary>
        /// Method is used to forward the correct input to the Datalayer
        /// </summary>
        /// <param name="carpoolName"></param>
        /// <param name="carpoolId"></param>
        /// <returns> CarpoolDto</returns>
        CarpoolDto ConnectionToChangeCarpoolName(string carpoolName, int carpoolId);

        /// <summary>
        /// Method is used to forward tegh correct input to the Datalayer
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <param name="userId"></param>
        void ConnectionToLeaveCarpool(int carpoolId, int userId);



    }
}
