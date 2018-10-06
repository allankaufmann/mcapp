using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Models
{
    public class Quiz
    {

        public long quizID { get; set; }

        public DateTime datum { get; set; }

        public List<Frage> fragen { get; set; }

        public int position { get; set; }

        public List<Thema> gewaelteThemen { get; set; }

        public Dictionary<Thema, String> AuswertungsTexte { get; set; }

        public Quiz()
        {
            AuswertungsTexte = new Dictionary<Thema, string>();
        }


        /**
         * Liefert zu einem Thema alle gezogenen Fragen.
         */
        public List<Frage> fragenZuThema(Thema thema)
        {
            List<Frage> fragen = new List<Frage>();

            foreach (Frage f in this.fragen)
            {
                if (f.themaID==thema.ThemaID)
                {
                    fragen.Add(f);
                }
            }
            return fragen;
        }


    }
}
