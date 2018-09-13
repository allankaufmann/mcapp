using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MCAPP_Project.Core.Models;

namespace MCAPP_Project.Core.Repositories
{
    public class DummyFragenRepository : IFragenRepository
    {
        public Task<List<Frage>> GetAllFragen()
        {
            throw new NotImplementedException();
        }

        public Frage GetSampleFrage()
        {
            Frage beispiel = new Frage();
            beispiel.Fragetext = "Wie hoch ist die MWSt in Deutschland?";
            return beispiel;
        }

        public Task Save(Frage frage)
        {
            throw new NotImplementedException();
        }
    }
}
