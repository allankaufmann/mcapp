using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class ThemenwahlViewModel : MvxViewModel
    {
        IFragenService fragenService;

        List<Thema> themenListe;

        public ObservableCollection<ThemaViewModel> Tables { get; }


        public ThemenwahlViewModel()
        {
            this.Tables = new ObservableCollection<ThemaViewModel>();
            this.fragenService = new FrageService(new DummyFragenRepository());
            this.themenListe = fragenService.GetAllThemen();   

            foreach(Thema t in themenListe)
            {
                Tables.Add(new ThemaViewModel(t));
            }
        }

    }
}
