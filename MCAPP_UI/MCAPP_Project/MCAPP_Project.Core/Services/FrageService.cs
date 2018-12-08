using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;

namespace MCAPP_Project.Core.Services
{
    public class FrageService : IFragenService
    {
        readonly IFragenRepository repository;
        readonly IQuizService quizService;

        /** Objekt für threadsichere Verarbeitung.*/
        private static readonly object syncLock = new object();

        /** Objekt zur Generierung von Zufallszahlen. */
        private static readonly Random random = new Random();

        public FrageService(IFragenRepository repository, IQuizService quizService)
        {
            this.repository = repository;
            this.quizService = quizService;
        }

        public Boolean refreshData()
        {
            return repository.loadThemen();
        }


        public int SaveFrage(Frage frage)
        {
            return repository.SaveFrage(frage);
        }

        public int SaveThema(Thema thema)
        {
            return repository.SaveThema(thema);
        }

        public List<Thema> GetAllThemen()
        {
            return repository.GetAllThemen();
        }

        public List<Frage> GetAllFragen()
        {
            return repository.GetAlleFragen();
        }


        public List<Frage> GetFragen(long themaID)
        {
            return repository.GetFragen(themaID);
        }

        public List<Frage> GetFragen(List<Thema> gewaelteThemen)
        {
            List<Frage> fragenListe = new List<Frage>();

            foreach (Thema t in gewaelteThemen)
            {
                List<Frage> fragen = GetFragen(t.id);
                foreach (Frage f in fragen)
                {
                    fragenListe.Add(f);
                }
            }               
            return fragenListe;
        }

        public Dictionary<long, List<Frage>> GetFragenDictionary(List<Thema> gewaelteThemen)
        {
            Dictionary<long, List<Frage>> fragenDictionary = new Dictionary<long, List<Frage>>();

            foreach (Thema t in gewaelteThemen)
            {
                List<Frage> fragenListe = new List<Frage>();
                List<Frage> fragen = GetFragen(t.id);
                foreach (Frage f in fragen)
                {
                    fragenListe.Add(f);
                }
                fragenDictionary.Add(t.id, fragenListe);

            }
            return fragenDictionary;
        }

        public int[] CalcAnzahlProThema(List<Thema> gewaelteThemen, int anz)
        {
            
            int anzahlAlleGewaehltenFragen = GetFragen(gewaelteThemen).Count;
            
            // Durchschnitt bilden
            int durchschnittFragenProThema = anz / gewaelteThemen.Count;

            // In diesem Feld halten wir fest, wieviele Fragen pro Thema
            int[] anzahlProThema = new int[gewaelteThemen.Count];

            // Hier halten wir fest, wenn zu einem Thema bereits alle Fragen gezogen werden
            bool[] themaHatZuwenigFragen = new bool[gewaelteThemen.Count];

            /*
             * Zunächst pro Thema Durchschnittswert in Feld setzen. 
             */
            for (int i = 0; i < gewaelteThemen.Count; i++)
            {
                int count = GetFragen(gewaelteThemen[i].id).Count;
                if (durchschnittFragenProThema <= count)
                {
                    anzahlProThema[i] = durchschnittFragenProThema;
                    themaHatZuwenigFragen[i] = false;
                }
                else
                {
                    anzahlProThema[i] = count;
                    themaHatZuwenigFragen[i] = true;
                }
            }

            // Möglicherweise sind genug Fragen vorhanden.
            int zwischensumme = 0;
            foreach (int i in anzahlProThema)
            {
                zwischensumme += i;
            }

            if (zwischensumme != anz)
            {
                int differenz = anz - zwischensumme;

                // Da noch Fragen fehlen wird nun für jede fehlende Frage geprüft, 
                // ob noch Fragen in einem der Themen vorhanden sind. 
                for (int i = 0; i < differenz; i++)
                {
                    bool found = false;

                    // Wir beginnen mit dem letztne gewählten thema
                    for (int j = (gewaelteThemen.Count-1); j >= 0; j--)
                    {
                        if (found)
                        {
                            continue;
                        }

                        if (themaHatZuwenigFragen[j])
                        {
                            continue;
                        }

                        /*
                         * Sofern noch eine weitere Frage vorhanden ist, wird
                         * der Counter erhöht. 
                         */
                        int count = GetFragen(gewaelteThemen[j].id).Count;
                        if (count > anzahlProThema[j] + 1)
                        {
                            anzahlProThema[j]++;
                            found = true;
                        }
                        else
                        {
                            themaHatZuwenigFragen[j] = true;
                        }

                    }
                    if (found == false)
                    {
                        break;
                    }

                }
            }

            return anzahlProThema;
        }

