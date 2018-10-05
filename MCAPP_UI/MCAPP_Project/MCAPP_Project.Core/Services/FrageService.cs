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

        /** Objekt für threadsichere Verarbeitung.*/
        private static readonly object syncLock = new object();

        /** Objekt zur Generierung von Zufallszahlen. */
        private static readonly Random random = new Random();

        public FrageService(IFragenRepository repository)
        {
            this.repository = repository;
        }

        public Task AddNewFrage(Frage frage)
        {
            return repository.Save(frage);
        }

        public Task<List<Frage>> GetAllFragen()
        {
            return repository.GetAllFragen();
        }

        public List<Thema> GetAllThemen()
        {
            return repository.GetAllThemen();
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
                List<Frage> fragen = GetFragen(t.ThemaID);
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
                List<Frage> fragen = GetFragen(t.ThemaID);
                foreach (Frage f in fragen)
                {
                    fragenListe.Add(f);
                }
                fragenDictionary.Add(t.ThemaID, fragenListe);

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
                int count = GetFragen(gewaelteThemen[i].ThemaID).Count;
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
                        int count = GetFragen(gewaelteThemen[j].ThemaID).Count;
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

        public List<Frage> GetFragen(List<Thema> gewaelteThemen, int anz)
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
                    fragenListe.AddRange(GetZufallsFragen(gewaelteThemen[i], anzahlProThema[i]));
                }

            }



            return fragenListe;
        }

        /* Ersetzt.... ggfls. kann Dictionary noch an anderer Stelle verwendet werden!
            public List<Frage> GetFragen2(List<Thema> gewaelteThemen, int anz)
        {
            if (gewaelteThemen==null || gewaelteThemen.Count==0 || anz == 0)
            {
                return new List<Frage>();
            }

            int anzahlAlleGewaehltenFragen = GetFragen(gewaelteThemen).Count;
            Dictionary<long, List<Frage>> fragenDict = GetFragenDictionary(gewaelteThemen);
            List<Frage> fragenListe = new List<Frage>();


            List<int> themenCounter = new List<int>();
            foreach (Thema t in gewaelteThemen) {
                themenCounter.Add(0);
            }


            int anzahlFrageProThema = anz / gewaelteThemen.Count;

            for (int i = 0; i < anz; i++)
            {               
                // Es dürfen nicht mehr Fragen gezogen werden, als vorhanden sind!
                if (anzahlAlleGewaehltenFragen> (i))
                {
                    Boolean foundFrage = false;
                    for (int j = 0; j < gewaelteThemen.Count; j++)
                    {
                        if (foundFrage)
                        {
                            continue;
                        }


                        if (themenCounter[j]<anzahlFrageProThema)
                        {
                            int fragenIndex = themenCounter[j];

                            if (fragenIndex < fragenDict[gewaelteThemen[j].ThemaID].Count)
                            {
                                fragenListe.Add(fragenDict[gewaelteThemen[j].ThemaID][fragenIndex]);
                                foundFrage = true;
                                themenCounter[j]++;
                            }
                        }
                    }

                    if (!foundFrage)
                    {
                        long letzteThemenID = gewaelteThemen[gewaelteThemen.Count - 1].ThemaID;
                        int themenIndex = themenCounter[gewaelteThemen.Count - 1];
                        if (fragenDict[letzteThemenID].Count > themenIndex)
                        {
                            fragenListe.Add(fragenDict[letzteThemenID][themenIndex]);
                        }
                        themenCounter[gewaelteThemen.Count - 1]++;                        
                    }
                    
                }
            }
            return fragenListe;
        }
        */


        public Frage GetSampleFrage()
        {
            Frage beispiel = new Frage();
            beispiel.Fragetext = "Wie hoch ist die MWSt in Deutschland?";
            return beispiel;
        }

        public Thema GetThema(long themaID)
        {
            List<Thema> themen = GetAllThemen();
            foreach (Thema t in themen)
            {
                if(t.ThemaID==themaID)
                {
                    return t;
                }
            }

            return null;
        }



        public List<Frage> GetZufallsFragen(Thema thema, int anz)
        {
            List<Frage> alleFragen = GetFragen(thema.ThemaID);
            List<Frage> zufallsFragen = new List<Frage>();

            for (int i = 0; i < anz; i++)
            {

                lock (syncLock)
                {
                    if (alleFragen.Count == 0)
                    {
                        break;
                    }

                    int index = random.Next(0, alleFragen.Count);
                    zufallsFragen.Add(alleFragen[index]);
                    alleFragen.RemoveAt(index);
                }
            }
            return zufallsFragen;

        }
    }
}