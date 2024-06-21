using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace assignment2.Controllers
{
    public class MenuController : ApiController
    {
        private static readonly int[] BurgerCalories = { 0, 461, 431, 420, 0 };
        private static readonly int[] DrinkCalories = { 0, 130, 160, 118, 0 };
        private static readonly int[] SideCalories = { 0, 100, 57, 70, 0 };
        private static readonly int[] DessertCalories = { 0, 167, 266, 75, 0 };
        
        [HttpGet]
        [Route("api/Menu/{burger}/{drink}/{side}/{dessert}")]
        public string GetCalorieCount(int burger, int drink, int side, int dessert)
        {
            if (burger < 1 || burger > 4 || drink < 1 || drink > 4 || side < 1 || side > 4 || dessert < 1 || dessert > 4)
            {
                return "Invalid menu item index.";
            }

            int totalCalories = BurgerCalories[burger] + DrinkCalories[drink] + SideCalories[side] + DessertCalories[dessert];
            return $"Your total calorie count is {totalCalories}";
        }
    }
}
