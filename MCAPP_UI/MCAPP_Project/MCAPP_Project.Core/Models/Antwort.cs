using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SQLite;

namespace MCAPP_Project.Core.Models
{
    public abstract class Antwort
    {

        /*
         * ID der Frage. Dient der Identifikation der Frage sowohl client- als auch serverseitig.
         */
        //[PrimaryKey, AutoIncrement]
        private long AntwortID { set; get; }

        /*
         * Markiert Antwort als 'wahre Aussage'
         */
        private Boolean wahr;


    }
}