using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Services
{
    public class QuizService : IQuizService
    {
        readonly IFragenRepository repository;
        readonly IQuizRepository quizRepo;

        private static int quizCounter;


        public QuizService(IFragenRepository repository, IQuizRepository quizRepo)
        {
            this.repository = repository;
            this.quizRepo = quizRepo;
        }

        public Quiz CreateAuswertung(Thema thema, Quiz quiz)
        {
            quiz.AuswertungsTexte.Add(thema, createAuswertungText(thema, quiz));

            return quiz;
        }




        private String createAuswertungText(Thema thema, Quiz quiz)
        {
            String text = "";

            List<Frage> fragen = quiz.fragenZuThema(thema);

            int anzahlRichtig = 0;

            foreach (Frage f in fragen)
            {
                Boolean frageRichtig = FrageRichtigBeantwortet(f);

                if (frageRichtig)
                {
                    quiz.FrageRichtigBeantwortet.Add(f.id, true);
                    anzahlRichtig++;
                } else
                {
                    quiz.FrageRichtigBeantwortet.Add(f.id, false);
                }

                Quiz_Frage quizFrage = new Quiz_Frage();
                quizFrage.datum = DateTime.Now;
                quizFrage.quizID = quiz.id;
                quizFrage.frageID = f.id;
                quizFrage.richtig_beantwortet = frageRichtig;

                if (!MCAPP_PROPERTIES.DEMO_MODUS)
                {
                    quizRepo.SaveQuiz_Frage(quizFrage);
                }           
            }

            text = anzahlRichtig + "/" + fragen.Count + " richtig beantwortet!";

            return text;
        }



        public Quiz CreateQuiz()
        {
            Quiz quiz = new Quiz();
            quiz.datum = DateTime.Now;

            if(!MCAPP_PROPERTIES.DEMO_MODUS)
            {
                quizRepo.Save(quiz);
            } else
            {
                quiz.id = quizCounter++;
            }

            return quiz;
        }

        public bool FrageRichtigBeantwortet(Frage frage)
        {
            Boolean fragerichtig = true;

            foreach (Textantwort a in frage.antworten)
            {
                if (a.wahr && !a.Auswahl)
                {
                    fragerichtig = false;
                }
                else if (!a.wahr && a.Auswahl)
                {
                    fragerichtig = false;
                }
            }

            return fragerichtig;
        }

        public async Task<bool> FrageNochNichtRichtigBeantwortet(long frageID)
        {
            return await quizRepo.FrageNochNichtRichtigBeantwortet(frageID);
        }
    }
}
