using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAPP_Project.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MCAPP_Project.Core.Services
{
    public interface IFragenService
    {
        Task AddNewFrage(Frage frage);

        Task<List<Frage>> GetAllFragen();

        Frage GetSampleFrage();

        List<Thema> GetAllThemen();

    }
}