using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Repositories
{
    public interface IMCAPPWebRepositorie
    {
        void holeToken();

        List<Thema> GetThemen();

        List<Frage> GetFragen();

    }

    public class MCAPPWebRepositorie : IMCAPPWebRepositorie
    {
        readonly HttpClient httpClient = new HttpClient();

        Token token = null;

        public MCAPPWebRepositorie()
        {
            httpClient.DefaultRequestHeaders.Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            httpClient.BaseAddress = new Uri(MCAPP_PROPERTIES.SERVER_BASE_URL);
            holeToken();
        }


        public List<Frage> GetFragen()
        {
            if (token==null)
            {
                holeToken();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/frages");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            List<Frage> fragen = JsonConvert.DeserializeObject<List<Frage>>(json);

            return fragen;

        }

        public List<Thema> GetThemen()
        {
            if (token == null)
            {
                holeToken();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/themas");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            List<Thema> themen = JsonConvert.DeserializeObject<List<Thema>>(json);

            return themen;

        }

        public async void holeToken()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/authenticate");
            request.Content = new StringContent("{\"password\":\"" + MCAPP_PROPERTIES.SERVER_PASSWORD + "\",\"username\":\"" + MCAPP_PROPERTIES.SERVER_USER + "\", \"rememberMe\":true}",
                                    Encoding.UTF8,
                                    "application/json");//CONTENT-TYPE header

            httpClient.BaseAddress = new Uri(MCAPP_PROPERTIES.SERVER_BASE_URL);
            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            this.token = JsonConvert.DeserializeObject<Token>(json);
        }
    }


}
