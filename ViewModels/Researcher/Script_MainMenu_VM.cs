using Autofac;
using Services.RequestDB.InterfaceDB;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models;
using ViewModels.Researcher.Script;

namespace ViewModels.Researcher
{
    public class Script_MainMenu_VM : BaseViewModel
    {
        public Script_MainMenu_Controls Controls { get; set; }
        private readonly ITrainModuleService trainModuleService;
        public override string Name { get; set; }

        public Script_MainMenu_VM(
            Script_MainMenu_Controls controls,
            ITrainModuleService trainModuleService)
        {
            Name = typeof(Script_MainMenu_VM).Name;
            Controls = controls;
            this.trainModuleService = trainModuleService;
            Controls.Scripts = new(trainModuleService.GetScripts());
        }
        private RelayCommand? getScript;

        public ICommand GetScript
        {
            get
            {
                return getScript ??= new RelayCommand(obj =>
                {
                    if(Controls.SelectedScript != null)
                    {
                        var script = trainModuleService.GetScript(Controls.SelectedScript.Id, true);
                        Controls.CurrentVM_ScriptMenu = Builder.Resolve<TrainInform_VM>(
                            new NamedParameter("Script", script));
                        Controls.SelectedScript = null;
                    }
                });
            }
        }
        
    }
}
