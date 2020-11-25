using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refactoring.Web.Services.Helpers;
using Refactoring.Web.Services.Interfaces;

namespace Refactoring.Web.Services
{
    public  class ChamberOfCommerceApi : IChamberOfCommerceAPI
    {
        private readonly IConfiguration _config;

        public ChamberOfCommerceApi(IConfiguration config)
        {
            _config = config;
        }

        public  async Task<DataResult> GetFor(string district) {
            using var client = new HttpClient();

            var absoulteUrl = BuildUrlForDistrict(district);
            var request = new HttpRequestMessage(HttpMethod.Get, absoulteUrl);

            var response = client.SendAsync(request);
            var data = await response.Result.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DataResult>(data);
            return result;
        }

        private Uri BuildUrlForDistrict(string district)
        {
            var districtId = DistrictHelper.GetDistrictNumber(district);
            var basePhotoUrl = new Uri(_config.GetValue<string>("BasePhotoUrl"));
            return new Uri(basePhotoUrl, districtId.ToString());
        }
    }

    public struct DataResult {
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
    }
}