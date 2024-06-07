using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment1.Controllers
{
    public class AddTenController : ApiController
    {
        /// <summary>
        /// Adds ten to the provided integer.
        /// </summary>
        /// <param name="id">The integer to add ten to.</param>
        /// <returns>The integer value increased by ten.</returns>
        /// GET api/AddTen/21 -> 31
        /// GET api/AddTen/0 -> 10
        /// GET api/AddTen/-9 -> 1
        /// 
        [HttpGet]
        [Route("api/AddTen/{id}")]
        public int AddTen(int id)
        {
            return id + 10;
        }
    }
}
