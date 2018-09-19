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

        public Frage GetSampleFrage()
        {
            Frage beispiel = new Frage();
            beispiel.Fragetext = "Wie hoch ist die MWSt in Deutschland?";

            List<Textantwort> antworten = new List<Textantwort>();

            Textantwort antwort1 = new Textantwort();
            antwort1.Text = "7 %";
            antwort1.wahr = true;
            antworten.Add(antwort1);

            Textantwort antwort2 = new Textantwort();
            antwort1.Text = "15 %";
            antwort1.wahr = false;
            antworten.Add(antwort2);

            Textantwort antwort3 = new Textantwort();
            antwort3.Text = "16 %";
            antwort3.wahr = false;
            antworten.Add(antwort3);

            Textantwort antwort4 = new Textantwort();
            antwort4.Text = "19 %";
            antwort4.wahr = true;
            antworten.Add(antwort4);

            Textantwort antwort5 = new Textantwort();
            antwort5.Text = "23 %";
            antwort5.wahr = false;
            antworten.Add(antwort5);

            Textantwort antwort6 = new Textantwort();
            antwort6.Text = "24 %";
            antwort6.wahr = false;
            antworten.Add(antwort6);

            Textantwort antwort7 = new Textantwort();
            antwort7.Text = "50 %";
            antwort7.wahr = false;
            antworten.Add(antwort7);

            Textantwort antwort8 = new Textantwort();
            antwort8.Text = "keine der Antworten ist richtig";
            antwort8.wahr = false;
            antworten.Add(antwort8);

            return beispiel;
        }

        public Task Save(Frage frage)
        {
            throw new NotImplementedException();
        }
    }
}
