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
            await synchronisiereThemen();

            //await syncronisiereFragen();

            // Nach Syncronisation der DB wird zur Themenwahl navigiert.
            await navigationService.Navigate(typeof(ThemenwahlViewModel));
        }

        private async Task synchronisiereThemen()
        {
            try
            {
                List<Thema> themenWeb = await mcappwebservice.GetThemen();
                Console.WriteLine("erg: " + themenWeb.Count);

                Boolean refresh = false;

                foreach (Thema t in themenWeb)
                {
                    if (MCAPP_PROPERTIES.DEMO_MODUS)
                    {
                        fragenService.SaveThema(t);
                        refresh = true;
                        MCAPP_PROPERTIES.DEMO_MODUS = false;
                        continue;
                    }

                    Thema themaInDB = fragenService.GetThema(t.id);

                    if (themaInDB == null)
                    {
                        // Neues Thema
                        fragenService.SaveThema(t);
                        refresh = true;
                    }
                    else
                    {
                        // Update
                        if (!t.ThemaText.Equals(themaInDB.ThemaText))
                        {
                            themaInDB.ThemaText = t.ThemaText;
                            fragenService.SaveThema(t);
                        }
                        refresh = true;
                    }
                }

                if (refresh)
                {
                    fragenService.refreshData();
                }

            }
            catch (MCAPPWebserviceException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task syncronisiereFragen()
        {
            try
            {
                List<Frage> fragenWeb = await mcappwebservice.GetFragen();

                List<Frage> fragenLokal = fragenService.GetAllFragen();

                Dictionary<long, Frage> fragenLokalDict = new Dictionary<long, Frage>();
                foreach (Frage f in fragenLokal) {
                    fragenLokalDict.Add(f.FrageId, f);
                }


                Boolean refresh = false;

                foreach (Frage f in fragenWeb)
                {
                    if (MCAPP_PROPERTIES.DEMO_MODUS)
                    {
                        await fragenService.AddNewFrage(f);
                        refresh = true;
                        MCAPP_PROPERTIES.DEMO_MODUS = false;
                        continue;
                    }

                    if (!fragenLokalDict.ContainsKey(f.FrageId))
                    {
                        // neue Fragen
                        await fragenService.AddNewFrage(f);
                        refresh = true;

                    } else
                    {
                        // vorhandene Frage

                    }


                }

                if (refresh)
                {
                    fragenService.refreshData();
                }



            }
            catch (MCAPPWebserviceException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
}
