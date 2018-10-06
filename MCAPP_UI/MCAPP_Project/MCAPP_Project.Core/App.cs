using MCAPP_Project.Core.Repositories;
using MCAPP_Project.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace MCAPP_Project.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //RegisterNavigationServiceAppStart<ViewModels.SampleQuestionViewModel>();
            //RegisterNavigationServiceAppStart<ViewModels.QuestionTableViewModel>();
            RegisterNavigationServiceAppStart<ViewModels.ThemenwahlViewModel>();
            Mvx.RegisterType<IFragenRepository, DummyFragenRepository>();
            Mvx.RegisterType<IQuizService, QuizService>();
            
            
            //
        }
    }
}
