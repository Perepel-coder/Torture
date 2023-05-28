using Autofac;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System.Linq;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.UserDB;

namespace ViewModels.Tutor.UserDB
{
    public class UserDB_VM : BaseViewModel
    {
        private readonly IUserService US;
        public UserDB_Controls Controls { get; set; }

        public UserDB_VM(
            UserDB_Controls controls,
            IUserService userService)
        {
            Controls = controls;
            this.US = userService;
            Controls.Tutor = (Tutor_S)User;
            Controls.Groups = new(Controls.Tutor.Groups);
            Controls.CurrentGroup = Controls.Tutor.Groups.FirstOrDefault();
        }
        private RelayCommand? getUserInfo;
        private RelayCommand? addGroup;
        private RelayCommand? changeGroup;
        private RelayCommand? formUpdate;
        private RelayCommand? groupDelete;
        public ICommand GetUserInfo
        {
            get
            {
                return getUserInfo ??= new RelayCommand(obj =>
                {
                    if(Controls.CurrentResearcher != null)
                    {
                        AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "UserInfo",
                            Builder.Resolve<UserInfo_VM>(
                                new NamedParameter("Researcher", Controls.CurrentResearcher))
                            ));
                    }
                });
            }
        }
        public ICommand AddGroup
        {
            get
            {
                return addGroup ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext(
                        "AddGroup", 
                        Builder.Resolve<AddGroup_VM>()));
                    FormUpdate.Execute(null);
                });
            }
        }
        public ICommand ChangeGroup
        {
            get
            {
                return changeGroup ??= new RelayCommand(obj =>
                {
                if (Controls.CurrentGroup != null)
                    {
                        AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "AddGroup",
                            Builder.Resolve<ChangeGroup_VM>(
                                new NamedParameter("Group", Controls.CurrentGroup))
                            ));
                        FormUpdate.Execute(null);
                    }
                });
            }
        }
        public ICommand FormUpdate
        {
            get
            {
                return formUpdate ??= new RelayCommand(obj =>
                {
                    User = US.GetTutor(User.ID);
                    Controls.Groups = ((Tutor_S)User).Groups;
                    Controls.CurrentGroup = Controls.Groups.FirstOrDefault();
                });
            }
        }
        public ICommand GroupDelete
        {
            get
            {
                return groupDelete ??= new RelayCommand(obj =>
                {
                    if(Controls.CurrentGroup != null)
                    {
                        if (US.DeleteGroup(Controls.CurrentGroup))
                        {
                            FormUpdate.Execute(obj);
                            InfoMessage("Группа удалена!");
                        }
                        else
                        {
                            ErrorMessage("Не удалось удалить группу");
                        }
                    }
                });
            }
        }
    }
}
