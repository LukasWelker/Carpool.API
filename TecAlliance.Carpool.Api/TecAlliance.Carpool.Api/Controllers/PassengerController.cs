using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Business.Interfaces;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : ControllerBase
    {
        IPassengerBusinessService passengerBusinessService;
        public PassengerController(IPassengerBusinessService passengerBusinessService)
        {
            this.passengerBusinessService = passengerBusinessService;
        }

        /// <summary>
        /// Creates an new Passengerprofile
        /// </summary>
        /// <param name="driverDto"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created Passenger</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PassengerDto>> Post(PassengerDto driverDto)
        {
            return passengerBusinessService.AddDriver(driverDto);
            //CreatedAtActtion 
        }
        /// <summary>
        ///  Function to find any Passenger based on their individual Id
        /// </summary>
        /// <param name="Id">is a uniqe Id which is needed to identify each Passenger</param>
        /// <returns></returns>
        /// <response code="200">Returns the Passenger based on its uniqe Id</response>
        /// <response code="404">If the Passenger/File does not exists</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PassengerDto>> GetById(int Id)
        {
            return passengerBusinessService.GetSpecificPassenger(Id);
        }
        /// <summary>
        /// Function to display all Passengers ever created
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all Passenger ever created</response>
        /// <response code="404">If the Passenger/File does not exists</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PassengerDto>>> Get()
        {
            return passengerBusinessService.GetAllPassengers();
        }
        /// <summary>
        /// Function to delete every Passenger
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Deletes every Passenger ever created</response>
        /// <response code="404">If the Passenger/File does not exists</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PassengerDto>> Delete()
        {
            passengerBusinessService.ConnectionToDeleteAllPassengers();
            return StatusCode(200, "Es wurden erfolgreich alle Passenger gelöscht");
        }
        /// <summary>
        /// Function to delete a Passenger based on its unige Id
        /// </summary>
        /// <param name="Id">is equivalent to a uniqe Id which is needed to identify the Passenger</param>
        /// <returns></returns>
        /// <response code="200">Deletes one Passenger based on its Id</response>
        /// <response code="404">If the Passenger/File does not exists</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PassengerDto>> DeleteById(int Id)
        {
            passengerBusinessService.ConnectionToDeleteSpecificPassenger(Id);
            return StatusCode(200, $"Es wurde erfolgreich der User mit der Id: {Id} gelöscht.");
        }
    }
}