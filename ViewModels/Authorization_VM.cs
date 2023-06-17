using Autofac;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using ViewModels.Interfaces;
using ViewModels.Researcher;
using ViewModels.Tutor;

namespace ViewModels
{
    public class Authorization_VM: BaseViewModel, IAuthorization<string>
    {
        private readonly IUserService userService;
        public BaseViewModel? CurrentVM { get; set; }
        public Authorization_VM(
            IUserService userService)
        {
            this.userService = userService;
        }

        public bool Authorization(string login, string password)
        {
            User_S? user = userService.GetUser(login, password);
            if(user == null)
            {
                return false;
            }
            if(user.GetType() == typeof(Tutor_S))
            {
                User = user;
                CurrentVM = Builder.Resolve<TutorMW_VM>();
            }
            if(user.GetType() == typeof(Researcher_S))
            {
                User = user;
                CurrentVM = Builder.Resolve<UserMW_VM>();
            }
            return true;
        }

        public bool Registration(string login, string password, USER_ROLE role)
        {
            return userService.RegUser(login, password, role);
        }
    }
}