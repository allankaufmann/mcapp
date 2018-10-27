using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MCAPP_Project.Core.ViewModels
{
    public class StartViewModel : MvxViewModel
    {
        readonly IMvxNavigationService navigationService;

        private readonly IMCAPPWebService mcappwebservice;


        public StartViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.mcappwebservice = Mvx.Resolve<IMCAPPWebService>();
            doSomething();

        }


        public async Task doSomething()
        {
            try
            {
                List<Thema> liste = await mcappwebservice.GetThemen();
                Console.WriteLine("erg: " + liste.Count);
            }
            catch (MCAPPWebserviceException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            await navigationService.Navigate(typeof(ThemenwahlViewModel));


        }



    }
}
