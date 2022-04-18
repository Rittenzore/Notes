using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Notes.Models;

namespace Notes.Helpers
{
    class HttpRequest
    {
        public static async Task<Response> MakePostRequest(string url_extension, List<KeyValuePair<string, string>> data)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://127.0.0.1:8000");

            HttpContent content = new FormUrlEncodedContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var result = await client.PostAsync(url_extension, content);
            try
            {
                string raw_result;
                JObject json;

                raw_result = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    json = JObject.Parse(raw_result);
                    if ((bool)json["success"])
                    {
                        Console.WriteLine("success =======");
                        return new Response(true, json["data"]);
                    }
                    else
                    {
                        Console.WriteLine("error =======");
                        return new Response(false, json["error"]);
                    }
                }
                else
                {
                    Console.WriteLine("Response failed =======");
                    return new Response(false);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Can't read response: " + e);
            };
        }

        public static async Task<Response> MakeGetRequest(string url_extension)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://127.0.0.1:8000");

            var result = await client.GetAsync(url_extension);
            try
            {
                string raw_result;
                JObject json;

                raw_result = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    json = JObject.Parse(raw_result);
                    if ((bool)json["success"])
                    {
                        Console.WriteLine("success =======");
                        return new Response(true, json["data"]);
                    }
                    else
                    {
                        Console.WriteLine("error =======");
                        return new Response(false, json["error"]);
                    }
                }
                else
                {
                    Console.WriteLine("Response failed =======");
                    return new Response(false);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Can't read response: " + e);
            };
        }
    }
}
