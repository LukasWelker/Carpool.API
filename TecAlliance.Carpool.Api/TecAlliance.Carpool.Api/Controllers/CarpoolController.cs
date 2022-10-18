using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Buisness.Services;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    public class CarpoolController : ControllerBase
    {
        CarpoolBusinessService carpoolBusinessService;
        public CarpoolController()
        {
            carpoolBusinessService = new CarpoolBusinessService();
        }
        [HttpGet]
        [Route("api/CarPoolApi/PostCarpools")]
        public async Task<ActionResult<CarpoolDto>> Post(CarpoolDto carpoolDto)
        {

            return NoContent();
        }
    }
}
