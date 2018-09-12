using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//using SQLite;

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
   //     [PrimaryKey, AutoIncrement]
        private long FrageId { get; set; }


        public string Fragetext { get; set; }






    }
}