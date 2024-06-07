using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment1.Controllers
{
    public class HostingCostController : ApiController
    {
        // GET: localhost/api/HostingCost/{id}
        // Example usage: GET localhost/api/HostingCost/0
        // Return type: string arry
        public String[] GetHostingCost(int id)
        {
            // Constants
            const double fortnightCost = 5.50;  // Cost per fortnight in CAD
            const double hstRate = 0.13;        // HST rate (13%)

            // Calculate number of complete fortnights
            int numberOfFortnights = 1 + (id / 14);   // Integer division gives complete fortnights

            // Calculate total cost before tax
            double totalBeforeTax = numberOfFortnights * fortnightCost;

            // Calculate HST amount
            double hstAmount = totalBeforeTax * hstRate;

            // Calculate total cost including HST
            double totalCost = totalBeforeTax + hstAmount;

            // Format the response strings
            string response1 = $"{numberOfFortnights} fortnights at ${fortnightCost:F2}/FN = ${totalBeforeTax:F2} CAD";
            string response2 = $"HST {hstRate * 100}% = ${hstAmount:F2} CAD";
            string response3 = $"Total = ${totalCost:F2} CAD";

            // Return as string array
            return new string[] { response1, response2, response3 };
        }
    }
}
