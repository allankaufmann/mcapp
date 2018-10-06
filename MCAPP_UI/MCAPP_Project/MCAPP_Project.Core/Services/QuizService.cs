using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Services
{
    public class QuizService : IQuizService
    {
        readonly IFragenRepository repository;

        public QuizService(IFragenRepository repository)
        {
            this.repository = repository;
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
                    anzahlRichtig++;
                }
            }

            text = anzahlRichtig + "/" + fragen.Count + " richtig beantwortet!";

            return text;
        }



        public Quiz GetNewQuiz()
        {
            Quiz quiz = new Quiz();
            quiz.quizID = repository.GetNewQuizID();
            quiz.datum = new DateTime();

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
