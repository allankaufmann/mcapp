using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace MCAPP_Project.Core.Models
{
    public abstract class Antwort
    {

        private Boolean auswahl;

        /*
         * ID der Frage. Dient der Identifikation der Frage sowohl client- als auch serverseitig.
         */
        public long id { set; get; }

        /*
         * Markiert Antwort als 'wahre Aussage'
         */
        public Boolean wahr { set; get; }

        [Ignore]
        public Boolean Auswahl
        {
            get { return this.auswahl; }
            set {
                this.auswahl = value;
            }
        }

        [Ignore]
        public Frage frage { get; set; }

        public long frage_id
        {
            set { }
            get {
                if (this.frage==null)
                {
                    return 0;
                }
                return this.frage.id;
            }
        }


    }
}