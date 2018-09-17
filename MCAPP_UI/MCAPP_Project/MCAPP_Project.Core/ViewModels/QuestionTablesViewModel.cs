using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{
    public class QuestionTablesViewModel : MvxViewModel
    {
        public QuestionTablesViewModel()
        {
            Tables = new ObservableCollection<QuestionTableViewModel>();
            Tables.Add(new QuestionTableViewModel());
        }


        public ObservableCollection<QuestionTableViewModel> Tables { get; }


    }
}
