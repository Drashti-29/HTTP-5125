using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace assignment2.Controllers
{
    public class DelivEDroidController : ApiController
    {
        [HttpPost]
        [Route("api/delivedroid/score")]
        public String CalculateScore([FromBody] ScoreInput input)
        {
            if (input == null)
            {
                return "Input data is missing.";
            }

            int P = input.PackagesDelivered;
            int C = input.CollisionsWithObstacles;

            // Calculate points
            int pointsFromPackages = P * 50;
            int pointsLostFromCollisions = C * 10;
            int bonusPoints = (P > C) ? 500 : 0;

            // Calculate final score
            int finalScore = pointsFromPackages - pointsLostFromCollisions + bonusPoints;

            // Return final score
            return finalScore.ToString();
        }

        public class ScoreInput
        {
            public int PackagesDelivered { get; set; }
            public int CollisionsWithObstacles { get; set; }
        }
    }
}
