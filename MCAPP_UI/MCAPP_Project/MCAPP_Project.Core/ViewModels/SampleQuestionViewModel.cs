using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class SampleQuestionViewModel : MvxViewModel
    {

        IFragenService service;

        Frage aktuelleFrage;


        public SampleQuestionViewModel()
        {
            service = new FrageService(new DummyFragenRepository());
            aktuelleFrage = service.GetSampleFrage();
        }

        public string FrageText
        {
            get { return aktuelleFrage.Fragetext; }
        }



        string hello = "Meine Frage";
        public string Hello
        {
            get { return hello; }
            set { SetProperty(ref hello, value); }
        }
    }
}
