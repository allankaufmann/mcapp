using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;

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

            Thema thema1 = new Thema();
            thema1.ThemaID = 1;
            thema1.ThemaText = "Externes Rechnungswesen";
            themen.Add(thema1);

            Thema thema2 = new Thema();
            thema2.ThemaText = "Einführung in die technische und theoretische Informatik";
            thema2.ThemaID = 2;
            themen.Add(thema2);

            Thema thema3 = new Thema();
            thema3.ThemaText = "Von-Neumann-Rechner und Prozessortechnik";
            thema3.ThemaID = 3;
            themen.Add(thema3);

            Thema thema4 = new Thema();
            thema4.ThemaText = "Speicherkonzepte";
            thema4.ThemaID = 4;
            themen.Add(thema4);

            Thema thema5 = new Thema();
            thema5.ThemaText = "Grundlegende Modelle der Informatik";
            thema5.ThemaID = 5;
            themen.Add(thema5);



            return themen;

        }

        private Textantwort getAntwort(String text, Boolean wahr)
        {
            Textantwort antwort = new Textantwort();
            antwort.Text = text;
            antwort.wahr = wahr;
            return antwort;
        }


        public Frage GetSampleFrage()
        {
            Frage beispiel = new Frage();
            beispiel.themaID = 1;
            beispiel.Fragetext = "Wie hoch ist die MWSt in Deutschland?";

            List<Textantwort> antworten = new List<Textantwort>();
            antworten.Add(getAntwort("7 %", true));
            antworten.Add(getAntwort("15 %", false));
            antworten.Add(getAntwort("16 %", false));
            antworten.Add(getAntwort("19 %", true));
            antworten.Add(getAntwort("23 %", false));
            antworten.Add(getAntwort("24 %", false));
            antworten.Add(getAntwort("50 %", false));
            antworten.Add(getAntwort("keine der Antworten ist richtig", false));
            beispiel.antworten = antworten;

            return beispiel;
        }

        public Task Save(Frage frage)
        {
            throw new NotImplementedException();
        }
    }
}