        public async Task<List<Frage>> GetFragen(List<Thema> gewaelteThemen, int anz)
        {
            if (gewaelteThemen == null || gewaelteThemen.Count == 0 || anz == 0)
            {
                return new List<Frage>();
            }
            List<Frage> fragenListe = new List<Frage>();

            int[] anzahlProThema = CalcAnzahlProThema(gewaelteThemen, anz);

            for(int i = 0; i < gewaelteThemen.Count; i++)
            {
                if (anzahlProThema[i]>0)
                {
                    List<Frage> fragen = await GetZufallsFragen(gewaelteThemen[i], anzahlProThema[i]);

                    fragenListe.AddRange(fragen);
                }

            }
            return fragenListe;
        }


        public Frage GetSampleFrage()
        {
            FragenBuilder builder = new FragenBuilder();
            return builder.createFrage(1, "Wie hoch ist die MWSt in Deutschland ?", 1)
                .WithAntwort("7 %", true)
                .WithAntwort("15 %", false)
                .WithAntwort("16 %", false)
                .WithAntwort("19 %", true)
                .WithAntwort("23 %", false)
                .WithAntwort("24 %", false)
                .WithAntwort("50 %", false)
                .WithAntwort("keine der Antworten ist richtig", false)
                .Build();
        }

        public Thema GetThema(long themaID)
        {
            List<Thema> themen = GetAllThemen();
            foreach (Thema t in themen)
            {
                if(t.id==themaID)
                {
                    return t;
                }
            }

            return null;
        }



        public async Task<List<Frage>> GetZufallsFragen(Thema thema, int anz)
        {
            List<Frage> alleFragen = GetFragen(thema.id);
            List<Frage> zufallsFragen = new List<Frage>();

            List<Frage> nichtBeantworteteFragen = new List<Frage>();

            foreach (Frage f in alleFragen)
            {
                bool frageNochNicht = await quizService.FrageNochNichtRichtigBeantwortet(f.id);

                if (frageNochNicht)
                {
                    nichtBeantworteteFragen.Add(f);
                }
            }

            foreach (Frage f in nichtBeantworteteFragen)
            {
                alleFragen.Remove(f);
            }           

            for (int i = 0; i < anz; i++)
            {

                lock (syncLock)
                {
                    if (alleFragen.Count == 0 && nichtBeantworteteFragen.Count == 0)
                    {
                        break;
                    }

                    List<Frage> fragenListe = (nichtBeantworteteFragen.Count > 0) ? nichtBeantworteteFragen : alleFragen;

                    int index = random.Next(0, fragenListe.Count);
                    zufallsFragen.Add(fragenListe[index]);
                    fragenListe.RemoveAt(index);
                }
            }

            return zufallsFragen;
        }

        public int LoescheFrage(Frage frage)
        {
            int loeschen = quizService.LoescheAuswertungFragen(frage);

            loeschen = repository.LoescheFrage(frage);


            return loeschen;
        }

        public int LoescheThema(Thema t)
        {
            int loeschen = repository.LoescheThema(t);
            return loeschen;
        }


    }
}