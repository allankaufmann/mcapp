using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Utils
{
    public class MCAPPUtils
    {
        public static Dictionary<long, Frage> convertFrageListToDictionary(List<Frage> fragenList)
        {
            Dictionary<long, Frage> fragenDict = new Dictionary<long, Frage>();
            foreach (Frage f in fragenList)
            {
                if (!fragenDict.ContainsKey(f.id))
                {
                    fragenDict.Add(f.id, f);
                }                
            }

            return fragenDict;
        }

        public static Dictionary<long, Thema> convertThemaListToDictionary(List<Thema> themaList)
        {
            Dictionary<long, Thema> themaDict = new Dictionary<long, Thema>();
            foreach (Thema f in themaList)
            {
                if (!themaDict.ContainsKey(f.id))
                {
                    themaDict.Add(f.id, f);
                }
            }

            return themaDict;
        }



    }
}
