using MCAPP_Project.Core.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class ThemaViewModel : MvxViewModel,  INotifyPropertyChanged
    {
        private Thema thema;

        public ThemaViewModel()
        {

        }


        public ThemaViewModel(Thema thema)
        {
            this.thema = thema;
        }

        public String ThemaText
        {
            get { return this.thema.ThemaText; }
        }

        public Boolean ThemaGewaehlt
        {
            get { return this.thema.ThemaGewaehlt; }
            set { this.thema.ThemaGewaehlt = value; RaisePropertyChanged(); }
        }


    }
}
