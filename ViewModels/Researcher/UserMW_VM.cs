using Autofac;
using Services.Models;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models;
using ViewModels.Tutor.UserDB;

namespace ViewModels.Researcher
{
    public class UserMW_VM:BaseViewModel
    {
        public override string Name { get; set; }
        public UserMW_Controls Controls { get; set; }

        public UserMW_VM(UserMW_Controls controls)
        {
            Name = typeof(UserMW_VM).Name;
            Controls = controls;
            GetScripts.Execute(null);
        }

        private RelayCommand? userProfile;
        private RelayCommand? getScripts;
        private RelayCommand? getComparison;
        private RelayCommand? getCrypto;
        private RelayCommand? getTasks;

        public ICommand UserProfile
        {
            get
            {
                return userProfile ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "UserInfo",
                            Builder.Resolve<UserInfo_VM>(
                                new NamedParameter("Researcher", (Researcher_S)User))
                            ));
                });
            }
        }
        public ICommand GetScripts
        {
            get
            {
                return getScripts ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_UserVM = Builder.Resolve<Script_MainMenu_VM>();
                });
            }
        }
        public ICommand GetComparison
        {
            get
            {
                return getComparison ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_UserVM = Builder.Resolve<CryptoComparison_VM>();
                });
            }
        }
        public ICommand GetCrypto
        {
            get
            {
                return getCrypto ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_UserVM = Builder.Resolve<CypherData_VM>();
                });
            }
        }
        public ICommand GetTasks
        {
            get
            {
                return getTasks ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_UserVM = Builder.Resolve<MainMenu_Task_VM >();
                });
            }
        }
    }
}