using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;

namespace MCAPP_Project.Core.Repositories
{
    public interface IFragenRepository
    {

        Task<List<Frage>> GetAllFragen();
        Task Save(Frage frage);

        Frage GetSampleFrage();

        List<Thema> GetAllThemen();

        List<Frage> GetAlleFragen();


    }
}