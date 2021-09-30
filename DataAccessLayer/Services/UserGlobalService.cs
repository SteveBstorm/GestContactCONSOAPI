using DataAccessLayer.Data;
using DataAccessLayer.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class UserGlobalService : IUserGlobalRepo
    {
        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public UserGlobal Login(string email, string passwd)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31101/api/");

            

            string jsonBody = JsonConvert.SerializeObject(new { email = email, password = passwd });
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = client.PostAsync("user/login", content).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();

                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UserGlobal>(json);
            }
        }

        public void Register(UserGlobal entity)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31101/api/");

            string jsonBody = JsonConvert.SerializeObject(entity);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using(HttpResponseMessage message = client.PostAsync("user/register", content).Result)
            {
                if (!message.IsSuccessStatusCode)
                    throw new HttpRequestException();
            }
        }
    }
}
