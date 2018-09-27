using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Wrapper;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTableViewModel : MvxViewModel<FragenWrapper>
    {
        private IFragenRepository repo;

        readonly IMvxNavigationService navigationService;

        // Sollte nach Navigation gesetzt werden...

        FragenWrapper wrapper;

        public QuestionTableViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            repo = new DummyFragenRepository();

            Tables = new ObservableCollection<QuestionViewModel>();
        }


        public ObservableCollection<QuestionViewModel> Tables { get; }

        public override void Prepare(FragenWrapper parameter)
        {
            this.wrapper = parameter;

            Frage frage = parameter.fragen[wrapper.position];

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
