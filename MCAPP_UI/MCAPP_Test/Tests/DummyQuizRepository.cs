using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;

namespace MCAPP_Test.Tests
{
    public class DummyQuizRepository : IQuizRepository
    {
        int quizCounter = 0;

        public Task<bool> FrageNochNichtRichtigBeantwortet(long frageID)
        {
            Boolean nichtBeantwortet = false;

            if (frageID == 1 ||
                frageID == 3 ||
                frageID == 5 ||
                frageID == 7 ||
                frageID == 9 ||
                frageID ==11)
            {
                nichtBeantwortet = true;
            }


            Task<bool> t = Task.FromResult<bool>(nichtBeantwortet);
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
