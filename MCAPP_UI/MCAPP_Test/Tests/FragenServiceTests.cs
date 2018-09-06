using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MCAPP_UI.Services;
using MCAPP_UI.Models;
using NUnit.Framework;
using MCAPP_UI.Repositories;
using Moq;
using System.Threading.Tasks;

namespace MCAPP_UI.Tests
{
    [TestFixture]
    public class FragenServiceTests
    {
        IFragenService service;

        //Mock<IFragenRepository> repo;
        IFragenRepository repo;

        [SetUp]
        public void SetUp()
        {
            //repo = new Mock<IFragenRepository>();
            repo = new FragenRepository();
            service = new FrageService(repo);

            var fragen = new List<Frage>
                { new Frage() };

            //repo.Setup(r => r.GetAllFragen()).ReturnsAsync(fragen);
        }


        [Test]
        public async Task TestSomething()
        {
            Task f = service.AddNewFrage(new Frage());

            Assert.AreEqual(1, service.GetAllFragen().Result.Count);
        }



    }
}