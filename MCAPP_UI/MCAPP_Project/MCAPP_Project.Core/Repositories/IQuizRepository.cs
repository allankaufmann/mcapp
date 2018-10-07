using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Repositories
{
    public interface IQuizRepository
    {

        Task Save(Quiz quiz);

        Task SaveAntwort(Quiz_Frage quizFrage);

        Task<List<Quiz>> GetAll();

    }
}
