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
        public CarpoolDto CreateNewCarpool(CarpoolDto carpoolDtos)
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
                carpoolDataService.CreateNewCarpool(carpools);
                CarpoolDto carpool = ConvertCarpoolToCarpoolDto(carpools);
                return carpool;
            }
        }
        public CarpoolDto GetSpecificCarpool(int Id)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                Carpools baa = carpoolDataService.SearchForSpecificCarpoolInCsvAndReadIt(Id);
                CarpoolDto carpooldto = ConvertCarpoolToCarpoolDto(baa);
                return carpooldto;
            }
            else
            {
                throw new ArgumentException("Die Datei ist leider nicht vorhanden");
            }
        }

        //change void to Carpools
        public List<CarpoolDto> GetAllCarpools()
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
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
            else
            {
                throw new ArgumentException("Die Datei ist leider nicht vorhanden");
            }
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
                //Mögliche Lösung driverBusinessService initialsiieren!!!
                //Benötigt, um den Namen zu erhalten
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
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                carpoolDataService.DeleteAllCarpools();
            }
            else
            {
                throw new Exception("Diese Datei existiert nicht oder wurde bereits gelöscht.");
            }
        }
        public void ConnectionToDeleteSpecificCarpool(int Id)
        {
            if (File.Exists("C:\\Projects001\\FahrgemeinschaftProject\\Carpool.csv"))
            {
                carpoolDataService.DeleteSpecificCarpool(Id);
            }
            else
            {
                throw new Exception("Diese Datei exitiert leider nicht");
            }
        }

    }
}
