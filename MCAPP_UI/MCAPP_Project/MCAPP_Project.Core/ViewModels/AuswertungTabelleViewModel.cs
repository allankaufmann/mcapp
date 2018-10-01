using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Wrapper;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MCAPP_Project.Core.ViewModels
{

    public class AuswertungTabelleViewModel : MvxViewModel<FragenWrapper>
    {
        FragenWrapper wrapper;

        public ObservableCollection<AuswertungThemaViewModel> Tables { get; }

        public AuswertungTabelleViewModel()
        {
            Tables = new ObservableCollection<AuswertungThemaViewModel>();
        }

        public override void Prepare(FragenWrapper parameter)
        {
            this.wrapper = parameter;

            foreach (Thema t in wrapper.gewaelteThemen)
            {
                Tables.Add(new AuswertungThemaViewModel(t, parameter));
            }
        }
    }
}
