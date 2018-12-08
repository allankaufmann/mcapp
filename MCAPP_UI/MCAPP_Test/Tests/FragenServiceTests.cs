using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using NUnit.Framework;
using MCAPP_Project.Core.Services;
using MCAPP_Project.Core.Models;
using System.Threading.Tasks;

namespace MCAPP_Test.Tests
{
    [TestFixture]
    public class FragenServiceTests
    {
        IFragenService service;
        IQuizService quizService;

        [SetUp]
        public void SetUp()
        {
            DummyFragenRepository repo = new DummyFragenRepository();
            DummyQuizRepository repoQuiz = new DummyQuizRepository();
            quizService = new QuizService(repo, repoQuiz);
            service = new FrageService(repo, quizService);

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
                if (f.thema_id!=1)
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

            Assert.AreEqual(1, t.id);
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
                    if (f.thema_id == t.id)
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
        public async System.Threading.Tasks.Task FragenMitFesterAnzahl()
        {
            List<Thema> themen = new List<Thema>();
            themen.Add(service.GetThema(1));
            themen.Add(service.GetThema(2));

            List<Frage> fragen = await service.GetFragen(themen, 10);

            Assert.LessOrEqual(fragen.Count, 10);
        }

        [Test]
        public async System.Threading.Tasks.Task FragenMitHoherFesterAnzahl()
        {
            List<Thema> themen = new List<Thema>();
            themen.Add(service.GetThema(1));
            themen.Add(service.GetThema(2));

            List<Frage> fragen = await service.GetFragen(themen, 99);

            Assert.LessOrEqual(fragen.Count, 99);
        }

        [Test]
        public void FragenNachThemenGruppiert()
        {
            List<Thema> themen = new List<Thema>();
            themen.Add(service.GetThema(1));
            themen.Add(service.GetThema(2));

            Dictionary<long, List<Frage>> fragenListe = service.GetFragenDictionary(themen);
            Assert.AreEqual(2, fragenListe.Count);

            Assert.AreEqual(1, fragenListe[1][0].thema_id);
            Assert.AreEqual(2, fragenListe[2][0].thema_id);
        }

        [Test]
        public async System.Threading.Tasks.Task FragenNachThemenGleichverteilt()
        {
            List<Thema> themen = new List<Thema>();
            themen.Add(service.GetThema(1));
            themen.Add(service.GetThema(2));
            themen.Add(service.GetThema(3));
            List<Frage> fragen = await service.GetFragen(themen, 10);

            int countThema1 = 0;
            int countThema2 = 0;
            int countThema3 = 0;

            foreach (Frage f in fragen)
            {
                if (f.thema_id==1)
                {
                    countThema1++;
                } else if (f.thema_id==2)
                {
                    countThema2++;
                } else if (f.thema_id==3)
                {
                    countThema3++;
                }

            }

            Assert.AreEqual(countThema1, countThema2);
            Assert.Greater(countThema3, countThema1);
        }

        [Test] 
        public async System.Threading.Tasks.Task zufallsFragenNotSame()
        {
            Thema t = service.GetThema(1);
            Frage f1 = (await service.GetZufallsFragen(t, 1))[0];
            Frage f2 = (await service.GetZufallsFragen(t, 1))[0];
            Frage f3 = (await service.GetZufallsFragen(t, 1))[0];

            Boolean equal = f1.Equals(f2);
            equal = equal && f2.Equals(f3);

            Assert.IsFalse(equal);            

        }

        [Test]
        public void VerteilungVonFragenAufThemen()
        {
            Thema t = service.GetThema(1);
            Thema t2 = service.GetThema(2);
            Thema t3 = service.GetThema(3);
            Thema t5 = service.GetThema(5); // Thema mit 2 Fragen
            Thema t6 = service.GetThema(6); // Thema mit 1 Frage
            Thema t7 = service.GetThema(7);
            List<Thema> themen = new List<Thema>();
            themen.Add(t);
            themen.Add(t2);
            themen.Add(t3);

            int[] verteilung = service.CalcAnzahlProThema(themen, 9);

            Assert.AreEqual(3, verteilung[0]);
            Assert.AreEqual(3, verteilung[1]);
            Assert.AreEqual(3, verteilung[2]);

            verteilung = service.CalcAnzahlProThema(themen, 10);

            Assert.AreEqual(3, verteilung[0]);
            Assert.AreEqual(3, verteilung[1]);
            Assert.AreEqual(4, verteilung[2]);

            themen = new List<Thema>();
            themen.Add(t);
            themen.Add(t2);
            themen.Add(t5);

            verteilung = service.CalcAnzahlProThema(themen, 10);
            Assert.AreEqual(3, verteilung[0]);
            Assert.AreEqual(5, verteilung[1]);
            Assert.AreEqual(2, verteilung[2]);

            themen = new List<Thema>();
            themen.Add(t);
            themen.Add(t5);
            themen.Add(t6);            
            verteilung = service.CalcAnzahlProThema(themen, 10);
            Assert.AreEqual(7, verteilung[0]);
            Assert.AreEqual(2, verteilung[1]);
            Assert.AreEqual(1, verteilung[2]);

            themen = new List<Thema>();
            themen.Add(t);
            themen.Add(t6);
            themen.Add(t7);
            verteilung = service.CalcAnzahlProThema(themen, 10);
            Assert.AreEqual(9, verteilung[0]);
            Assert.AreEqual(1, verteilung[1]);
            Assert.AreEqual(0, verteilung[2]);

            themen = new List<Thema>();
            themen.Add(t);
            themen.Add(t6);
            themen.Add(t7);
            verteilung = service.CalcAnzahlProThema(themen, 99);
            Assert.AreEqual(11, verteilung[0]);
            Assert.AreEqual(1, verteilung[1]);
            Assert.AreEqual(0, verteilung[2]);

        }

        [Test]
        public async Task zufallsFrageOhneBereitsbeantworteteFragen()
        {
            Thema t = service.GetThema(1);
            List<Frage> fragen = await service.GetZufallsFragen(t, 5);

            Boolean foundBeantworteFragen = false;

            foreach (Frage f in fragen) {

                if (f.id==2 || f.id == 4 || f.id == 6
                    || f.id == 8 || f.id == 10)                   
                {
                    foundBeantworteFragen = true;
                }

            }
            Assert.IsFalse(foundBeantworteFragen);
        }

        [Test]
        public async Task zufallsFrageMitNichtBeantworteteFragen()
        {
            Thema t = service.GetThema(1);
            List<Frage> fragen = await service.GetZufallsFragen(t, 6);

            Boolean foundFrage1 = false;
            Boolean foundFrage3 = false;
            Boolean foundFrage5 = false;
            Boolean foundFrage7 = false;
            Boolean foundFrage9 = false;
            Boolean foundFrage11 = false;


            foreach (Frage f in fragen)
            {

                if (f.id==1)
                {
                    foundFrage1 = true;
                } else if (f.id == 3)
                {
                    foundFrage3 = true;
                }
                else if (f.id == 5)
                {
                    foundFrage5 = true;
                }
                else if (f.id == 7)
                {
                    foundFrage7 = true;
                }
                else if (f.id == 9)
                {
                    foundFrage9 = true;
                }
                else if (f.id == 11)
                {
                    foundFrage11 = true;
                }
            }

            Boolean ok = foundFrage1 && foundFrage3
                && foundFrage5 && foundFrage7 && foundFrage9 && foundFrage11;

            Assert.IsTrue(ok);
        }

        [Test]
        public async Task ZufallsFrageEnthaeltAuchBeantworteFragen()
        {
            Thema t = service.GetThema(1);
            List<Frage> fragen = await service.GetZufallsFragen(t, 7);

            Boolean foundBeantworteFragen = false;

            foreach (Frage f in fragen)
            {

                if (f.id == 2 || f.id == 4 || f.id == 6
                    || f.id == 8 || f.id == 10)
                {
                    foundBeantworteFragen = true;
                }

            }
            Assert.IsTrue(foundBeantworteFragen);
        }



    }
}