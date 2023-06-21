using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModels.Models.Tasks
{
    public class Task_Main_Controls: ReactiveObject
    {
        private Visibility combLock;
        private Visibility rocket;
        private Visibility keySelect;

        public Visibility CombLock
        {
            get => combLock;
            set => this.RaiseAndSetIfChanged(ref combLock, value);
        }
        public Visibility Rocket
        {
            get => rocket;
            set => this.RaiseAndSetIfChanged(ref rocket, value);
        }
        public Visibility KeySelect
        {
            get => keySelect;
            set => this.RaiseAndSetIfChanged(ref keySelect, value);
        }
    }
}
