using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTableViewModel : MvxViewModel<List<Thema>>
    {
        private IFragenRepository repo;

        readonly IMvxNavigationService navigationService;

        // Sollte nach Navigation gesetzt werden...
        List<Thema> gewaelteThemen;


        public QuestionTableViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            repo = new DummyFragenRepository();

            Tables = new ObservableCollection<QuestionViewModel>();





        }


        public ObservableCollection<QuestionViewModel> Tables { get; }

        public override void Prepare(List<Thema> parameter)
        {
            this.gewaelteThemen = parameter;

            Frage frage;

            if (gewaelteThemen != null && gewaelteThemen.Count > 0)
            {
                frage = repo.GetFragen(gewaelteThemen.ToArray()[0].ThemaID).ToArray()[0];
            }
            else
            {
                frage = repo.GetSampleFrage();
            }



            Tables.Add(new QuestionViewModel(frage));
            Tables.Add(new QuestionViewModel(frage));

            if (frage.antworten != null)
            {
                foreach (Textantwort a in frage.antworten)
                {
                    Tables.Add(new QuestionViewModel(frage, a));

                }
            }


        }
    }
}
