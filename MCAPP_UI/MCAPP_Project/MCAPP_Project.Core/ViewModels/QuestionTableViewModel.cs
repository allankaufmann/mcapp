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
            Tables = new ObservableCollection<QuestionTableViewModel>();
        }


        public ObservableCollection<QuestionTableViewModel> Tables { get; }






    }
}
