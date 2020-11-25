using System.Collections.Generic;

namespace Refactoring.Web.Services.Helpers
{
    public static class DistrictHelper
    {
        public static string Cambridge => "Cambridge";
        public static string Downtown => "Downtown";
        public static string County => "County";
        public static string Middleton => "Middleton";

        public static IEnumerable<string> AllDistricts => new List<string>
        {
            Cambridge, Downtown, County, Middleton
        };
    }
}
