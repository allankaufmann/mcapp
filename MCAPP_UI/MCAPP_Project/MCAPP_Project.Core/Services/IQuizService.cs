using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Services
{
    interface IQuizService
    {
        /*
         * Liefert neue Quiz-Instanz. 
         */
        Task<Quiz> CreateQuiz();

        /*
         * Wertet nach Beendigung des Quiz die eingegebenen Antworten aus
         * und erstellt Auwertungstext zu einem Thema. 
         */
        Quiz CreateAuswertung(Thema theme, Quiz quiz);

        /*
         * Prüft, ob eine einzelne Frage korrekt beantwortet wurde. 
         */
        Boolean FrageRichtigBeantwortet(Frage frage);

    }
}
