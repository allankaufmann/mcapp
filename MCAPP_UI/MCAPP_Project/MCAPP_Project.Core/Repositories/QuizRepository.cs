using MCAPP_Project.Core.Models;
using PCLStorage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        readonly SQLiteAsyncConnection connection;

        public QuizRepository()
        {
            // Später wird auf LocalStorage umgestellt, für
            // Entwicklung ist es aber mühseelig immer den Pfad auf dem
            // Geärt herauszusuchen, daher wird erstmal unter /Users gespeichert!
            
            //var local = FileSystem.Current.LocalStorage.Path;
            //var datafile = PortablePath.Combine(local, "test2.db");
            //connection = new SQLiteAsyncConnection(datafile);

            connection = new SQLiteAsyncConnection("/Users/allan/test.db");

            
            connection.GetConnection().CreateTable<Quiz_Frage>();
            connection.GetConnection().CreateTable<Quiz>();
        }

        public Task Save(Quiz quiz)
        {
            Task ta = connection.InsertAsync(quiz);

            return ta;

        }

        public Task SaveAntwort(Quiz_Frage quizFrage)
        {
            Task ta = connection.InsertAsync(quizFrage);
            return ta;
        }


        public Task<List<Quiz>> GetAll()
        {
            return connection.Table<Quiz>().ToListAsync();
        }


    }
}
