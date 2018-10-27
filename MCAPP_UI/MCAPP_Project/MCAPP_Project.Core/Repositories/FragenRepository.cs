using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCLStorage;
using SQLite;
using System.IO;
using MCAPP_Project.Core.Models;
using System.Threading.Tasks;
using MCAPP_Project.Core.Services;

namespace MCAPP_Project.Core.Repositories
{
    /*
     * Repository um Fragen aus der Datenbank zu laden.
     */
    public class FragenRepository : IFragenRepository
    {

        private List<Frage> alleFragen;
        private List<Thema> themen;

        //readonly SQLiteAsyncConnection connection;
        readonly SQLiteConnection connection;


        public FragenRepository()
        {
            // Später wird auf LocalStorage umgestellt, für
            // Entwicklung ist es aber mühseelig immer den Pfad auf dem
            // Geärt herauszusuchen, daher wird erstmal unter /Users gespeichert!

            //var local = FileSystem.Current.LocalStorage.Path;
            //var datafile = PortablePath.Combine(local, "test2.db");
            //connection = new SQLiteAsyncConnection(datafile);

            connection = new SQLiteConnection("/Users/allan/test.db");
            connection.CreateTable<Frage>();
            connection.CreateTable<Thema>();

            loadThemen();
            loadFragen();

        }

        public List<Frage> GetAlleFragen()
        {
            return this.alleFragen;
        }

        public List<Frage> GetAllFragen()
        {
            
            return this.alleFragen;
        }

        public List<Thema> GetAllThemen()
        {
            return this.themen;
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

        public Frage GetSampleFrage()
        {
            throw new NotImplementedException();
        }

        public bool loadFragen()
        {
            this.alleFragen = new List<Frage>();

            if (MCAPP_PROPERTIES.DEMO_MODUS)
            {
                FragenBuilder builder = new FragenBuilder();
                Frage frage = builder.createFrage(0, "Demo Frage", 0)
                    .WithAntwort("Demo Antwort 1", true)
                    .WithAntwort("Demo Antwort 2", false)
                    .WithAntwort("Demo Antwort 3", false)
                    .WithAntwort("Demo Antwort 4", false)
                    .WithAntwort("Demo Antwort 5", false)
                    .WithAntwort("Demo Antwort 6", false)
                    .WithAntwort("Demo Antwort 7", false)
                    .WithAntwort("Demo Antwort 8", false)
                    .Build();
                this.alleFragen.Add(frage);

            }

            return true;
        }

        public bool loadThemen()
        {
            this.themen = connection.Table<Thema>().ToList<Thema>();

            if (themen == null || themen.Count == 0)
            {
                MCAPP_PROPERTIES.DEMO_MODUS = true;

                Thema demoThema = new Thema();
                demoThema.ThemaText = "Demo-Thema";

                
                this.themen.Add(demoThema);
            }

            return true;
        }

        public Task Save(Frage frage)
        {
            //return connection.InsertOrReplaceAsync(frage);         
            return null;
        }

        List<Thema> IFragenRepository.GetAllThemen()
        {
            return this.themen;
        }

        public int SaveThema(Thema thema)
        {
            return connection.InsertOrReplace(thema);
        }
    }
}