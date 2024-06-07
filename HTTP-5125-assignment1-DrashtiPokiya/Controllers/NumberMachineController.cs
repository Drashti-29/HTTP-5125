using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment1.Controllers
{
    public class NumberMachineController : ApiController
    {
        // GET: localhost/api/NumberMachine/{id}
        // GET localhost/api/NumberMachine/10
        // GET localhost/api/NumberMachine/-5
        // GET localhost/api/NumberMachine/30
        // Return type: String
        public String GetNumberMachine(int id)
        {
            int additionResult = id + 10;        // Example addition operation
            int subtractionResult = id - 5;      // Example subtraction operation
            int multiplicationResult = id * 3;   // Example multiplication operation

            // Division operation with handling division by zero
            int divisionResult;
            if (id != 0)
            {
                divisionResult = 100 / id;
            }
            else
            {
                divisionResult = 0; // Handle division by zero as needed
            }

            // Return results as strig with all action and their output
            return $"Addition: {additionResult}, Multiplication: {multiplicationResult}, Subtraction: {subtractionResult}, Division: {divisionResult}";

        }
    }
}