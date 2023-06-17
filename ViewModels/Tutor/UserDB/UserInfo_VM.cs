using Autofac;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System.Linq;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.UserDB;

namespace ViewModels.Tutor.UserDB
{
    public class UserInfo_VM : BaseViewModel
    {
        public UserInfo_Controls Controls { get; set; }
        public UserInfo_VM(
            UserInfo_Controls controls,
            IUserService userService,
            Researcher_S researcher)
        {
            Controls = controls;
            if(User.Discriminator == "Researcher")
            {
                Controls.Researcher = userService.GetResearcher(researcher.ID);
            }
            else if(User.Discriminator == "Tutor")
            {
                Controls.Researcher = researcher;
            }
            if(Controls.Researcher?.GroupId != null)
            {
                Controls.Group = userService.GetGroupName((int)Controls.Researcher.GroupId);
            }
        }
        private RelayCommand? getTest;
        public ICommand GetTest
        {
            get
            {
                return getTest ??= new RelayCommand(obj =>
                {
                    if(Controls.CurrentScript != null)
                    {
                        Controls.CurrentVM = Builder.Resolve<UserInfoTest_VM>(
                            new NamedParameter("Script", Controls.CurrentScript));
                    }
                });
            }
        }
    }
}
