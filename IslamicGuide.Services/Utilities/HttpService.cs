using IslamicGuide.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace IslamicGuide.Services.Utilities
{
    public class HttpService
    {
        public async Task<ResponseResult> SendHttpGetRequest(string path)
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = _httpClient.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                return new ResponseResult() { IsSuccess = true, data = new JavaScriptSerializer().Deserialize<Data.Data>(response.Content.ToString()) };
            }
            else
            {
                return new ResponseResult() { IsSuccess = false };
            }
        }
    }
}
