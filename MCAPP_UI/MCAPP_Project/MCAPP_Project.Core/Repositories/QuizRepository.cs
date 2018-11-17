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
        //readonly SQLiteAsyncConnection connection;
        readonly SQLiteConnection connection;

        public QuizRepository()
        {
            // Später wird auf LocalStorage umgestellt, für
            // Entwicklung ist es aber mühseelig immer den Pfad auf dem
            // Geärt herauszusuchen, daher wird erstmal unter /Users gespeichert!
            
            //var local = FileSystem.Current.LocalStorage.Path;
            //var datafile = PortablePath.Combine(local, "test2.db");
            //connection = new SQLiteAsyncConnection(datafile);

            connection = new SQLiteConnection("/Users/allan/test.db");

            
            connection.CreateTable<Quiz_Frage>();
            connection.CreateTable<Quiz>();
        }

        public Quiz Save(Quiz quiz)
        {
                        try
            {
                connection.Insert(quiz);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
                                   return quiz;
        }

        public Quiz_Frage SaveQuiz_Frage(Quiz_Frage quizFrage)
        {
            connection.Insert(quizFrage);
            return quizFrage;
        }


        /*public Task<List<Quiz>> GetAll()
        {
            return connection.Table<Quiz>().ToList();
        }*/

        public async Task<bool> FrageNochNichtRichtigBeantwortet(long frageID)
        {

            /*
             * Es werden alle Beantwortungen nach Datum sortiert zu dieser
             * Frage geladen. Tatsächlich interessiert uns nur der erste Eintrag.
             */
            List<Quiz_Frage> fragen = connection.Table<Quiz_Frage>()
                .Where(v => v.frageID == frageID)
                .OrderByDescending(v=>v.datum)
                .ToList();

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

        public List<Quiz> GetAllQuizNotSendet()
        {
            List<Quiz> quizList = new List<Quiz>();

            try
            {
                quizList = connection.Table<Quiz>()
                .Where(v => v.auswertung_gesendet == false)
                .OrderByDescending(v => v.datum)
                .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return quizList;
        }

        public Quiz Update(Quiz quiz)
        {
            try
            {
                connection.Update(quiz);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return quiz;
        }

        public List<Quiz_Frage> GetAllQuiz_Frages(Quiz quiz)
        {
            List<Quiz_Frage> frageList = new List<Quiz_Frage>();
            try
            {
                frageList = connection.Table<Quiz_Frage>()
                    .Where(v => v.quizID == quiz.id)
                    .ToList();

            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return frageList;
        }

        public int LoescheAuswertungFragen(Frage frage)
        {
            int loeschen = 0;

            List<Quiz_Frage> fragen = connection.Table<Quiz_Frage>()
            .Where(v => v.frageID == frage.id)
            .ToList();

            try
            {
                foreach (Quiz_Frage f in fragen)
                {
                    loeschen = connection.Delete(f);
                }
            }
            catch (Exception e)
            {
                loeschen = 0;
            }

            return loeschen;                      
        }
    }
}
