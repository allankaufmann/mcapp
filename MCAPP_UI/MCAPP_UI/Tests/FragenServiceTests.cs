using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
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

        Mock<IFragenRepository> repo;

        [SetUp]
        public void SetUp()
        {
            repo = new Mock<IFragenRepository>();
            service = new FrageService(repo.Object);
        }


        [Test]
        public async Task TestSomething()
        {
            await service.AddNewFrage("LaLa");

            Assert.AreEqual(1, service.GetAllFragen().Result.Count);
        }



    }
}