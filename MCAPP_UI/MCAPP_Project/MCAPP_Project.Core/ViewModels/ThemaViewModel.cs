using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
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

        private readonly IFragenService fragenService;

        public ThemaViewModel(IMvxAsyncCommand startCommand)
        {
            this.startCommand = startCommand;
        }


        public ThemaViewModel(IMvxAsyncCommand startCommand, Thema thema, IFragenService fragenService)
        {
            this.startCommand = startCommand;
            this.thema = thema;
            this.fragenService = fragenService;
        }

        public String ThemaText
        {
            get {
                String s = "";
                s += this.thema.ThemaText;

                List<Frage> fragen = fragenService.GetFragen(this.thema.id);

                if (fragen!=null)
                {
                    s += " (";
                    s += fragen.Count;
                    s += " Fragen)";
                }
                return s; }
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

        private int anzahlFrage = 10;

        public int AnzahlFrage { get { return this.anzahlFrage; }
            set { this.anzahlFrage = value;
                RaisePropertyChanged(() => AnzahlFrage);
            }
        }

        public String anzahlText
        {
            get { return "max Anzahl Fragen: "; }
        }

    }
}
