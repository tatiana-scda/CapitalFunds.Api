using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalFunds.Api.Controllers
{
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public class HealthController : ControllerBase
    {

        /// <summary>
        /// Tests if connection is active
        /// </summary>
        /// <returns></returns>
        [HttpGet("Health")]
        public IActionResult Health()
        {
            return Ok("It's working!");
        }
    }
}