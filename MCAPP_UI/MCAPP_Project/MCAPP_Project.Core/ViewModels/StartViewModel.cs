using MCAPP_Project.Core.Models;
using MCAPP_Project.Core.Services;
using MCAPP_Project.Core.Utils;
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
        private readonly IQuizService quizService;



        public StartViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.fragenService = Mvx.Resolve<IFragenService>();
            this.mcappwebservice = Mvx.Resolve<IMCAPPWebService>();
            this.quizService = Mvx.Resolve<IQuizService>();
            
            synchronisiereDB();           
        }

 
            public async Task synchronisiereDB()
        {

            Boolean webserviceOnline = await mcappwebservice.isAlive();

            if (webserviceOnline)
            {
                await synchronisiereThemen();

                // TODO: Bei gelöschten Fragen müssen Quiz_Fragen aufgeräumt werden -> sonst Exception ;)
                await syncronisiereFragen();

                await synchronisiereAuswertung();
            } 

            // Nach Syncronisation der DB wird zur Themenwahl navigiert.
            await navigationService.Navigate(typeof(ThemenwahlViewModel));
        }

        private async Task synchronisiereAuswertung()
        {
            try
            {
                List<Quiz> quizListe = this.quizService.GetAllQuizNotSendet();

                foreach(Quiz q in quizListe)
                {
                    try
                    {

                        List<Quiz_Frage> quizfragen = this.quizService.GetAllQuiz_Frages(q);

                        await this.mcappwebservice.sendQuizauswertung(q, quizfragen);  

                        q.auswertung_gesendet = true;
                        quizService.UpdateQuiz(q);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch (MCAPPWebserviceException ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

                List<Frage> fragenInDB = fragenService.GetAllFragen();

                Dictionary<long, Frage> fragenInDBDict = MCAPPUtils.convertListToDictionary(fragenInDB);

                Boolean refresh = false;

                foreach (Frage f in fragenWeb)
                {
                    if (MCAPP_PROPERTIES.DEMO_MODUS)
                    {
                        fragenService.SaveFrage(f);
                        refresh = true;
                        MCAPP_PROPERTIES.DEMO_MODUS = false;
                        continue;
                    }

                    // Frage wird gespeichert, unabhängig ob vorhanden oder nicht
                    fragenService.SaveFrage(f);
                    refresh = true;

                    // Wir nehmen jede verarbeite Frage aus der Map heraus. 
                    // Damit wird später geprüft, ob inzwischen Fragen gelöscht wurden.
                    fragenInDBDict.Remove(f.id);
                }

                foreach (Frage f in fragenInDBDict.Values)
                {                    
                    fragenService.LoescheFrage(f);
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
}
