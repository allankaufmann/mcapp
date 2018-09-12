using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCAPP_CROSS.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MCAPP_CROSS.Core.Services
{
    public interface IFragenService
    {
        Task AddNewFrage(Frage frage);

        Task<List<Frage>> GetAllFragen();

        Frage GetSampleFrage();


    }
}