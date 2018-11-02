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
        Task<Token> holeToken();

        Task<List<Thema>> GetThemen();


        Task<List<Frage>> GetFragen();

        Task<Boolean> isAlive();

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
        }


        public async Task<List<Frage>> GetFragen()
        {
            if (token==null)
            {
                this.token = await holeToken();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/frages");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            List<Frage> fragen = JsonConvert.DeserializeObject<List<Frage>>(json);
            foreach (Frage f in fragen)
            {
                if (f.thema!=null)
                {
                    f.thema_id = f.thema.id;
                }                
            }


                // Da Textantworten nicht mitgeliefert werden, werden diese zugeladen. 
                request = new HttpRequestMessage(HttpMethod.Get, "/api/text-antworts");
            response = httpClient.SendAsync(request).Result;
            json = response.Content.ReadAsStringAsync().Result;

            List<Textantwort> antworten = JsonConvert.DeserializeObject<List<Textantwort>>(json);


            Dictionary<long, List<Textantwort>> antwortDict = new Dictionary<long, List<Textantwort>>();
            foreach (Textantwort a in antworten)
            {
                if (a.frage!=null)
                {
                    // Muss sein, damit lokale DB die richtige ID erhält.
                    a.frage_id = a.frage.id;
                }


                if (!antwortDict.ContainsKey(a.frage_id))
                {
                    antwortDict.Add(a.frage_id, new List<Textantwort>());
                }
                List<Textantwort> list = antwortDict[a.frage_id];
                list.Add(a);
            }

            foreach (Frage f in fragen)
            {
                if (antwortDict.ContainsKey(f.id))
                {
                    f.antworten = antwortDict[f.id];
                }
            }           
          
            // Muss noch in lokale DB gespeichert werden.....


            return fragen;

        }

        public async Task<List<Thema>> GetThemen()
        {
            if (token == null)
            {            
                this.token = await holeToken();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/themas");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            List<Thema> themen = JsonConvert.DeserializeObject<List<Thema>>(json);

            return themen;

        }

        public async Task<Token> holeToken()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/authenticate");
            request.Content = new StringContent("{\"password\":\"" + MCAPP_PROPERTIES.SERVER_PASSWORD + "\",\"username\":\"" + MCAPP_PROPERTIES.SERVER_USER + "\", \"rememberMe\":true}",
                                    Encoding.UTF8,
                                    "application/json");//CONTENT-TYPE header            

            try
            {
                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Token>(json);
            }
            catch(Exception e)
            {
                throw new MCAPPWebserviceException("Webservice fehler", e);
            }
        }

        public async Task<bool> isAlive()
        {
            HttpClient testhttpclient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, MCAPP_PROPERTIES.SERVER_BASE_URL+"/api");
            testhttpclient.Timeout = TimeSpan.FromSeconds(2);

            try
            {
                var response = await testhttpclient.SendAsync(request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }


}
