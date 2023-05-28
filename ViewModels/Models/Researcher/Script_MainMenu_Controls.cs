using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;
using ViewModels.Interfaces;

namespace ViewModels.Models
{
    public class Script_MainMenu_Controls : ReactiveObject
    {
        public ObservableCollection<AScript> Scripts { get; set; }
        private Script_S? selectedScript;
        public Script_S? SelectedScript
        {
            get => selectedScript;
            set => this.RaiseAndSetIfChanged(ref selectedScript, value);
        }
        private BaseViewModel? currentVM_ScriptMenu;
        public BaseViewModel? CurrentVM_ScriptMenu
        {
            get => currentVM_ScriptMenu;
            set => this.RaiseAndSetIfChanged(ref currentVM_ScriptMenu, value);
        }
    }
}
