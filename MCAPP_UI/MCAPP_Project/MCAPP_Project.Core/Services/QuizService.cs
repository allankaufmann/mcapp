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
                    quiz.FrageRichtigBeantwortet.Add(f.FrageId, true);
                    anzahlRichtig++;
                } else
                {
                    quiz.FrageRichtigBeantwortet.Add(f.FrageId, false);
                }

                Quiz_Frage quizFrage = new Quiz_Frage();
                quizFrage.quizID = quiz.quizID;
                quizFrage.frageID = f.FrageId;
                quizFrage.richtig_beantwortet = frageRichtig;
                quizRepo.SaveAntwort(quizFrage);

            }

            text = anzahlRichtig + "/" + fragen.Count + " richtig beantwortet!";

            return text;
        }



        public async Task<Quiz> CreateQuiz()
        {
            Quiz quiz = new Quiz();
            quiz.datum = DateTime.Now;
            await quizRepo.Save(quiz);
            


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


    }
}
