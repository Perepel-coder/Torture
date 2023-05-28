using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Interfaces;

namespace ViewModels.Models.Script
{
    public class TaskScript_Panel_Controls : ReactiveObject
    {
        private ATask? selectTask;
        private Script_S? script;

        public Script_S? Script
        {
            get => script;
            set => this.RaiseAndSetIfChanged(ref script, value);
        }
        public ATask? SelectTask
        {
            get => selectTask;
            set => this.RaiseAndSetIfChanged(ref selectTask, value);
        }
        private BaseViewModel? currentVM_TaskPanel;
        public BaseViewModel? CurrentVM_TaskPanel
        {
            get => CurrentVM_TaskPanel;
            set => this.RaiseAndSetIfChanged(ref currentVM_TaskPanel, value);
        }
    }
}
