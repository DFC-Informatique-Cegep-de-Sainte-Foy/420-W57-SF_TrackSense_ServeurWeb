using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrackSense.API.Entities;
using TrackSense.API.Models;
using TrackSense.API.Services.ServiceRides;
using TrackSense.API.Services.ServiceUsers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannedRidesController : ControllerBase
    {
        private ManipulationRides m_ridesManipulation;
        private ManipulationUsers m_usersManipulation;

        public PlannedRidesController(ManipulationRides p_manipulationRides, ManipulationUsers p_manipulationUsers)
        {
            if (p_manipulationRides == null)
            {
                throw new ArgumentNullException("ManipulationRides ne doit pas etre null - ctor PlannedRideController");
            }
            if (p_manipulationUsers == null)
            {
                throw new ArgumentNullException("ManipulationUsers ne doit pas etre null - ctor PlannedRideController");
            }

            this.m_ridesManipulation = p_manipulationRides;
            this.m_usersManipulation = p_manipulationUsers;
        }

        // GET api/<PlannedRidesController>/5
        [HttpGet("{p_plannedRideId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult<PlannedRideModel> Get(string p_plannedRideId)
        {
            ActionResult<PlannedRideModel> response;

            if (!this.CheckUserToken())
            {
                response = Unauthorized();
            }
            else if (p_plannedRideId == string.Empty)
            {
                response = BadRequest();
            }
            else
            {
                try
                {
                    PlannedRide? plannedRide = this.m_ridesManipulation.GetPlannedRideById(p_plannedRideId);

                    response = plannedRide != null 
                        ? Ok(new PlannedRideModel(plannedRide)) 
                        : NoContent();
                }
                catch (Exception)
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }

            return response;
        }

        // POST api/<CompletedRidesController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Post([FromBody] PlannedRideModel p_plannedRideModel)
        {
            ActionResult response;

            if (!this.CheckUserToken())
            {
                response = Unauthorized();
            }
            else if (p_plannedRideModel == null)
            {
                response = BadRequest();
            }
            else
            {
                try
                {
                    PlannedRide plannedRide = p_plannedRideModel.ToEntity();
                    this.m_ridesManipulation.AddPlannedRide(plannedRide);
                    
                    string url = $"api/plannedRides/{plannedRide.PlannedRideId}";
                    var NewRide = this.m_ridesManipulation.GetPlannedRideById(plannedRide.PlannedRideId)!;
                    response = Created(url, new PlannedRideModel(NewRide));
                }
                catch (Exception)
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }

            return response;
        }

        // PUT api/<CompletedRidesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompletedRidesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        private bool CheckUserToken()
        {
            string? token = this.GetUserToken();


            ///////////////////////////////////////////////////////////////////
            return true;
            ///////////////////////////////////////////////////////////////////
            return !String.IsNullOrWhiteSpace(token) && this.m_usersManipulation.CheckUserToken(token);
        }

        private string? GetUserToken()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            string[] tokenParts = token.Split(' ');

            if (tokenParts.Length != 2 || !string.Equals(tokenParts[0], "Bearer", StringComparison.OrdinalIgnoreCase))
            {
                token = null;
            }
            else
            {
                token = tokenParts[1];
            }

            return token;
        }
    }
}
