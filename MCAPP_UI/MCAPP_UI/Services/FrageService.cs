using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using MCAPP_UI.Models;
using UIKit;
using MCAPP_UI.Repositories;

namespace MCAPP_UI.Services
{
    class FrageService : IFragenService
    {
        readonly IFragenRepository repository;

        public FrageService(IFragenRepository repository)
        {
            this.repository = repository;
        }

        public Task<Frage> AddNewFrage(string frageText)
        {
            throw new NotImplementedException();
        }

        public Task<List<Frage>> GetAllFragen()
        {
            return repository.GetAllFragen();
        }
    }
}