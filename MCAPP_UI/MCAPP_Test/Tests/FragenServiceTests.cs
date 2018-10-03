using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using NUnit.Framework;
using MCAPP_Project.Core.Services;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Models;

namespace MCAPP_UI.Tests
{
    [TestFixture]
    public class FragenServiceTests
    {
        IFragenService service;

        [SetUp]
        public void SetUp()
        {
            IFragenRepository repo = new DummyFragenRepository();
            service = new FrageService(repo);

        }

        [Test]
        public void testSampleFrage()
        {
            Frage frage = service.GetSampleFrage();
            Assert.NotNull(frage);
        }

        [Test]
        public void testThemenListe()
        {
            List<Thema> themenListe = service.GetAllThemen();
            Assert.GreaterOrEqual(themenListe.Count, 1);
        }

        [Test]
        public void fragenFuerEinThema()
        {
            List<Frage> fragen = service.GetFragen(1);
            Boolean ok = true;
            foreach(Frage f in fragen)
            {
                if (f.themaID!=1)
                {
                    ok = false;
                }
            }

            Assert.IsTrue(ok);
        }

        [Test]
        public void foundThema()
        {
            Thema t = service.GetThema(1);

            Assert.AreEqual(1, t.ThemaID);
        }

        [Test]
        public void fragenFuerMehrereThemen()
        {
            List<Thema> themen = new List<Thema>();
            themen.Add(service.GetThema(1));
            themen.Add(service.GetThema(2));

            List<Frage> fragen = new List<Frage>();
            fragen = service.GetFragen(themen);

            Boolean ok = true;

            foreach (Thema t in themen)
            {
                Boolean found = false;
                foreach (Frage f in fragen)
                {
                    if (f.themaID == t.ThemaID)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    ok = false;
                }
            }
            Assert.IsTrue(ok);
        }

        [Test]
        public void FragenMitFesterAnzahl()
        {
            List<Thema> themen = new List<Thema>();
            themen.Add(service.GetThema(1));
            themen.Add(service.GetThema(2));

            List<Frage> fragen = service.GetFragen(themen, 10);

            Assert.LessOrEqual(fragen.Count, 10);

        }


    }
}