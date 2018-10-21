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

        public Quiz Save(Quiz quiz)
        {



            //Task ta = connection.InsertAsync(quiz);

            return null;

        }

        public Quiz_Frage SaveQuiz_Frage(Quiz_Frage quizFrage)
        {
            //Task ta = connection.InsertAsync(quizFrage);
            //return ta;
            return null;
        }


        public Task<List<Quiz>> GetAll()
        {
            return connection.Table<Quiz>().ToListAsync();
        }

        public async Task<bool> FrageNochNichtRichtigBeantwortet(long frageID)
        {

            /*
             * Es werden alle Beantwortungen nach Datum sortiert zu dieser
             * Frage geladen. Tatsächlich interessiert uns nur der erste Eintrag.
             */
            List<Quiz_Frage> fragen = await connection.Table<Quiz_Frage>()
                .Where(v => v.frageID == frageID)
                .OrderByDescending(v=>v.datum)
                .ToListAsync();

            /*
             * Wenn Frage in der Vergangenheit falsch oder richtig beantwortet wurde, 
             * dann wird dies ignoriert. Es zählt, ob die Frage beim letzten Mal
             * korrekt beantwortet wurde.
             */
            if (fragen.Count>0)
            {
                Quiz_Frage f = fragen[0];
                return !f.richtig_beantwortet;
            }



            return false;
        }
    }
}
