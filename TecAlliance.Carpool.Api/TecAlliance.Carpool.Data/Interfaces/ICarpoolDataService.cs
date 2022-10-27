using System.Data;
using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
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

        void CreateNewCarpool(Carpools carpools, int userId);


        List<Carpools> DisplayEveryCarpool();

        Carpools SearchForSpecificCarpoolInCsvAndReadIt(int Id);

        void DeleteAllCarpools();

        void DeleteSpecificCarpool(int Id);

        void InstantDeletionOfCarPoolIfEmpty(int carpoolId);

        void AddUserToCarpool(int carpoolId, int userId);

        void LeaveCarpool(int carpoolId, int userId);

        Carpools? ChangeCarpoolName(string carpoolName, int carpoolId);
      
    
    }
}
