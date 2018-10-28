using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SQLite;


namespace MCAPP_Project.Core.Models
{



    /*
     * Modellklasse für eine Frage der MC-App.
     */

    public class Frage
    {

        /*
         * ID der Frage. Dient der Identifikation der Frage sowohl client- als auch serverseitig.
         */
        [PrimaryKey, AutoIncrement]
        public long id { get; set; }


        public string Fragetext { get; set; }

        [Ignore]
        public List<Textantwort> antworten { get; set; }

        /*
         * Für die lokale DB wird der primitive Wert benötigt, 
         * für die Webservice hingegen das Thema-Objekt. Daher wird hier
         * der Getter-verwendet.
         */
        public long thema_id {
            get; set; 
        }

        [Ignore]
        public Thema thema { get; set; }

    }
}