using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Wrapper;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class AuswertungThemaViewModel : MvxViewModel
    {

        private Thema thema;

        private FragenWrapper wrapper;

        public AuswertungThemaViewModel(Thema thema, FragenWrapper wrapper)
        {
            this.thema = thema;
            this.wrapper = wrapper;
        }

        public String auswertungText
        {
            get
            {
                String text = "";

                List<Frage> fragen = wrapper.fragenZuThema(thema);

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
