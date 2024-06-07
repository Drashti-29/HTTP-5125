using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment1.Controllers
{
    public class SquareController : ApiController
    {
        /// <summary>
        /// Returns the square of the provided integer.
        /// </summary>
        /// <param name="id">The integer to be squared.</param>
        /// <returns>The squared value of the integer.</returns>
        /// <example>GET api/Square/2 -> 4</example>
        /// <example>GET api/Square/-2 -> 4</example>
        /// <example>GET api/Square/10 -> 100</example>

        [HttpGet]
        [Route("api/Square/{id}")]
        public int Square(int id)
        {
            return id * id;
        }
    }
}
