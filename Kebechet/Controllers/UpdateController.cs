using System;
using Kebechet.Logic;
using Kebechet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kebechet.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {        
        // Constructors

        public UpdateController(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        // Fields

        private readonly IUpdateService _updateService;

        // Methods

        [HttpGet]
        public ActionResult UpdateHostViaGet([FromQuery]UpdateHostMessage model)
        {
            return UpdateHostViaPost(model);
        }

        [HttpPost]
        public ActionResult UpdateHostViaPost(UpdateHostMessage model)
        {
            var result = _updateService.UpdateHost(model.Host, model.IpAddress);

            switch (result)
            {
                case UpdateResult.DnsZoneNotFound:
                case UpdateResult.RecordSetNotFound:
                    return BadRequest("invalid host data");
                case UpdateResult.Success:
                    return Ok();
                default:
                    return StatusCode(500, "internal error occured");
            }            
        }
    }
}