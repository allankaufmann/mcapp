using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTableViewModel : MvxViewModel
    {
        public QuestionTableViewModel()
        {
            Tables = new ObservableCollection<QuestionViewModel>();
            Tables.Add(new QuestionViewModel());
            Tables.Add(new QuestionViewModel());
        }


        public ObservableCollection<QuestionViewModel> Tables { get; }


    }
}
