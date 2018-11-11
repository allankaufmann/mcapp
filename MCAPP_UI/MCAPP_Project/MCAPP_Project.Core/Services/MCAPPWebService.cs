using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.Services
{
    public interface IMCAPPWebService
    {
        Task<List<Thema>> GetThemen();

        Task<List<Frage>> GetFragen();

        Task<Boolean> isAlive();

        Task<Boolean> sendQuizauswertung(Quiz quiz, List<Quiz_Frage> quizfragen);

    }

    public class MCAPPWebService : IMCAPPWebService
    {
        readonly IMCAPPWebRepositorie repo;

        public MCAPPWebService(IMCAPPWebRepositorie repo)
        {
            this.repo = repo;
        }

        public Task<List<Frage>> GetFragen()
        {
            return repo.GetFragen();
        }

        public Task<List<Thema>> GetThemen()
        {
            return repo.GetThemen();
        }

        public Task<bool> isAlive()
        {
            return repo.isAlive();
        }

        public Task<bool> sendQuizauswertung(Quiz quiz, List<Quiz_Frage> quizfragen)
        {
            return repo.sendQuizauswertung(quiz, quizfragen);
        }
    }

}
