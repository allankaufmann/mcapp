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

        private List<Thema> themen;

        private List<Frage> alleFragen;


        public DummyFragenRepository()
        {
            loadThemen();
            loadFragen();
        }


        public bool loadThemen()
        {
            this.themen = new List<Thema>();
            themen.Add(getThema(1, "Externes Rechnungswesen"));
            themen.Add(getThema(2, "Einführung in die technische und theoretische Informatik"));
            themen.Add(getThema(3, "Von-Neumann-Rechner und Prozessortechnik"));
            themen.Add(getThema(4, "Speicherkonzepte"));
            themen.Add(getThema(5, "Grundlegende Modelle der Informatik"));
            themen.Add(getThema(6, "Beispielthema 6"));
            themen.Add(getThema(7, "Beispielthema 7"));
            return true;
        }

        public bool loadFragen()
        {
            this.alleFragen = new List<Frage>();

            int frageID = 1;

            FragenBuilder builder = new FragenBuilder();


            this.alleFragen.Add(builder.createFrage(frageID++, "Wie hoch ist die MWSt in Deutschland ?", 1)
                .WithAntwort("7 %", true)
                .WithAntwort("15 %", false)
                .WithAntwort("16 %", false)
                .WithAntwort("19 %", true)
                .WithAntwort("23 %", false)
                .WithAntwort("24 %", false)
                .WithAntwort("50 %", false)
                .WithAntwort("keine der Antworten ist richtig", false)
                .Build()
            );

            for (int i = 0; i < 10; i++)
            {
                this.alleFragen.Add(createBeispielfrage(frageID++, 1));
            }



            this.alleFragen.Add(
                builder.createFrage(frageID++, "Welche der folgenden Aussagen treffen zu?", 2)
                .WithAntwort("Zum Chipsatz eines PC´s gehören im Wesentlichen die Brückenbausteine, welche die unterschiedlichen Geschwindigkeiten angeschlossener Systeme ausgleichen, und der Hauptspeicher", false)
                .WithAntwort("Ein PC ist in der Regel einem bestimmten Benutzer zugeordnet, ein Server hingegen liefert Dienstleistungen für viele angekoppelte Desktops oder Notebooks", true)
                .WithAntwort("Die Definition einer Spur ist sowohl für Festplatten als auch für CD-Roms identisch", false)
                .WithAntwort("Linux verwaltet die Festplattenadressen durch Verkettung einzelner Speicherblöcke.Diese Speicherblöcke werden auch Inodes genannt", false)
                .WithAntwort("LCDs basieren auf den optischen Eigenschaften von Flüssigkeitskristallen, die aus durchsichtigen organischen Molekülen bestehen", true)
                .Build()
            );

            for (int i = 0; i < 10; i++)
            {
                this.alleFragen.Add(createBeispielfrage(frageID++, 2));
            }

            this.alleFragen.Add(
                builder.createFrage(frageID++, "Beispielfrage Thema 3?", 3)
                .WithAntwort("BlaBlaBlubb 1", false)
                .WithAntwort("(R) Ein PC ist in der Regel einem bestimmten Benutzer zugeordnet, ein Server hingegen liefert Dienstleistungen für viele angekoppelte Desktops oder Notebooks", true)
                .WithAntwort("BlaBlaBlubb 2", false)
                .WithAntwort("BlaBlaBlubb 3", false)
                .WithAntwort("BlaBlaBlubb 4", false)
                .Build()
            );

            for (int i = 0; i < 10; i++)
            {
                this.alleFragen.Add(createBeispielfrage(frageID++, 3));
            }

            this.alleFragen.Add(
                builder.createFrage(frageID++, "Beispielfrage Thema 4?", 4)
                .WithAntwort("BlaBlaBlubb 5", false)
                .WithAntwort("Ein PC ist in der Regel einem bestimmten Benutzer zugeordnet, ein Server hingegen liefert Dienstleistungen für viele angekoppelte Desktops oder Notebooks", true)
                .WithAntwort("BlaBlaBlubb 6", false)
                .WithAntwort("BlaBlaBlubb 7", false)
                .WithAntwort("BlaBlaBlubb 8", false)
                .Build()
            );

            for (int i = 0; i < 10; i++)
            {
                this.alleFragen.Add(createBeispielfrage(frageID++, 4));
            }

            this.alleFragen.Add(
                builder.createFrage(frageID++, "Beispielfrage Thema 5?", 5)
                .WithAntwort("BlaBlaBlubb 9", false)
                .WithAntwort("Ein PC ist in der Regel einem bestimmten Benutzer zugeordnet, ein Server hingegen liefert Dienstleistungen für viele angekoppelte Desktops oder Notebooks", true)
                .WithAntwort("BlaBlaBlubb 10", false)
                .WithAntwort("BlaBlaBlubb 11", false)
                .WithAntwort("BlaBlaBlubb 12", false)
                .Build()
            );

            this.alleFragen.Add(
               builder.createFrage(frageID++, "Beispielfrage 2 Thema 5?", 5)
               .WithAntwort("BlaBlaBlubb 13", false)
               .WithAntwort("Ein PC ist in der Regel einem bestimmten Benutzer zugeordnet, ein Server hingegen liefert Dienstleistungen für viele angekoppelte Desktops oder Notebooks", true)
               .WithAntwort("BlaBlaBlubb 14", false)
               .WithAntwort("BlaBlaBlubb 15", false)
               .WithAntwort("BlaBlaBlubb 16", false)
               .Build()
           );

            this.alleFragen.Add(
               builder.createFrage(frageID++, "Beispielfrage 1 Thema 6?", 6)
               .WithAntwort("BlaBlaBlubb 13", false)
               .WithAntwort("Ein PC ist in der Regel einem bestimmten Benutzer zugeordnet, ein Server hingegen liefert Dienstleistungen für viele angekoppelte Desktops oder Notebooks", true)
               .WithAntwort("BlaBlaBlubb 14", false)
               .WithAntwort("BlaBlaBlubb 15", false)
               .WithAntwort("BlaBlaBlubb 16", false)
               .Build()
           );


            return true;
        }


        public List<Thema> GetAllThemen()
        {
            return this.themen;
        }



        public Task<List<Frage>> GetAllFragen()
        {
            throw new NotImplementedException();
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
            return GetAlleFragen().ToArray()[1];
        }

        public Task Save(Frage frage)
        {
            throw new NotImplementedException();
        }

        private Frage createBeispielfrage(int frageID, int themaID)
        {
            FragenBuilder builder = new FragenBuilder();
            Frage frage = builder.createFrage(frageID, "Beispielfrage " + frageID + " zu Thema " + themaID, themaID)
                .WithAntwort("Antwort 1", false)
                .WithAntwort("Antwort 2 (R)", true)
                .WithAntwort("Antwort 3", false)
                .WithAntwort("Antwort 4", false)
                .WithAntwort("Antwort 5", false)
                .WithAntwort("Antwort 6", false)
                .Build();
            return frage;
        }

        public List<Frage> GetAlleFragen()
        {
            return this.alleFragen;
        }

        public List<Frage> GetFragen(long themaID)
        {
            List<Frage> fragen = new List<Frage>();

            foreach (Frage f in this.alleFragen)
            {
                if (f.themaID == themaID)
                {
                    fragen.Add(f);
                }
            }

            return fragen;
        }


    }
}
