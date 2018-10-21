using MCAPP_Project.Core;
using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace MCAPP_Test.Tests
{

    [TestFixture]
    public class RestCallTests
    {
        readonly HttpClient httpClient = new HttpClient();

        Token token = null;

        [SetUp]
        public async Task SetUp()
        {
            if (token == null)
            {
                httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

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

        [Test]
        public async Task tokenMussVorhandenSein()
        {
            Assert.IsNotNull(token.ID_TOKEN);
        }

        [Test]
        public async Task holeFragen()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/frages");
            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<Frage> fragen = JsonConvert.DeserializeObject <List<Frage>>(json);

            Assert.Greater(fragen.Count, 0);
        }

        [Test]
        public async Task holeThemen()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ID_TOKEN);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/themas");
            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            List<Thema> themen = JsonConvert.DeserializeObject<List<Thema>>(json);

            Assert.Greater(themen.Count, 0);
        }

    }
}
