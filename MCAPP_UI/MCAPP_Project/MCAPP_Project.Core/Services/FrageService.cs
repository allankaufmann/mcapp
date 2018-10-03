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

        public List<Frage> GetFragen(List<Thema> gewaelteThemen, int anz)
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
    }
}