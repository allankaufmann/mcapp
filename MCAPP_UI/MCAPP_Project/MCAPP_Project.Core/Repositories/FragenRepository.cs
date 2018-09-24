using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCLStorage;
using SQLite;
using System.IO;
using MCAPP_Project.Core.Models;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Repositories
{
    /*
     * Repository um Fragen aus der Datenbank zu laden.
     */
    public class FragenRepository : IFragenRepository
    {

        readonly SQLiteAsyncConnection connection;


        public FragenRepository()
        {
            connection = new SQLiteAsyncConnection("test.db");
            connection.GetConnection().CreateTable<Frage>();
        }

        public List<Frage> GetAlleFragen()
        {
            throw new NotImplementedException();
        }

        public Task<List<Frage>> GetAllFragen()
        {
            return connection.Table<Frage>().ToListAsync();      
        }

        public Task<List<Thema>> GetAllThemen()
        {
            throw new NotImplementedException();
        }

        public List<Frage> GetFragen(long themaID)
        {
            throw new NotImplementedException();
        }

        public Frage GetSampleFrage()
        {
            throw new NotImplementedException();
        }

        public Task Save(Frage frage)
        {
            return connection.InsertOrReplaceAsync(frage);         
        }

        List<Thema> IFragenRepository.GetAllThemen()
        {
            throw new NotImplementedException();
        }
    }
}