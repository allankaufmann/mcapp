using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.Models
{
    public class Thema
    {
        private String themaText;

        private Boolean themaGewaehlt = false;


        public String ThemaText
        {
            get { return this.themaText; }
            set { this.themaText = value; }
        }

        public Boolean ThemaGewaehlt
        {
            get { return this.themaGewaehlt; }
            set { this.themaGewaehlt = value; }
        }


    }
}
