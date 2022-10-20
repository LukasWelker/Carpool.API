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
        public DriverBusinessService driverBusinessService;
        public CarpoolBusinessService()
        {
            carpoolDataService = new CarpoolDataService();
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
            var foo = new List<int>();
            foo.Add(carpoolDtos.CarpoolId);
            var convertedCarpools = new Carpools(carpoolDtos.CarpoolId, carpoolDtos.CarpoolName, carpoolDtos.Start,
                carpoolDtos.Destination, carpoolDtos.Time, carpoolDtos.Seatcount, carpoolDtos.ExistenceOfDriver, foo);
            return convertedCarpools;
        }
        public CarpoolDto ConvertCarpoolToCarpoolDto(Carpools carpools)
        {
            List<PassengerInfoDto> baa = new List<PassengerInfoDto>();

            foreach (var id in carpools.PassengerIds)
            {
                //Schafft Probleme, da ich darauf nicht zugreifen kann
                //wird benötigt um die Id und den Namen zu bekommen
                //Mögliche Lösung Methode schreiben, die den PassengerDto returnt!!!
                var passenger = driverBusinessService.GetSpecificPassenger(id);
                //var passenger = driverBusinessService.GetSpecificPassengerDetail(id);
                //var passenger = driverBusinessService.GetSpecificPassengerInfoDot(id);

                var passengerDto = new PassengerInfoDto() { PassengerId = passenger.Id, PassengerName = $"{passenger.FirstName} {passenger.LastName}" };

                baa.Add(passengerDto);
            }

            var convertedCarpoolDto = new CarpoolDto(carpools.CarpoolId, carpools.CarpoolName, carpools.Start,
                carpools.Destination, carpools.Time, carpools.Seatcount, carpools.ExistenceOfDriver, baa);
            //Liste aus Objekten muss ich hinzufügen diese:  List<PassengerInfo> passengerInfos
            return convertedCarpoolDto;
        }
    }
}
