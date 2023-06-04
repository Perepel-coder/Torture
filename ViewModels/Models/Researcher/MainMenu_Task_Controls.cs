using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;
using System.Windows;
using ViewModels.Interfaces;

namespace ViewModels.Models
{
    public class MainMenu_Task_Controls : ReactiveObject
    {
        private BaseViewModel currentVM_MenuTasks = null!;
        public BaseViewModel CurrentVM_MenuTasks
        {
            get => currentVM_MenuTasks;
            set => this.RaiseAndSetIfChanged(ref currentVM_MenuTasks, value);
        }

        public ObservableCollection<ATask> Tasks { get; set; } = new();
        private ATask selectedTask = new();
        public ATask SelectTask
        {
            get => selectedTask;
            set => this.RaiseAndSetIfChanged(ref selectedTask, value);
        }

        private Visibility combLock_V;
        private Visibility rocket_V;
        private Visibility keySelection_V;

        public Visibility CombLock_V
        {
            get => combLock_V;
            set => this.RaiseAndSetIfChanged(ref combLock_V, value);
        }
        public Visibility Rocket_V
        {
            get => rocket_V;
            set => this.RaiseAndSetIfChanged(ref rocket_V, value);
        }
        public Visibility KeySelection_V
        {
            get => keySelection_V;
            set => this.RaiseAndSetIfChanged(ref keySelection_V, value);
        }

        public void Visi_CombLock()
        {
            Collapsed_All();
            CombLock_V = Visibility.Visible;
        }
        public void Visi_Rocket()
        {
            Collapsed_All();
            Rocket_V = Visibility.Visible;
        }
        public void Visi_KeySelection()
        {
            Collapsed_All();
            KeySelection_V = Visibility.Visible;
        }
        public void Collapsed_All()
        {
            CombLock_V = Visibility.Collapsed;
            Rocket_V = Visibility.Collapsed;
            KeySelection_V = Visibility.Collapsed;
        }
    }
}
