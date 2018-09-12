using MvvmCross.Platform.IoC;

namespace MCAPP_CROSS.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            
            
            RegisterNavigationServiceAppStart<ViewModels.FirstViewModel>();
            //RegisterNavigationServiceAppStart<ViewModels.SampleQuestionViewModel>();
            
        }
    }
}
