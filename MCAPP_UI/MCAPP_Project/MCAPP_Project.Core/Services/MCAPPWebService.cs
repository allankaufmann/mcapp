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


    }

    public class MCAPPWebService : IMCAPPWebService
    {
        readonly IMCAPPWebRepositorie repo;

        public MCAPPWebService(IMCAPPWebRepositorie repo)
        {
            this.repo = repo;
        }


        public Task<List<Thema>> GetThemen()
        {
            return repo.GetThemen();
        }
    }

}
