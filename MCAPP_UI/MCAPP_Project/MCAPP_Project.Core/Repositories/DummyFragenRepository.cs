using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;

namespace MCAPP_Project.Core.Repositories
{
    public class DummyFragenRepository : IFragenRepository
    {
        public Task<List<Frage>> GetAllFragen()
        {
            throw new NotImplementedException();
        }

        public List<Thema> GetAllThemen()
        {
            List<Thema> themen = new List<Thema>();

            themen.Add(getThema(1, "Externes Rechnungswesen"));
            themen.Add(getThema(2, "Einführung in die technische und theoretische Informatik"));
            themen.Add(getThema(3, "Von-Neumann-Rechner und Prozessortechnik"));
            themen.Add(getThema(4, "Speicherkonzepte"));
            themen.Add(getThema(5, "Grundlegende Modelle der Informatik"));
     
            return themen;

        }

        private Thema getThema(long id, String text)
        {
            Thema thema = new Thema();
            thema.ThemaID = id;
            thema.ThemaText = text;

            return thema;
        }

        public Frage GetSampleFrage()
        {
            FragenBuilder builder = new FragenBuilder();

            Frage beispiel = builder.createFrage(1, "Wie hoch ist die MWSt in Deutschland ?", 1)
                .WithAntwort("7 %", true)
                .WithAntwort("15 %", false)
                .WithAntwort("16 %", false)
                .WithAntwort("19 %", true)
                .WithAntwort("23 %", false)
                .WithAntwort("24 %", false)
                .WithAntwort("50 %", false)
                .WithAntwort("keine der Antworten ist richtig", false)
                .Build();


            return beispiel;
        }

        public Task Save(Frage frage)
        {
            throw new NotImplementedException();
        }

        public List<Frage> GetAlleFragen()
        {
            List<Frage> fragen = new List<Frage>();
            


            return fragen;
        }
    }
}
