using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SQLite;

namespace MCAPP_UI.Models
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
        private long FrageId { get; set; }

        /*
         * Die eigentliche Fragestellung in Textform. 
         */

        private string Fragetext { get; set; }






    }
}