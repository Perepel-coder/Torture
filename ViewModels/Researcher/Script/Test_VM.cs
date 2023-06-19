using Autofac;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Script;

namespace ViewModels.Researcher.Script
{
    public class Test_VM : BaseViewModel
    {
        private IUserService US;
        public Test_Controls Controls { get; set; }
        public Test_VM(
            Test_Controls controls,
            IUserService userService,
            Script_S script)
        {
            Name = typeof(Test_VM).Name;
            Controls = controls;
            Controls.Script = script;
            US = userService;
        }
        private RelayCommand? complete;
        public ICommand Complete
        {
            get
            {
                return complete ??= new RelayCommand(obj =>
                {
                    if(Controls.Script != null)
                    {
                        Controls.CurrentVM_Test = Builder.Resolve<TaskScript_Panel_VM>(
                            new NamedParameter("Script", Controls.Script),
                            new NamedParameter("UserScript", US.GetScript(Controls.Script, true, true))
                            );
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
