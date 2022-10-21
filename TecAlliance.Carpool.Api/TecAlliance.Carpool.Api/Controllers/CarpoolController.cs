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
        public async Task<ActionResult<CarpoolDto>> Post(CarpoolDto carpoolDtos)
        {
            return carpoolBusinessService.CreateNewCarpool(carpoolDtos);
            
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

    }
}
