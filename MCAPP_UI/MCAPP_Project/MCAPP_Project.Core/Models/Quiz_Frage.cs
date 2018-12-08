using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Models
{

    /**
     * Klasse zeigt Zusammenhang zwischne einem Quiz und einer Frage ab und wird
     * dafür verwendet um festzuhalten, welche Fragen bereits korrekt oder falsch
     * beantwortet wurden. 
     */
    public class Quiz_Frage
    {
        [PrimaryKey, AutoIncrement]
        public long quiz_Frage_ID { get; set; }

        public long quizID { get; set; }

        public long frageID { get; set; }

        public bool richtig_beantwortet { get; set; }

        // Datum wird für Sortierung der Quiz_Frage-Liste verwendet, wenn Auswertung erstellt wird. Das führende Datum
        // steht jedoch im Quiz-Objekt. Nur das Datum aus dem Quiz-Objekt wird später an Server gesendet. 
        public DateTime datum { get; set; }
    }
}
