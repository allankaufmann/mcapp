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
         * Liefert Liste aller vorhandenen Themen.
         */
        List<Thema> GetAllThemen();

        
        int SaveFrage(Frage frage);

        int SaveThema(Thema thema);

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

        int LoescheFrage(Frage frage);

        int LoescheThema(Thema t);
    }
}