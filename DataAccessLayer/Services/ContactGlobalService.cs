using DataAccessLayer.Data;
using DataAccessLayer.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class ContactGlobalService : IContactGlobalRepo
    {
        private HttpClient _client;
        public ContactGlobalService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:31101/api/");
        }

        public void Delete(int contactId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage message = _client.DeleteAsync("contact/" + contactId).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }

        public IEnumerable<ContactGlobal> Get(int userID, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (HttpResponseMessage message = _client.GetAsync("contact/userContact/").Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();

                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<ContactGlobal>>(json);
            }
        }

        public ContactGlobal GetById(int contactId)
        {
            using (HttpResponseMessage message = _client.GetAsync("contact/contactDetail/"+contactId).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();

                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ContactGlobal>(json);
            }
        }

        public void Insert(ContactGlobal contact, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string jsonBody = JsonConvert.SerializeObject(contact);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = _client.PostAsync("contact/", content).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }

        public void Update(int id, ContactGlobal contact, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string jsonBody = JsonConvert.SerializeObject(contact);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = _client.PutAsync("contact/", content).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }
    }
}
