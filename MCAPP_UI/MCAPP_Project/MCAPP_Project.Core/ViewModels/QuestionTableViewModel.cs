using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Repositories;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTableViewModel : MvxViewModel
    {
        private IFragenRepository repo;


        public QuestionTableViewModel()
        {
            repo = new DummyFragenRepository();

            Tables = new ObservableCollection<QuestionViewModel>();

            Frage frage = repo.GetSampleFrage();

            Tables.Add(new QuestionViewModel(frage));
            Tables.Add(new QuestionViewModel(frage));

            /*foreach (Textantwort a in frage.antworten) {
                Tables.Add(new QuestionViewModel(a));

            }*/
        }


        public ObservableCollection<QuestionViewModel> Tables { get; }


    }
}
