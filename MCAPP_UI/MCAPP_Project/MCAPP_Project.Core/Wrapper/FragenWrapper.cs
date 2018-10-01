using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Wrapper
{
    public class FragenWrapper
    {
        public List<Frage> fragen { get; set; }

        public int position { get; set; }

        public List<Thema> gewaelteThemen { get; set; }

        public List<Frage> fragenZuThema(Thema thema)
        {
            List<Frage> fragen = new List<Frage>();

            foreach (Frage f in this.fragen)
            {
                if (f.themaID==thema.ThemaID)
                {
                    fragen.Add(f);
                }
            }
            return fragen;
        }


    }
}
