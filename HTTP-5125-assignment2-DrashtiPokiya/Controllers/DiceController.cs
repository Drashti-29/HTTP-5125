using System;
using System.Web.Http;

namespace assignment2.Controllers
{
    public class DiceController : ApiController
    {
        [HttpGet]
        [Route("api/J2/DiceGame/{m}/{n}")]
        public String GetWaysToRoll10(int m, int n)
        {
            if ( m <= 0 || m > 12 || n > 12 || n <= 0)
            {
                return "The number of sides must be positive and less than 13 integers.";
            }

            int ways = 0;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (i + j == 10)
                    {
                        ways++;
                    }
                }
            }

            string result = ways == 1
                ? $"There is {ways} way to get the sum 10."
                : $"There are {ways} ways to get the sum 10.";

            return result;
        }
    }
}
