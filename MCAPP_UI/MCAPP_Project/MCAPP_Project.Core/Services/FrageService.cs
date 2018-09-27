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
                if (fragen.Count>0)
                {
                    fragenListe.Add(fragen[0]);
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
    }
}