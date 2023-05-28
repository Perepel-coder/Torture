using Autofac;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repo.Autofac
{
    public class RepoAuto : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new Cryptoalgorithm_DBContext("Cryptoalgorithm.db"))
                .As<DbContext>()
                .AsSelf();

            builder.Register(c => new Cryptoalgorithm_Repository<Cryptoalgorithm>(
                c.Resolve<Cryptoalgorithm_DBContext>()))
                .As<IRepository<Cryptoalgorithm>>()
                .AsSelf();
            builder.Register(c => new Cryptoalgorithm_Repository<NumOfKey>(
                c.Resolve<Cryptoalgorithm_DBContext>()))
                .As<IRepository<NumOfKey>>()
                .AsSelf();
            builder.Register(c => new Cryptoalgorithm_Repository<Mode>(
                c.Resolve<Cryptoalgorithm_DBContext>()))
                .As<IRepository<Mode>>()
                .AsSelf();


            builder.Register(c => new User_DBContext("User.db"))
                .As<DbContext>()
                .AsSelf();

            builder.Register(c => new User_Repository<User>(c.Resolve<User_DBContext>()))
               .As<IRepository<User>>()
               .AsSelf();
            builder.Register(c => new User_Repository<Tutor>(c.Resolve<User_DBContext>()))
               .As<IRepository<Tutor>>()
               .AsSelf();
            builder.Register(c => new User_Repository<Group>(c.Resolve<User_DBContext>()))
               .As<IRepository<Group>>()
               .AsSelf();
            builder.Register(c => new User_Repository<Researcher>(c.Resolve<User_DBContext>()))
               .As<IRepository<Researcher>>()
               .AsSelf();
            builder.Register(c => new User_Repository<TestUser>(c.Resolve<User_DBContext>()))
               .As<IRepository<TestUser>>()
               .AsSelf();


            builder.Register(c => new TrainingModule_DBContext("TrainingModule.db"))
                .As<DbContext>()
                .AsSelf();

            builder.Register(c=> new TrainingModuleDB_Repository<BaseMath>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<BaseMath>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Compiler>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Compiler>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Script>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Script>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Topic>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Topic>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Answer>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Answer>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Question>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Question>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Test>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Test>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<Task>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<Task>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<MethodProgramm>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<MethodProgramm>>()
                .AsSelf();
            builder.Register(c => new TrainingModuleDB_Repository<SelectKey>(
                c.Resolve<TrainingModule_DBContext>()))
                .As<IRepository<SelectKey>>()
                .AsSelf();
        }
    }
}
