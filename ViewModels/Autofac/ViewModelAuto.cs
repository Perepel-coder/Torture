using Autofac;
using Services;
using Services.Autofac;
using Services.Crypto;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using ViewModels.Interfaces;
using ViewModels.Models;
using ViewModels.Models.Script;
using ViewModels.Models.Tasks;
using ViewModels.Models.Tutor;
using ViewModels.Models.Tutor.TrainModule;
using ViewModels.Models.Tutor.UserDB;
using ViewModels.Researcher;
using ViewModels.Researcher.Script;
using ViewModels.Researcher.Tasks;
using ViewModels.Tutor;
using ViewModels.Tutor.TrainModule;
using ViewModels.Tutor.UserDB;

namespace ViewModels.Autofac
{
    public class ViewModelAuto : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceAuto());

            builder.RegisterType<CryptoComparison_Controls>().AsSelf();
            builder.RegisterType<CypherData_Controls>().AsSelf();
            builder.RegisterType<CombLock_Controls>().AsSelf();
            builder.RegisterType<Rocket_Controls>().AsSelf();
            builder.RegisterType<TrainInform_Controls>().AsSelf();
            builder.RegisterType<UserMW_Controls>().AsSelf();
            builder.RegisterType<MainMenu_Task_Controls>().AsSelf();
            builder.RegisterType<Script_MainMenu_Controls>().AsSelf();
            builder.RegisterType<Test_Controls>().AsSelf();
            builder.RegisterType<TaskScript_Panel_Controls>().AsSelf();
            builder.Register(c => new CypherData_Controls(c.Resolve<CypherTools>())).AsSelf();
            builder.RegisterType<TutorMW_Controls>();
            builder.RegisterType<UserDB_Controls>();
            builder.RegisterType<UserInfo_Controls>();
            builder.RegisterType<UserInfoTest_Controls>();
            builder.RegisterType<AddGroup_Controls>();
            builder.RegisterType<KeySelect_Controls>();
            builder.RegisterType<CreatScrip_Controls>();
            builder.RegisterType<CreatQuestion_Controls>();
            builder.RegisterType<LookDetails_Controls>();
            builder.RegisterType<CreatTask_Controls>();
            builder.RegisterType<Seting_Controls>();
            builder.RegisterType<Registration_Controls>();
            builder.RegisterType<CreatTopic_Controls>();


            builder.Register(c => new Authorization_VM(
                c.Resolve<IUserService>()))
                .As<BaseViewModel>()
                .As<IAuthorization<string>>()
                .AsSelf();
            builder.Register(c => new CryptoComparison_VM(
                c.Resolve<ICryptoAlgService>(),
                c.Resolve<CryptoComparison_Controls>(),
                c.Resolve<CryptoStopwatchService>())
            ).AsSelf();
            builder.Register(c => new CypherData_VM(
                c.Resolve<CypherData_Controls>(),
                c.Resolve<DialogService>(),
                c.Resolve<BlockEncryption>(),
                c.Resolve<ICryptoAlgService>())
            ).AsSelf();
            builder.Register((c, p) => new CombLock_VM(
                c.Resolve<CombLock_Controls>(),
                p.Named<ATask>("Task")))
                .AsSelf();
            builder.Register((c, p) => new Rocket_VM(
                c.Resolve<Rocket_Controls>(),
                p.Named<ATask>("Task")))
                .AsSelf();
            builder.Register((c, p) => new KeySelect_VM(
                c.Resolve<KeySelect_Controls>(),
                c.Resolve<ICryptoAlgService>(),
                c.Resolve<BlockEncryption>(),
                p.Named<ATask>("Task")))
                .AsSelf();
            builder.Register((c, p) => new UserMW_VM(
                c.Resolve<UserMW_Controls>()))
                .AsSelf();
            builder.Register((c, p) => new MainMenu_Task_VM (
                c.Resolve<MainMenu_Task_Controls>(),
                c.Resolve<ITrainModuleService>())
            ).AsSelf();
            builder.Register((c, p) => new TaskScript_Panel_VM(
                c.Resolve<TaskScript_Panel_Controls>(),
                c.Resolve<ITrainModuleService>(),
                c.Resolve<IUserService>(),
                p.Named<Script_S>("Script"),
                p.Named<ScriptUser_S>("UserScript")))
                .AsSelf();
            builder.Register((c, p) => new Test_VM(
                c.Resolve<Test_Controls>(),
                c.Resolve<IUserService>(),
                p.Named<Script_S>("Script")))
                .AsSelf();
            builder.Register((c, p) => new TrainInform_VM(
               c.Resolve<TrainInform_Controls>(),
               p.Named<Script_S>("Script")))
                .AsSelf();
            builder.Register(c => new Script_MainMenu_VM(
                c.Resolve<Script_MainMenu_Controls>(),
                c.Resolve<ITrainModuleService>())
            ).AsSelf();

            builder.Register((c, p) => new TutorMW_VM(
                c.Resolve<TutorMW_Controls>())
            ).AsSelf();
            builder.Register((c, p) => new UserDB_VM(
               c.Resolve<UserDB_Controls>(),
               c.Resolve<IUserService>())
            ).AsSelf();
            builder.Register((c, p) => new UserInfo_VM(
                c.Resolve<UserInfo_Controls>(),
                c.Resolve<IUserService>(),
                p.Named<Researcher_S>("Researcher"))
            ).AsSelf();
            builder.Register((c, p) => new UserInfoTest_VM(
                c.Resolve<UserInfoTest_Controls>(),
                p.Named<ScriptUser_S>("Script"))
            ).AsSelf();
            builder.Register(c => new AddGroup_VM(
                c.Resolve<AddGroup_Controls>(),
                c.Resolve<IUserService>())
            ).AsSelf();
            builder.Register((c, p) => new ChangeGroup_VM(
                c.Resolve<AddGroup_Controls>(),
                c.Resolve<IUserService>(),
                p.Named<Group_S>("Group"))
            ).AsSelf();
            builder.Register(c => new CreatScript_VM(
               c.Resolve<CreatScrip_Controls>(),
               c.Resolve<DialogService>(),
               c.Resolve<ITrainModuleService>())
            ).AsSelf();
            builder.Register((c, p) => new LookAnswers_VM(
                p.Named<Question_S>("Question"))
            ).AsSelf();
            builder.Register(c => new CreatQuestion_VM(
               c.Resolve<CreatQuestion_Controls>(),
               c.Resolve<ITrainModuleService>())
            ).AsSelf();
            builder.Register((c, p) => new LookDetails_VM(
               c.Resolve<LookDetails_Controls>(),
               c.Resolve<ITrainModuleService>(),
               p.Named<ATask>("Task"))
            ).AsSelf();
            builder.Register(c => new CreatTask_VM(
               c.Resolve<CreatTask_Controls>(),
               c.Resolve<ITrainModuleService>(),
               c.Resolve<DialogService>())
            ).AsSelf();
            builder.Register(c => new Seting_VM(
               c.Resolve<Seting_Controls>())
            ).AsSelf();
            builder.Register(c => new Registration_VM(
               c.Resolve<Registration_Controls>(),
               c.Resolve<IUserService>())
            ).AsSelf();
            builder.Register(c => new CreatTopic_VM(
               c.Resolve<CreatTopic_Controls>(),
               c.Resolve<ITrainModuleService>())
            ).AsSelf();
            builder.Register((c, p) => new ChangeScriptName_VM(
               p.Named<CreatScrip_Controls>("Controls"))
            ).AsSelf();
        }
    }
}
