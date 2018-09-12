using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace MCAPP_Project.Droid
{
    [Activity(
        Label = "MCAPP_Project"
        , MainLauncher = true
        , Icon = "@mipmap/ic_launcher"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
