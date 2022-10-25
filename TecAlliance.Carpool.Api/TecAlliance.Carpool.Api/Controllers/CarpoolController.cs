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
        [HttpPost]
        public async Task<ActionResult<CarpoolDto>> Post(CarpoolDto carpoolDtos, int userId)
        {
            return carpoolBusinessService.CreateNewCarpool(carpoolDtos,userId);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CarpoolDto>> GetById(int Id)
        {
            return carpoolBusinessService.GetSpecificCarpool(Id);
        }
        [HttpGet]
        public async Task<ActionResult<List<CarpoolDto>>> Get()
        {
            return carpoolBusinessService.GetAllCarpools();
        }
        [HttpDelete]
        public async Task<ActionResult<CarpoolDto>> Delete()
        {
            carpoolBusinessService.ConnectionToDeleteAllcarpools();
            return StatusCode(200, "Die Datei und somit alle Fahrgemeinschaften wurden erfolgreich gelöscht.");
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<CarpoolDto>> DeleteById(int Id)
        {
            carpoolBusinessService.ConnectionToDeleteSpecificCarpool(Id);
            return StatusCode(200, $"Das Carpool mit der Id: {Id} wurde erfolgreich gelöscht");
        }
        [HttpPut("join")]
        public async Task<ActionResult<CarpoolDto>> PutByIdJoin(int carpoolId, int userId)
        {
            carpoolBusinessService.ConnectionToAddUserToCarpool(carpoolId, userId);
            return StatusCode(200, $"Zu dem Carpool mit der Id: {carpoolId}, wurde der User mit der Id: {userId} erfolgreich hinzugefügt.");
        }
        [HttpPut("leave")]
        public  async Task<ActionResult<CarpoolDto>> PutByIdLeave(int carpoolId, int userId)
        {
            carpoolBusinessService.ConnectionToLeaveCarpool(carpoolId, userId);
            return StatusCode(200, $"Zu dem Carpool mit der Id: {carpoolId}, wurde der User mit der Id: {userId} erfolgreich entefernt.");
        }
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
