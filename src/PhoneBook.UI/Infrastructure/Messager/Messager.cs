using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public R Get<R>(string url)
        {
            //client.GetAsync(url).Result.Content.
            return default(R);
        }

        public R Post<T, R>(string url, T payload, params string[] parameters)
        {
            throw new NotImplementedException();
        }
        private void SetHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );
            client.DefaultRequestHeaders.Add("User-Agent", "PhoneBook.API Client v1");
            client.DefaultRequestHeaders.Add("X-Api-Key", "112233");
        }
    }
}
