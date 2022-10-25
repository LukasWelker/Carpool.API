using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Buisness.Services
{
    public class CarpoolBusinessService
    {
        private readonly CarpoolDataService carpoolDataService;
        // reggex durchläfut eine Liste und checkt ob eine bedinung aus chars erfüllt ist
        public static Regex regex = new Regex("^\\d+$");
        //Variablen Deklaration
        public PassengerBusinessService driverBusinessService;
        public CarpoolBusinessService()
        {
            //Hier findet die Initialisierung statt
            carpoolDataService = new CarpoolDataService();
            driverBusinessService = new PassengerBusinessService();
        }
        public CarpoolDto CreateNewCarpool(CarpoolDto carpoolDtos, int userId)
        {
            //basic Errorhandling
            if (string.IsNullOrEmpty(carpoolDtos.CarpoolName) || string.IsNullOrEmpty(carpoolDtos.Start) || string.IsNullOrEmpty(carpoolDtos.Destination) || string.IsNullOrEmpty(carpoolDtos.Time) ||
                 string.IsNullOrEmpty(carpoolDtos.ExistenceOfDriver))
            {
                throw new ArgumentNullException("Ungültige Eingabe");
            }
            // check if Seatcount is correct
            else if (carpoolDtos.Seatcount.ToString().Length > 1 && !regex.IsMatch(carpoolDtos.Seatcount.ToString()))
            {
                throw new ArgumentOutOfRangeException("Ungültige Eingabe, Sitzplatzangabe muss eine Zahl sein");
            }
            else
            {
                Carpools carpools = ConvertCarpoolDtosToCarpools(carpoolDtos);
                carpoolDataService.CreateNewCarpool(carpools, userId);
                CarpoolDto carpool = ConvertCarpoolToCarpoolDto(carpools);
                return carpool;
            }
        }
        public CarpoolDto GetSpecificCarpool(int Id)
        {
            Carpools baa = carpoolDataService.SearchForSpecificCarpoolInCsvAndReadIt(Id);
            CarpoolDto carpooldto = ConvertCarpoolToCarpoolDto(baa);
            return carpooldto;
        }

        //change void to Carpools
        public List<CarpoolDto> GetAllCarpools()
        {


            List<Carpools> everyCarpool = carpoolDataService.DisplayEveryCarpool();
            //Ich initialisiere eine Liste aus Dto-Objekten 
            List<CarpoolDto> allCarpools = new List<CarpoolDto>();
            foreach (Carpools carpool in everyCarpool)
            {
                CarpoolDto carpoolDto = ConvertCarpoolToCarpoolDto(carpool);
                allCarpools.Add(carpoolDto);
            }
            return allCarpools;
        }
        public Carpools ConvertCarpoolDtosToCarpools(CarpoolDto carpoolDtos)
        {
            //Erstellung eine rneure intListe, wird benötigt für die Befüllung des Carpoolobjekts
            var intListOfIds = new List<int>();
            intListOfIds.Add(carpoolDtos.CarpoolId);
            var convertedCarpools = new Carpools(carpoolDtos.CarpoolId, carpoolDtos.CarpoolName, carpoolDtos.Start,
                carpoolDtos.Destination, carpoolDtos.Time, carpoolDtos.Seatcount, carpoolDtos.ExistenceOfDriver, intListOfIds);
            return convertedCarpools;
        }
        public CarpoolDto ConvertCarpoolToCarpoolDto(Carpools carpools)
        {
            //Erstellung einer neuen PassengerInfoDtoListeaus objekten
            List<PassengerInfoDto> newPassengerInfoList = new List<PassengerInfoDto>();
            //für jede Id in der Liste aus PassengerIds
            foreach (var id in carpools.PassengerIds)
            {
                var passenger = driverBusinessService.GetSpecificPassenger(id);
                //var passenger = driverBusinessService.GetSpecificPassengerDetail(id);
                //var passenger = driverBusinessService.GetSpecificPassengerInfoDot(id);
                // baue das Objekt mithilfe der Id, bekomme ich übergeben und dem Namen aus GetSpecificPassenger
                var passengerDto = new PassengerInfoDto() { PassengerId = passenger.Id, PassengerName = $"{passenger.FirstName} {passenger.LastName}" };
                newPassengerInfoList.Add(passengerDto);
            }
            var convertedCarpoolDto = new CarpoolDto(carpools.CarpoolId, carpools.CarpoolName, carpools.Start,
                carpools.Destination, carpools.Time, carpools.Seatcount, carpools.ExistenceOfDriver, newPassengerInfoList);
            //Liste aus Objekten muss ich hinzufügen diese:  List<PassengerInfo> passengerInfos
            return convertedCarpoolDto;
        }

        public PassengerDto GetPassengerDto(PassengerDto passengerDto)
        {
            PassengerDto passenger = new PassengerDto();
            return passenger;
        }
        public void ConnectionToDeleteAllcarpools()
        {
            carpoolDataService.DeleteAllCarpools();
        }
        public void ConnectionToDeleteSpecificCarpool(int carpoolId)
        {
            carpoolDataService.DeleteSpecificCarpool(carpoolId);
        }
        public void ConnectionToAddUserToCarpool(int carpoolId, int userId)
        {
            carpoolDataService.AddUserToCarpool(carpoolId, userId);
        }
        public CarpoolDto ConnectionToChangeCarpoolName(string carpoolName, int carpoolId)
        {
            Carpools carpool = carpoolDataService.ChangeCarpoolName(carpoolName, carpoolId);
            CarpoolDto carpoolDto = ConvertCarpoolToCarpoolDto(carpool);
            return carpoolDto;
        }
        public void ConnectionToLeaveCarpool(int carpoolId, int userId)
        {
            carpoolDataService.LeaveCarpool(carpoolId, userId);
        }
        //value can be almost everything in this example it can be carpoolId etc. and the path is alos variable but in the given example it is  the Carpoolfile.

    }
}
