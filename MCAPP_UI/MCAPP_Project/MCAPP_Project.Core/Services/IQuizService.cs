using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Services
{
    public interface IQuizService
    {
        /*
         * Liefert neue Quiz-Instanz. 
         */
        Quiz CreateQuiz();

        /*
         * Wertet nach Beendigung des Quiz die eingegebenen Antworten aus
         * und erstellt Auwertungstext zu einem Thema. 
         */
        Quiz CreateAuswertung(Thema theme, Quiz quiz);

        /*
         * Prüft, ob eine einzelne Frage korrekt beantwortet wurde. 
         */
        Boolean FrageRichtigBeantwortet(Frage frage);

        /*
         * Prüft, ob Frage bereits beantwortet wurde bzw. ob 
         * Frage beim letzten Mal falsch beantwortet wurde.
         */
        Task<bool> FrageNochNichtRichtigBeantwortet(long frageID);

    }
}
