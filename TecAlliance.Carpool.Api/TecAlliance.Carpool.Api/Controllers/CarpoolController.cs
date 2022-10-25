using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Buisness.Services;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarpoolController : ControllerBase
    {
        CarpoolBusinessService carpoolBusinessService;
        public CarpoolController()
        {
            carpoolBusinessService = new CarpoolBusinessService();
        }
        /// <summary>
        /// You can create a new individual Carpool
        /// </summary>
        /// <param name="carpoolDtos"></param>
        /// <param name="userId">is equivalent to your personal Id is needed to Add yourself to the carpool</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CarpoolDto>> Post(CarpoolDto carpoolDtos, int userId)
        {
            return carpoolBusinessService.CreateNewCarpool(carpoolDtos,userId);
        }
        /// <summary>
        /// Function to find any Carpool due to their individual Id
        /// </summary>
        /// <param name="Id">is equivalent to a uniqe Id which is needed to identify each Carpool</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult<CarpoolDto>> GetById(int Id)
        {
            return carpoolBusinessService.GetSpecificCarpool(Id);
        }
        /// <summary>
        /// Function to display all Carpools ever created
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CarpoolDto>>> Get()
        {
            return carpoolBusinessService.GetAllCarpools();
        }
        /// <summary>
        /// Function to delete every Carpool
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<CarpoolDto>> Delete()
        {
            carpoolBusinessService.ConnectionToDeleteAllcarpools();
            return StatusCode(200, "Die Datei und somit alle Fahrgemeinschaften wurden erfolgreich gelöscht.");
        }
        /// <summary>
        /// Function to delete an Carpool based on its unige Id
        /// </summary>
        /// <param name="Id">is equivalent to a uniqe Id which is needed to identify the Carpool</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<ActionResult<CarpoolDto>> DeleteById(int Id)
        {
            carpoolBusinessService.ConnectionToDeleteSpecificCarpool(Id);
            return StatusCode(200, $"Das Carpool mit der Id: {Id} wurde erfolgreich gelöscht");
        }
        /// <summary>
        /// Function to join a Carpool
        /// </summary>
        /// <param name="carpoolId">is needed to identify the carpool you want to join</param>
        /// <param name="userId">is equivalent to your uniqe Id, is needed in order to add you to the Carpool</param>
        /// <returns></returns>
        [HttpPut("join")]
        public async Task<ActionResult<CarpoolDto>> PutByIdJoin(int carpoolId, int userId)
        {
            carpoolBusinessService.ConnectionToAddUserToCarpool(carpoolId, userId);
            return StatusCode(200, $"Zu dem Carpool mit der Id: {carpoolId}, wurde der User mit der Id: {userId} erfolgreich hinzugefügt.");
        }
        /// <summary>
        /// Function to leave a Carpool
        /// </summary>
        /// <param name="carpoolId">is needed to identify the carpool you want to leave</param>
        /// <param name="userId">is equivalent to your uniqe Id, is needed in order to remove  you from the Carpool</param>
        /// <returns></returns>
        [HttpPut("leave")]
        public  async Task<ActionResult<CarpoolDto>> PutByIdLeave(int carpoolId, int userId)
        {
            carpoolBusinessService.ConnectionToLeaveCarpool(carpoolId, userId);
            return StatusCode(200, $"Zu dem Carpool mit der Id: {carpoolId}, wurde der User mit der Id: {userId} erfolgreich entefernt.");
        }
        /// <summary>
        /// Function to change the Carpoolname
        /// </summary>
        /// <param name="carpoolName"></param>
        /// <param name="carpoolId">is needed to identify the carpool you want to update</param>
        /// <returns></returns>
        [HttpPut("Change CarpoolName")]
        public async Task<ActionResult<CarpoolDto>> PutChangeCarpoolName(string carpoolName, int carpoolId)
        {
          var result = carpoolBusinessService.ConnectionToChangeCarpoolName(carpoolName,carpoolId);
            if (result == null)
            {
                return StatusCode(200, "Carpoolname wurde nicht geändert.");
            }
            return result;
        }


    }
}
