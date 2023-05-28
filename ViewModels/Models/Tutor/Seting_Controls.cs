using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;
using ViewModels.Interfaces;

namespace ViewModels.Models.Tutor
{
    public class Seting_Controls : ReactiveObject
    {
        private BaseViewModel? setingViewModel;
        public BaseViewModel? SettingViewModel
        {
            get => setingViewModel;
            set => this.RaiseAndSetIfChanged(ref setingViewModel, value);
        }
    }
}
