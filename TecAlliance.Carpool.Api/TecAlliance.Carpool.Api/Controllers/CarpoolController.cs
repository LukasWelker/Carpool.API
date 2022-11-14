using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Business.Interfaces;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarpoolController : ControllerBase
    {
        ICarpoolBusinessService carpoolBusinessService;
        public CarpoolController(ICarpoolBusinessService carpoolBusinessService)
        {
            this.carpoolBusinessService = carpoolBusinessService;
        }
        //XML Comments
        /// <summary>
        /// You can create a new individual Carpool
        /// </summary>
        /// <param name="carpoolDtos"></param>
        /// <param name="userId">is equivalent to your personal Id </param>
        /// <returns>A newly created Carpool</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarpoolDto>> Post(CarpoolDto carpoolDtos, int userId)
        {
            return carpoolBusinessService.CreateNewCarpool(carpoolDtos,userId);
        }
        /// <summary>
        /// Function to find any Carpool due to their individual Id
        /// </summary>
        /// <param name="Id">is a uniqe Id which is needed to identify each Carpool</param>
        /// <returns>A Carpool based on its Id</returns>
        /// <response code="200">Returns the Carpool based on its uniqe Id</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolDto>> GetById(int Id)
        {
            return carpoolBusinessService.GetSpecificCarpool(Id);
        }
        /// <summary>
        /// Function to display all Carpools ever created
        /// </summary>
        /// <returns>All Carpools ever created</returns>
        /// <response code="200">Returns all Carpools ever created</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CarpoolDto>>> Get()
        {
            return carpoolBusinessService.GetAllCarpools();
        }
        /// <summary>
        /// Function to delete every Carpool
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Deletes every Carpool ever created</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <response code="200">Deletes one Carpool based on its Id</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <response code="204">User entered the Carpool</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpPut("join")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        /// <response code="204">User left the Carpool</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpPut("leave")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        /// <response code="204">Carpoolname was changed</response>
        /// <response code="404">If the Carpool/File does not exists</response>
        [HttpPut("Change CarpoolName")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
