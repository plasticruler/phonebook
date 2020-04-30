using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure.Messager
{
    public class Messager : IMessager
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _config;

        public Messager(IConfiguration configuaration)
        {
            _config = configuaration;
            SetHeaders(client);
        }

        public async Task<R> Get<R>(string url, string jwtToken=null)
        {            
            R result = default(R);
            if (!string.IsNullOrWhiteSpace(jwtToken))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<R>(str);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return result;
        }

        public async Task<R> Post<T, R>(string url, T payload, string jwtToken=null, params string[] parameters)
        {
            R result = default(R);
            var json = JsonConvert.SerializeObject(payload);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            if (!string.IsNullOrWhiteSpace(jwtToken))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<R>(str);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return result;
        }

        private void SetHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
            client.DefaultRequestHeaders.Add("User-Agent", "PhoneBook.API Client v1");
        }
    }
}
