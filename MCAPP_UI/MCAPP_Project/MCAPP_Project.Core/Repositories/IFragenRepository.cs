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

        /**
         * Lädt alle hinterlegten Themen aus 
         * lokaler DB.
         */
        Boolean loadThemen();

        /**
         * Lädt sämtliche hinterlegten Fragen.
         */
        Boolean loadFragen();

        /**
         * Liefert Liste aller vorhandenen Themen.
         */
        List<Thema> GetAllThemen();

        // Quatsch
        List<Frage> GetAllFragen();

        // Quatsch
        Task Save(Frage frage);

        /*
         * Liefert eine Beispielfrage.
         */
        Frage GetSampleFrage();

        
        /**
         * Liefert alle vorhandenen Fragen. 
         */
        List<Frage> GetAlleFragen();

        /**
         * Liefert Liste aller Fragen zu einem bestimmten Thema. 
         */
        List<Frage> GetFragen(long themaID);

    }
}