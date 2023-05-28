using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Interfaces;

namespace ViewModels.Models.Script
{
    public class TrainInform_Controls : ReactiveObject
    {
        private BaseViewModel? currentVM_TrainInform;

        public Script_S? Script { get; set; }

        public Test_S Test { get; set; } = null!;

        private string? info;
        public string? Info
        {
            get => info;
            set => this.RaiseAndSetIfChanged(ref info, value);
        }

        public BaseViewModel? CurrentVM_TrainInform
        {
            get => currentVM_TrainInform;
            set => this.RaiseAndSetIfChanged(ref currentVM_TrainInform, value);
        }
    }
}
