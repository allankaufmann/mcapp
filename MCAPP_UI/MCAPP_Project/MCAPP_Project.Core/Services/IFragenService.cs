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

        List<Frage> GetAllFragen();

        /*
        * Liefert eine Beispielfrage.
        */
        Frage GetSampleFrage();

        /**
         * Liefert Liste aller vorhandenen Themen.
         */
        List<Thema> GetAllThemen();

        /**
         * Liefert alle Fragen, die einem bestimmten Thema zugeordnet sind. 
         */
        List<Frage> GetFragen(long themaID);

        /**
         * Liefert Fragen, zu mehreren Themen.
         */
        List<Frage> GetFragen(List<Thema> gewaelteThemen);

        /**
         * Liefert Fragen zu gewählten Themen, nach Thema gruppiert.
         */
        Dictionary<long, List<Frage>> GetFragenDictionary(List<Thema> gewaelteThemen);

        /**
         * Liefert Liste mit fester Anzahl an Fragen zu bestimmten Themen.
         */

        Task<List<Frage>> GetFragen(List<Thema> gewaelteThemen, int anz);

        /**
         * Liefert nach Zufallsprinzip Fragen eines bestimmten Themas.
         */
        Task<List<Frage>> GetZufallsFragen(Thema thema, int anz);

        /**
         * Liefert Themaobjekt zu ThemaID.
         */
        Thema GetThema(long themaID);

        /**
         * Liefert zu einer Liste von Themen eine 
         * Feld, welches ermittelt, wieviele Fragen zu einem Thema
         * gezogen werden können. 
         */
        int[] CalcAnzahlProThema(List<Thema> gewaelteThemen, int anz);
    }
}