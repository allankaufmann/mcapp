
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Models
{
    public class Thema
    {
       

        [PrimaryKey, AutoIncrement]
        public long ThemaID
        {
            get; set;
        }


        public String ThemaText
        {
            get; set;
        }

        [Ignore]
        public Boolean ThemaGewaehlt
        {
            get; set;
        }


    }
}
