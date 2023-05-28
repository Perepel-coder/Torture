using ReactiveUI;
using Services.Models;
using ViewModels.Interfaces;

namespace ViewModels.Models.Script
{
    public class Test_Controls : ReactiveObject
    {
        private Script_S? script;
        private BaseViewModel? currentVM_Test;

        public BaseViewModel? CurrentVM_Test
        {
            get => currentVM_Test;
            set => this.RaiseAndSetIfChanged(ref currentVM_Test, value);
        }

        public Script_S? Script
        {
            get => script;
            set => this.RaiseAndSetIfChanged(ref script, value);
        }
        public void Clear()
        {
            Script = null;
        }
    }
}