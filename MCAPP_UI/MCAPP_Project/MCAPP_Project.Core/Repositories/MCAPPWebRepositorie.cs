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

        /*
         * Meldet Auswertung eines Quiz an Webanwendung.
         */
        Task<Boolean> sendQuizauswertung(Quiz quiz, List<Quiz_Frage> quizfragen);


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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/frages");
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
            request = new HttpRequestMessage(HttpMethod.Get, "api/text-antworts");
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

            // Da Textantworten nicht mitgeliefert werden, werden diese zugeladen. 
            request = new HttpRequestMessage(HttpMethod.Get, "api/bild-antworts");
            response = httpClient.SendAsync(request).Result;
            json = response.Content.ReadAsStringAsync().Result;

            // Neben Textantworten können auch Bildantworten geliefert werden:
            List<Bildantwort> bildAntworten = JsonConvert.DeserializeObject<List<Bildantwort>>(json);

            Dictionary<long, List<Bildantwort>> bildantwortDict = new Dictionary<long, List<Bildantwort>>();

            foreach (Bildantwort a in bildAntworten)
            {
                if (a.frage != null)
                {
                    // Muss sein, damit lokale DB die richtige ID erhält.
                    a.frage_id = a.frage.id;
                }


                if (!bildantwortDict.ContainsKey(a.frage_id))
                {
                    bildantwortDict.Add(a.frage_id, new List<Bildantwort>());
                }
                List<Bildantwort> list = bildantwortDict[a.frage_id];
                list.Add(a);
            }

            foreach (Frage f in fragen)
            {
                if (bildantwortDict.ContainsKey(f.id))
                {
                    f.bildantworten = bildantwortDict[f.id];
                }
            }

            // Problem - wenn es sowohl Text- als auch Bildantworten gibt, dann wird dies überschrieben!


            return fragen;

        }

        public async Task<List<Thema>> GetThemen()
        {
            if (token == null)
            {            
                this.token = await holeToken();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/themas");
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

        public async Task<bool> sendQuizauswertung(Quiz quiz, List<Quiz_Frage> quizfragen)
        {
            HttpClient testhttpclient = new HttpClient();
            if (token == null)
            {
                this.token = await holeToken();
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/quizzes");
            
            String datum = quiz.datum.ToString("yyyy-MM-dd");
            request.Content = new StringContent("{\"id\": null , \"datum\": \"" + datum + "\",\"quizFrageIDS´\": null}",
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.SendAsync(request);

            /*
             * Der Server hat andere IDs, als die lokale DB. Nach dem POST des Quiz-Objekts, liefert der 
             * WebService das erstellte Objekt zurück. Dieses enthält auch die ID auf dem Server. 
             * Dies wird für die Unterobjekte Quiz_Frage benötigt.
             */

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Quiz quizServer = JsonConvert.DeserializeObject<Quiz>(json);



            foreach(Quiz_Frage frage in quizfragen)
            {
                try
                {
                    request = new HttpRequestMessage(HttpMethod.Post, "api/quiz-frages");
                    frage.quizID = quizServer.id;
                    request.Content = new StringContent("{\"id\": null , \"richtig\": \"" + frage.richtig_beantwortet + "\",\"frage\": {\"id\":" + frage.frageID + "}, \"quiz\":{\"id\":" + quizServer.id + "}}",
                        Encoding.UTF8,
                        "application/json"
                    );
                    response = await httpClient.SendAsync(request);
                    Quiz_Frage quizFrageServer = JsonConvert.DeserializeObject<Quiz_Frage>(json);
                    Console.WriteLine(quizFrageServer.quiz_Frage_ID);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return true;
        }

        /*private async Task<bool> sendQuizFragen(long quizServerID)
        {
            return null;
        }*/



    }


}
