using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Buisness.Models;
using TecAlliance.Carpool.Buisness.Services;
namespace T_ecAllianceCarpoolAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : ControllerBase
    {
        PassengerBusinessService driverBusinessService;
        public PassengerController()
        {
             driverBusinessService = new PassengerBusinessService();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<PassengerDto>> GetById(int Id)
        {
            return driverBusinessService.GetSpecificPassenger(Id);
        }
        [HttpGet]
        public async Task<ActionResult<List<PassengerDto>>> Get()
        {
            return driverBusinessService.GetAllPassengers();
        }

        [HttpPost]
        public async Task<ActionResult<PassengerDto>> Post(PassengerDto driverDto)
        {
            return driverBusinessService.AddDriver(driverDto);
            //CreatedAtActtion 
        }
        [HttpDelete]
        public async Task<ActionResult<PassengerDto>> Delete()
        {
            driverBusinessService.ConnectionToDeletePassengers();
            return StatusCode(200, "Es wurden erfolgreich alle Passenger gelöscht");
        }
    }
}