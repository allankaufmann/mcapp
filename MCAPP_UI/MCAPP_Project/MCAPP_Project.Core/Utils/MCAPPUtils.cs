using MCAPP_Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Utils
{
    public class MCAPPUtils
    {
        public static Dictionary<long, Frage> convertListToDictionary(List<Frage> fragenList)
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

        

    }
}
