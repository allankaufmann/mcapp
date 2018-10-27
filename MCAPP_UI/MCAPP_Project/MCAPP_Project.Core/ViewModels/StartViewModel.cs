using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
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
        private readonly IFragenService fragenService;



        public StartViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.fragenService = Mvx.Resolve<IFragenService>();
            this.mcappwebservice = Mvx.Resolve<IMCAPPWebService>();
            synchronisiereDB();

        }


        public async Task synchronisiereDB()
        {
            try
            {
                List<Thema> themenWeb = await mcappwebservice.GetThemen();
                Console.WriteLine("erg: " + themenWeb.Count);

                foreach (Thema t in themenWeb)
                {
                    if (MCAPP_PROPERTIES.DEMO_MODUS)
                    {
                        fragenService.AddNewThema(t);
                        fragenService.refreshData();
                        MCAPP_PROPERTIES.DEMO_MODUS = false;
                        continue;
                    }



                    Thema themaInDB = fragenService.GetThema(t.id);

                    if (themaInDB==null)
                    {
                        // Neues Thema
                        fragenService.AddNewThema(t);
                    } else
                    {
                        // Update
                        Console.WriteLine(t.ThemaID + " ist vorhanden");
                    }
                    fragenService.refreshData();


                }                
            }
            catch (MCAPPWebserviceException ex)
            {
                Console.WriteLine(ex.ToString());
            }



            // Nach Syncronisation der DB wird zur Themenwahl navigiert.
            await navigationService.Navigate(typeof(ThemenwahlViewModel));
        }



    }
}
