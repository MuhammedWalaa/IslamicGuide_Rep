using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace IslamicGuide.Services.Utilities
{
    public class HttpService
    {
        //public async Task<ResponseResult> SendHttpGetRequest(string path)
        //{
        //    HttpClient _httpClient = new HttpClient();
        //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = await _httpClient.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        //string body = await response.Content.ReadAsStringAsync();
        //        //ResponseResult response = new JavaScriptSerializer().Deserialize<Data.Data>(body);
        //        //return new ResponseResult() { IsSuccess = true, data = new JavaScriptSerializer().Deserialize<Data.Data>(body) };
        //    }
        //    else
        //    {
        //        return new ResponseResult() { IsSuccess = false };
        //    }
        //}
        public static string GetWdcToken(string url, string token, string method)
        {
            try
            {

                WebClient client = new WebClient()
                {
                    Encoding = Encoding.UTF8,

                };
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                if (!string.IsNullOrEmpty(token))
                {
                    client.Headers.Add("Authorization", "Bearer " + token);
                }

                string jsonData = string.Empty;
                try
                {
                    jsonData = client.UploadString(url, method);
                }
                catch (WebException ex)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(jsonData))
                {
                    return null;
                }

                return jsonData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static T DownloadJsonData<T>(string url, string token) where T : new()
        {
            using (WebClient w = new WebClient() { Encoding = Encoding.UTF8 })
            {
                w.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string code = "";
                string message = "";

                string jsonData = string.Empty;
                if (!string.IsNullOrEmpty(token))
                {
                    w.Headers.Add("Authorization", "Bearer " + token);
                }

                try
                {
                    jsonData = w.DownloadString(url);
                }
                catch (WebException e)
                {

                    if (e.Message.Contains("(400)"))
                    {
                        code = "400";
                        // message = "No wallet Exsit";
                    }
                    if (e.Message.Contains("(500)"))
                    {
                        code = "500";
                        // message = "General Error";
                    }
                    if (e.Message.Contains("(404)"))
                    {
                        code = "404";
                        //message = "Not Found";
                    }
                }

                if (string.IsNullOrEmpty(jsonData))
                {
                    jsonData = "{'Code':" + code + "}";
                    jsonData = jsonData.Replace("[", string.Empty).Replace("]", string.Empty);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }

                jsonData = jsonData.Replace("[", string.Empty).Replace("]", string.Empty);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
        }
    }
}
