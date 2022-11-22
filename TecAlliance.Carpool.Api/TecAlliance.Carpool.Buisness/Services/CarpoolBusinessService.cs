using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TecAlliance.Carpool.Business.Interfaces;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Interfaces;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business.Services
{
    public class CarpoolBusinessService :ICarpoolBusinessService
    {
        ICarpoolDataService carpoolDataService;
        // reggex durchläfut eine Liste und checkt ob eine bedinung aus chars erfüllt ist
        public static Regex regex = new Regex("^\\d+$");
        //Variablen Deklaration
        public IPassengerBusinessService _passengerBusinessService;
        public CarpoolBusinessService(IPassengerBusinessService passengerBusinessService, ICarpoolDataService carpoolDataService)
        {
            //Hier findet die Initialisierung statt
            this.carpoolDataService = carpoolDataService;
            _passengerBusinessService = passengerBusinessService;
        }
        /// <summary>
        /// Connection between Data and Controller level, needed for the creation of a new Carpool
        /// </summary>
        /// <param name="carpoolDtos"></param>
        /// <param name="userId">uniqe Id to add a User to the newly created carpool </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CarpoolDto CreateNewCarpool(CarpoolDto carpoolDtos, int userId)
        {
            //basic Errorhandling
            if (string.IsNullOrEmpty(carpoolDtos.CarpoolName) || string.IsNullOrEmpty(carpoolDtos.Start)
                || string.IsNullOrEmpty(carpoolDtos.Destination) || string.IsNullOrEmpty(carpoolDtos.Time))
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
                CarpoolEntity carpools = ConvertCarpoolDtosToCarpools(carpoolDtos);
                carpoolDataService.CreateNewCarpool(carpools, userId);
                List<int> passengerIds = carpoolDataService.GetPassengerIds(carpoolDtos.CarpoolId);
                CarpoolDto carpool = ConvertCarpoolToCarpoolDto(carpools, passengerIds);
                return carpool;
            }
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to find a carpools based on its Id
        /// </summary>
        /// <param name="Id">uniqe Id to identify a carpool</param>
        /// <returns></returns>
        public CarpoolDto GetSpecificCarpool(int Id)
        {
            CarpoolEntity carpool = carpoolDataService.SearchForSpecificCarpoolInCsvAndReadIt(Id);
            List<int> passengerIds = carpoolDataService.GetPassengerIds(Id);
            CarpoolDto carpooldto = ConvertCarpoolToCarpoolDto(carpool, passengerIds);
            return carpooldto;
        }

        /// <summary>
        ///  Connection between Data and Controller level, needed to find all carpools ever created
        /// </summary>
        /// <returns> All Carpools ever created</returns>
        public List<CarpoolDto> GetAllCarpools()
        {
            List<CarpoolEntity> everyCarpool = carpoolDataService.DisplayEveryCarpool();
            //Ich initialisiere eine Liste aus Dto-Objekten 
            List<CarpoolDto> allCarpools = new List<CarpoolDto>();
            foreach (CarpoolEntity carpool in everyCarpool)
            {
                List<int> passengerIds = carpoolDataService.GetPassengerIds(carpool.CarpoolId);
                CarpoolDto carpoolDto = ConvertCarpoolToCarpoolDto(carpool, passengerIds);
                allCarpools.Add(carpoolDto);
            }
            return allCarpools;
        }

        private CarpoolEntity ConvertCarpoolDtosToCarpools(CarpoolDto carpoolDtos)
        {
            //Erstellung eine rneure intListe, wird benötigt für die Befüllung des Carpoolobjekts
            var intListOfIds = new List<int>();
            intListOfIds.Add(carpoolDtos.CarpoolId);
            //creating a new Carpools object with the following properties and save the newly created object in a variable from type Carpools
            CarpoolEntity convertedCarpools = new CarpoolEntity(carpoolDtos.CarpoolId, carpoolDtos.CarpoolName, carpoolDtos.Start,
                carpoolDtos.Destination, carpoolDtos.Time, carpoolDtos.Seatcount, Convert.ToInt32(carpoolDtos.ExistenceOfDriver), intListOfIds);
            return convertedCarpools;
        }



        private CarpoolDto ConvertCarpoolToCarpoolDto(CarpoolEntity carpools, List<int> passengerIds)
        {
            //TODO-
            //Erstellung einer neuen PassengerInfoDtoListeaus objekten
            List<PassengerInfoDto> newPassengerInfoList = new List<PassengerInfoDto>();
            //für jede Id in der Liste aus PassengerIds
            foreach (var carpoolId in passengerIds)
            {
                //Line is nedded  to get the Name of the Passenger then we create the new PassengerInfoobject and add each of this objects to a list in order to later build the carpoolDto object we need to return
                var passenger = _passengerBusinessService.GetSpecificPassenger(carpoolId);
                // baue das Objekt mithilfe der Id, bekomme ich übergeben und dem Namen aus GetSpecificPassenger
                var passengerDto = new PassengerInfoDto() { PassengerId = passenger.Id, PassengerName = $"{passenger.FirstName} {passenger.LastName}" };
                newPassengerInfoList.Add(passengerDto);
            }
            var convertedCarpoolDto = new CarpoolDto(carpools.CarpoolId, carpools.CarpoolName, carpools.Start,
                carpools.Destination, carpools.Time, carpools.Seatcount, Convert.ToInt32(carpools.ExistenceOfDriver), newPassengerInfoList);
            //Liste aus Objekten muss ich hinzufügen diese:  List<PassengerInfo> passengerInfos
            return convertedCarpoolDto;
        }
        /// <summary>
        /// Creates a new object PassengerDto and returns it
        /// </summary>
        /// <param name="passengerDto"></param>
        /// <returns> Returns a PassengerDto object</returns>
        public PassengerDto GetPassengerDto(PassengerDto passengerDto)
        {
            PassengerDto passenger = new PassengerDto();
            return passenger;
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to delete all carpools ever created
        /// </summary>
        public void ConnectionToDeleteAllcarpools()
        {
            carpoolDataService.DeleteAllCarpools();
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to delete one specific carpool
        /// </summary>
        /// <param name="carpoolId"> uniqe Id to identify the different carpools</param>
        public void ConnectionToDeleteSpecificCarpool(int carpoolId)
        {
            carpoolDataService.DeleteSpecificCarpool(carpoolId);
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to add a Passenger to a Carpools
        /// </summary>
        /// <param name="carpoolId">uniqe Id to identify the carpool where the user wants to join</param>
        /// <param name="userId">uniqe Id to identify and later add the user to the Carpool</param>
        public void ConnectionToAddUserToCarpool(int carpoolId, int userId)
        {
            carpoolDataService.AddUserToCarpool(carpoolId, userId);
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to change the name of a carpool
        /// </summary>
        /// <param name="carpoolName"> is equivalent to the new carpool name</param>
        /// <param name="carpoolId">uniqe Id to identify the carpool </param>
        /// <returns></returns>
        public CarpoolDto ConnectionToChangeCarpoolName(string carpoolName, int carpoolId)
        {
            CarpoolEntity carpool = carpoolDataService.ChangeCarpoolName(carpoolName, carpoolId);
            List<int> passengerIds = carpoolDataService.GetPassengerIds(carpoolId);
            CarpoolDto carpoolDto = ConvertCarpoolToCarpoolDto(carpool, passengerIds);
            return carpoolDto;
        }

        /// <summary>
        /// Connection between Data and Controller level, needed to remove a Passenger to a Carpools
        /// </summary>
        /// <param name="carpoolId">uniqe Id to identify the carpool which needs to be adapted</param>
        /// <param name="userId"> uniqe Id to identify the user who wants to leave</param>
        public void ConnectionToLeaveCarpool(int carpoolId, int userId)
        {
            carpoolDataService.LeaveCarpool(carpoolId, userId);
        }
        //value can be almost everything in this example it can be carpoolId etc. and the path is alos variable but in the given example it is  the Carpoolfile.
       
    }
}
