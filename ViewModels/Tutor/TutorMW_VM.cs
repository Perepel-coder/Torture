using Autofac;
using Services.Models;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models;
using ViewModels.Tutor.TrainModule;
using ViewModels.Tutor.UserDB;

namespace ViewModels.Tutor
{
    public class TutorMW_VM : BaseViewModel
    {
        public TutorMW_Controls Controls { get; set; }
        public TutorMW_VM(
            TutorMW_Controls controls)
        {
            Controls = controls;
            Name = typeof(TutorMW_VM).Name;
            Controls.Tutor = (Tutor_S)User;
            GetUserDB.Execute(null);
        }

        private RelayCommand? getUserDB;
        private RelayCommand? getTrainModuleDB;
        private RelayCommand? getSettingsDB;

        public ICommand GetUserDB
        {
            get
            {
                return getUserDB ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_TutorVM = Builder.Resolve<UserDB_VM>();
                });
            }
        }
        public ICommand GetTrainModuleDB
        {
            get
            {
                return getTrainModuleDB ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_TutorVM = Builder.Resolve<CreatScript_VM>();
                });
            }
        }
        public ICommand GetSettingsDB
        {
            get
            {
                return getSettingsDB ??= new RelayCommand(obj =>
                {
                    Controls.CurrentVM_TutorVM = Builder.Resolve<Seting_VM>();
                });
            }
        }
    }
}
