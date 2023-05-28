using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor;
using ViewModels.Tutor.TrainModule;
using Autofac;

namespace ViewModels.Tutor
{
    public class Seting_VM : BaseViewModel
    {
        public Seting_Controls Controls { get; set; }
        public Seting_VM(Seting_Controls controls)
        {
            Controls = controls;
        }
        private RelayCommand? creatQuestions;
        private RelayCommand? curatorReg;
        public ICommand CreatQuestions
        {
            get
            {
                return creatQuestions ??= new RelayCommand(obj =>
                {
                    Controls.SettingViewModel = Builder.Resolve<CreatQuestion_VM>();
                });
            }
        }
        public ICommand CuratorReg
        {
            get
            {
                return curatorReg ??= new RelayCommand(obj =>
                {
                    Controls.SettingViewModel = Builder.Resolve<Registration_VM>();
                });
            }
        }
    }
}
