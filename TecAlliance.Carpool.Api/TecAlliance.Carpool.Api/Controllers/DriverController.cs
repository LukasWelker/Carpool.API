using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Buisness.Services;
namespace T_ecAllianceCarpoolAPI.Controllers
{
    [ApiController]
    public class DriverController : ControllerBase
    {
        DriverBusinessService driverBusinessService;
        public DriverController()
        {
             driverBusinessService = new DriverBusinessService();
        }
        [HttpGet]
        [Route("api/CarPoolApi/GetDrivers")]
        public ActionResult<string> GetDrivers()
        {
            return "foobar";
        }


        [HttpPost]
        [Route("api/CarPoolApi/PostDriver")]
        public async Task<ActionResult<DriverDto>> Post(DriverDto driverDto)
        {
            driverBusinessService.AddDriver(driverDto);
            return NoContent();
        }
    }
}