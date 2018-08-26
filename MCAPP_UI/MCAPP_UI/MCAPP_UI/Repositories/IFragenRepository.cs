using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using MCAPP_UI.Models;

namespace MCAPP_UI.Repositories
{
    public interface IFragenRepository
    {

        Task<List<Frage>> GetAllFragen(); 



    }
}