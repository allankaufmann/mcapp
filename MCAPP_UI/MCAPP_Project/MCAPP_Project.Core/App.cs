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

            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterNavigationServiceAppStart<ViewModels.StartViewModel>();

            Mvx.RegisterType<IFragenRepository, FragenRepository>();
            Mvx.RegisterType<IQuizService, QuizService>();
            Mvx.RegisterType<IQuizRepository, QuizRepository>();
            Mvx.RegisterType<IMCAPPWebRepositorie, MCAPPWebRepositorie>();
            Mvx.RegisterType<IMCAPPWebService, MCAPPWebService>();
        }
    }
}
