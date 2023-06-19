using Autofac;
using View.Researcher;
using View.Researcher.Scripts;
using View.Tutor;
using View.Tutor.Resourses;
using ViewModels.Autofac;

namespace View.Autofac
{
    public class ViewAuto : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ViewModelAuto());
            builder.Register(c => new UserMainWindow()).AsSelf();
            builder.Register(c=> new ScriptTask_Window()).AsSelf();
            builder.Register(c=> new Authorization()).AsSelf();
            builder.Register(c=> new TutorMainWindow()).AsSelf();
            builder.Register(c=> new UserInfoWindow()).AsSelf();
            builder.Register(c=> new AddGroupWindow()).AsSelf();
            builder.Register(c=> new LookAnswersWindow()).AsSelf();
            builder.Register(c=> new LookDetailsWindow()).AsSelf();
            builder.Register(c=> new CreatTopicWindow()).AsSelf();
            builder.Register(c=> new ChangeNameWindow()).AsSelf();
            builder.Register(c=> new AddTopicWindow()).AsSelf();
        }
    }
}
