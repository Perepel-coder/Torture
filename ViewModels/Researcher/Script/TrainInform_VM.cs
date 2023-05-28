using Autofac;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Script;

namespace ViewModels.Researcher.Script
{
    public class TrainInform_VM : BaseViewModel
    {
        public TrainInform_Controls Controls { get; set; }
        public TrainInform_VM(
            TrainInform_Controls controls,
            AScript script)
        {
            Controls = controls;
            Controls.Script = script as Script_S;
            if (Controls.Script != null)
            {
                Controls.Info =
                    Environment.CurrentDirectory +
                    @"\Researcher\Scripts\Resourses\" +
                    script.Information;
            }
            else
            {
                ErrorMessage("Ошибка при выборе сценария!", "Программная ошибка");
            }
        }

        private RelayCommand? forth;
        public ICommand Forth
        {
            get
            {
                return forth ??= new RelayCommand(obj =>
                {
                    if(Controls.Script != null)
                    {
                        Controls.CurrentVM_TrainInform = Builder.Resolve<Test_VM>(
                            new NamedParameter("Script", Controls.Script));
                    }
                    else
                    {
                        ErrorMessage("Ошибка при выборе сценария!", "Программная ошибка");
                    }
                });
            }
        }
    }
}
