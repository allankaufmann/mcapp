using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Services
{
    public class FragenBuilder
    {
        private long FrageId { get; set; }

        private string Fragetext { get; set; }

        private long themaID { get; set; }

        private List<Textantwort> antworten { get; set; }


        public FragenBuilder createFrage(long FrageId, string FrageText, long themaId)
        {
            this.FrageId = FrageId;
            this.Fragetext = FrageText;
            this.themaID = themaId;
            antworten = new List<Textantwort>();
            return this;
        }

        public FragenBuilder WithAntwort(string antwortText, Boolean wahr)
        {
            Textantwort antwort = new Textantwort();
            antwort.Text = antwortText;
            antwort.wahr = wahr;
            antworten.Add(antwort);
            return this;
        }

        public Frage Build()
        {
            Frage frage = new Frage();
            frage.id = this.FrageId;
            frage.Fragetext = this.Fragetext;
            frage.thema_id = this.themaID;
            frage.antworten = this.antworten;
            return frage;
        }
    }
}
