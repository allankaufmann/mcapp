using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Repositories
{
    public interface IQuizRepository
    {

        Quiz Save(Quiz quiz);

        Quiz Update(Quiz quiz);

        Quiz_Frage SaveQuiz_Frage(Quiz_Frage quizFrage);

        Task<bool> FrageNochNichtRichtigBeantwortet(long frageID);

        List<Quiz> GetAllQuizNotSendet();

        /**
         * Liefert alle Quiz_Frage-Objektes zu einem Quiz-Objekt.
         */
        List<Quiz_Frage> GetAllQuiz_Frages(Quiz quiz);


    }
}
