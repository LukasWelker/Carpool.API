using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Business.Services
{
    public interface ICarpoolBusinessService
    {

        CarpoolDto CreateNewCarpool(CarpoolDto carpoolDtos, int userId);

        CarpoolDto GetSpecificCarpool(int Id);

        List<CarpoolDto> GetAllCarpools();


        PassengerDto GetPassengerDto(PassengerDto passengerDto);

        void ConnectionToDeleteAllcarpools();

        void ConnectionToDeleteSpecificCarpool(int carpoolId);

        void ConnectionToAddUserToCarpool(int carpoolId, int userId);

        CarpoolDto ConnectionToChangeCarpoolName(string carpoolName, int carpoolId);

        void ConnectionToLeaveCarpool(int carpoolId, int userId);
        
        

    }
}
