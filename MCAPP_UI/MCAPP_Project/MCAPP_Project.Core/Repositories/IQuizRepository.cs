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

        Quiz_Frage SaveQuiz_Frage(Quiz_Frage quizFrage);

        Task<List<Quiz>> GetAll();

        Task<bool> FrageNochNichtRichtigBeantwortet(long frageID);

    }
}
