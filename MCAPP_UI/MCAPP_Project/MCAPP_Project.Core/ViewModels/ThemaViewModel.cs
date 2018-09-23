using MCAPP_Project.Core.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class ThemaViewModel : MvxViewModel
    {
        private Thema thema;

        private IMvxAsyncCommand startCommand;

        public ThemaViewModel(IMvxAsyncCommand startCommand)
        {
            this.startCommand = startCommand;
        }


        public ThemaViewModel(IMvxAsyncCommand startCommand, Thema thema)
        {
            this.startCommand = startCommand;
            this.thema = thema;
        }

        public String ThemaText
        {
            get { return this.thema.ThemaText; }
        }

        public Boolean ThemaGewaehlt
        {
            get { return this.thema.ThemaGewaehlt; }
            set {
                this.thema.ThemaGewaehlt = value;
                this.startCommand.RaiseCanExecuteChanged();
            }
        }

        public IMvxAsyncCommand StartQuizCommand { get { return this.startCommand; } }

    }
}
