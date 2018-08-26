using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MCAPP_UI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MCAPP_UI.Services
{
    public interface IFragenService
    {
        Task<Frage> AddNewFrage(string frageText);

        Task<List<Frage>> GetAllFragen();
    }
}