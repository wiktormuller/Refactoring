using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refactoring.Web.Services.Helpers
{
    public class BusinessHelper
    {
        public static string Barbershop => "Barbershop";
        public static string Bakery => "Bakery";
        public static string ShoeStore => "ShoeStore";
        public static string PizzaPlace => "Pizza place";
        public static string Diner => "Diner";
        public static string AutoRepair => "Auto repair";
        public static string Pharmacy => "Pharmacy";
        public static string Grocery => "Grocery";

        public static HashSet<string> AllBusinesses => new HashSet<string>
        {
             "Barbershop", "Bakery", "Shoe Store", "Pizza Place", "Diner", "Auto Repair", "Pharmacy", "Grocery"
        };
    }
}
