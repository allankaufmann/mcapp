using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;

namespace MCAPP_Project.Core.Repositories
{
    public class DummyQuestionRepository : IQuizRepository
    {
        int quizCounter = 0;

        public Task<bool> FrageNochNichtRichtigBeantwortet(long frageID)
        {
            Task<bool> t = Task.FromResult<bool>(false);
            return t;
        }

        public Task<List<Quiz>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Save(Quiz quiz)
        {


            quiz.quizID = this.quizCounter++;

            Task<Quiz> t = Task.FromResult<Quiz>(quiz);
            return t;



        }

        public Task SaveQuiz_Frage(Quiz_Frage quizFrage)
        {
            throw new NotImplementedException();
        }
    }
}
