using MCAPP_Project.Core.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class AuswertungThemaViewModel : MvxViewModel
    {

        private Thema thema;

        private Quiz quiz;

        public AuswertungThemaViewModel(Thema thema, Quiz quiz)
        {
            this.thema = thema;
            this.quiz = quiz;
        }

        public String auswertungText
        {
            get
            {
                String text = "";

                List<Frage> fragen = quiz.fragenZuThema(thema);

                int anzahlRichtig = 0;

                foreach (Frage f in fragen)
                {
                    Boolean fragerichtig = true;

                    foreach (Textantwort a in f.antworten)
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

                    if (fragerichtig)
                    {
                        anzahlRichtig++;
                    }
                }

                text = anzahlRichtig + "/" + fragen.Count + " richtig beantwortet!";

                return text;
            }

           
        }



        public Thema Thema
        { get { return this.thema; } }

    }
}
