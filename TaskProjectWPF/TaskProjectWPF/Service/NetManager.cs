using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskProjectWPF.Service
{
    public static class NetManager
    {
        public static string URL = "http://localhost:61998/";
        public static HttpClient httpClient = new HttpClient();

        public static async Task<HttpResponseMessage> GetResponse(string path)
        {
            var response = await httpClient.GetAsync(URL+path);
            return response;
        }
        public static async Task<T> ParseData<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        public static async Task<HttpResponseMessage> PostData<T>(T data,string path)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var response = await httpClient.PostAsync(URL + path, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return response;
             
        }
        public static async Task<HttpResponseMessage> PutData<T>(T data, string path)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var response = await httpClient.PutAsync(URL + path, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return response;
        }
        public static async Task<HttpResponseMessage> DeletData( string path)
        {
            
            var response = await httpClient.DeleteAsync(URL + path);
            return response;
        }
    }
}
