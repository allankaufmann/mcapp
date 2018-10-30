using MCAPP_Project.Core.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Models
{
    public class Quiz
    {

        [PrimaryKey, AutoIncrement]
        public long id { get; set; }

        public DateTime datum { get; set; }

        [Ignore]
        public List<Frage> fragen { get; set; }

        [Ignore]
        public int position { get; set; }

        [Ignore]
        public List<Thema> gewaelteThemen { get; set; }

        [Ignore]
        public Dictionary<Thema, String> AuswertungsTexte { get; set; }

        [Ignore]
        public Dictionary<long, bool> FrageRichtigBeantwortet { get; set; }

        [Ignore]
        public Boolean ended { get; set; }


        public Quiz()
        {
            AuswertungsTexte = new Dictionary<Thema, string>();
            FrageRichtigBeantwortet = new Dictionary<long, bool>();
        }


        /**
         * Liefert zu einem Thema alle gezogenen Fragen.
         */
        public List<Frage> fragenZuThema(Thema thema)
        {
            List<Frage> fragen = new List<Frage>();

            foreach (Frage f in this.fragen)
            {
                if (f.thema_id==thema.id)
                {
                    fragen.Add(f);
                }
            }
            return fragen;
        }


    }
}
