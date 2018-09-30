using MCAPP_Project.Core.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class AuswertungThemaViewModel : MvxViewModel
    {

        private Thema thema;

        public AuswertungThemaViewModel(Thema thema)
        {
            this.thema = thema;
        }

        public Thema Thema
        { get { return this.thema; } }

    }
}
