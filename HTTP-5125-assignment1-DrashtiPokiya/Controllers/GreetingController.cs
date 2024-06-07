using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment1.Controllers
{
    public class GreetingController : ApiController
    {
        /// <summary>
        /// Returns the string "Hello World!".
        /// </summary>
        /// <returns>"Hello World!"</returns>
        /// <example>POST api/Greeting -> "Hello World!"</example>
        [HttpGet]
        [Route("api/Greeting")]
        public String Greeting()
        {
            return "Hello World!";
        }

        /// <summary>
        /// Returns the string "Greetings to {id} people!" where id is an integer value.
        /// </summary>
        /// <param name="id">The number of people to greet.</param>
        /// <returns>The greeting string.</returns>
        /// <example>GET api/Greeting/6 -> "Greetings to 6 people!"</example>
        /// <example>GET api/Greeting/3 -> "Greetings to 3 people!"</example>
        /// <example>GET api/Greeting/0 -> "Greetings to 0 people!"</example>
        [HttpGet]
        [Route("api/Greeting/{id}")]
        public String GetGreeting(int id)
        {
            return $"Greetings to {id} people!";
        }
    }
}
