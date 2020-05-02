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
        private readonly IConfiguration _config;

        public Messager(IConfiguration configuaration)
        {
            _config = configuaration;

        }

        public async Task<R> Get<R>(string url, string jwtToken = null)
        {
            R result = default(R);
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(handler))
                {
                    SetHeaders(client, jwtToken);
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
            }            
        }
        public async Task<R> Put<T, R>(string url, T payload, string jwtToken = null)
        {
            return await Process<T, R>("PUT", url, payload, jwtToken);            
        }
        
        public async Task<R> Post<T, R>(string url, T payload, string jwtToken = null)
        {
            return await Process<T, R>("POST", url, payload, jwtToken);            
        }
        public async Task<R> Process<T, R>(string verb, string url, T payload, string jwtToken = null){
                R result = default(R);
            var json = JsonConvert.SerializeObject(payload); 
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(handler))
                {
                    SetHeaders(client, jwtToken);

                    HttpResponseMessage response;
                    if (verb == "POST")
                    {
                        response = await client.PostAsync(url, stringContent);
                    }
                    else if(verb== "PUT"){
                        response = await client.PutAsync(url, stringContent);
                    }
                    else{
                        throw new Exception($"Unsupported verb '{verb}'.");
                    }
                     
                    
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
            }
        }
        private void SetHeaders(HttpClient client, string jwtToken = null)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            if (!string.IsNullOrWhiteSpace(jwtToken))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
            client.DefaultRequestHeaders.Add("User-Agent", "PhoneBook.API Client v1");
        }
    }
}
