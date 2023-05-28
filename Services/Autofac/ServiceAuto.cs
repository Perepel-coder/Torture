using Autofac;
using Models;
using Repo;
using Repo.Autofac;
using Services.Crypto;
using Services.Models;
using Services.RequestDB;
using Services.RequestDB.InterfaceDB;

namespace Services.Autofac
{
    public class ServiceAuto : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepoAuto());
            builder.RegisterType<DialogService>().AsSelf();
            builder.RegisterType<BlockEncryption>().AsSelf();
            builder.RegisterType<CryptoStopwatch>().AsSelf();
            builder.RegisterType<CypherTools>().AsSelf();
            builder.Register(
                c => new CryptoStopwatchService(c.Resolve<CryptoStopwatch>())
            ).AsSelf();
            builder.Register(c => new CryptoAlgService(
                c.Resolve<IRepository<Cryptoalgorithm>>(),
                c.Resolve<IRepository<NumOfKey>>(),
                c.Resolve<IRepository<Mode>>()))
                .As<ICryptoAlgService>()
                .AsSelf();
            builder.Register(c => new TrainModuleService(
                c.Resolve<IRepository<BaseMath>>(),
                c.Resolve<IRepository<Compiler>>(),
                c.Resolve<IRepository<Script>>(),
                c.Resolve<IRepository<Topic>>(),
                c.Resolve<IRepository<Answer>>(),
                c.Resolve<IRepository<Question>>(),
                c.Resolve<IRepository<Test>>(),
                c.Resolve<IRepository<Task>>(),
                c.Resolve<IRepository<MethodProgramm>>(),
                c.Resolve<IRepository<SelectKey>>()))
                .As<ITrainModuleService>()
                .AsSelf();
            builder.Register(c => new UserService(
                c.Resolve<IRepository<User>>(),
                c.Resolve<IRepository<Tutor>>(),
                c.Resolve<IRepository<Group>>(),
                c.Resolve<IRepository<Researcher>>(),
                c.Resolve<IRepository<TestUser>>(),
                c.Resolve<AssessmentSystem>()))
                .As<IUserService>()
                .AsSelf();

            builder.Register(c => new AssessmentSystem(
                c.Resolve<ITrainModuleService>()))
                .AsSelf();
        }
    }
}