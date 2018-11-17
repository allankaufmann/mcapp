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
using MCAPP_Project.Core.Utils;

namespace MCAPP_Project.Core.Repositories
{
    /*
     * Repository um Fragen aus der Datenbank zu laden.
     */
    public class FragenRepository : IFragenRepository
    {

        //private List<Frage> alleFragen;
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
            connection.CreateTable<Textantwort>();
            connection.CreateTable<Quiz_Frage>();
            connection.CreateTable<Quiz>();

            loadThemen();
        }







        public List<Thema> GetAllThemen()
        {
            return this.themen;
        }

        public List<Frage> GetFragen(long themaID)
        {
            List<Frage> fragen = new List<Frage>();

            foreach (Frage f in this.GetAlleFragen())
            {
                if (f.thema_id == themaID && (f.antworten!=null && f.antworten.Count > 0))
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

        public List<Frage> GetAlleFragen()
        {
            List<Frage> alleFragen = new List<Frage>();

            try
            {
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
                    alleFragen.Add(frage);
                    return alleFragen;
                }

                alleFragen = connection.Table<Frage>().ToList<Frage>();
                Dictionary<long, Frage> alleFragenDict = MCAPPUtils.convertListToDictionary(alleFragen);

                List<Textantwort> antworten = connection.Table<Textantwort>().ToList<Textantwort>();
                foreach (Textantwort a in antworten)
                {
                    if (alleFragenDict.ContainsKey(a.frage_id))
                    {
                        Frage f = alleFragenDict[a.frage_id];
                        if (f.antworten == null)
                        {
                            f.antworten = new List<Textantwort>();
                        }
                        f.antworten.Add(a);
                    }
                }
                return alleFragen;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return alleFragen;
            }          
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

        public int SaveFrage(Frage frage)
        {

            int rows = 0;
            int antwortRows = 0;

            try
            {
                rows = connection.InsertOrReplace(frage);

                if (frage.antworten != null && frage.antworten.Count > 0)
                {
                    foreach (Antwort a in frage.antworten)
                    {
                        antwortRows+= connection.InsertOrReplace(a);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(rows + " Fragen wurden in DB geschrieben!");
                Console.WriteLine(antwortRows + " Antworten wurden in DB geschrieben!");
                Console.WriteLine(e.ToString());
            }                      
            return rows;   
        }

        List<Thema> IFragenRepository.GetAllThemen()
        {
            return this.themen;
        }

        public int SaveThema(Thema thema)
        {
            return connection.InsertOrReplace(thema);
        }

        public int LoescheFrage(Frage frage)
        {
            List<Textantwort> antworten = connection.Table<Textantwort>()
                .Where(v => v.frage_id == frage.id)
                .ToList<Textantwort>();

            try
            {
                foreach(Textantwort a in antworten)
                {
                    connection.Delete(a);
                }

            } catch (Exception e)
            {
                return 0;
            }



            return connection.Delete(frage);            
        }
    }
}