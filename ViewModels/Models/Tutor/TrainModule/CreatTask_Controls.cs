using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class CreatTask_Controls : ReactiveObject
    {
        public ObservableCollection<string> ClassTask { get; set; } = new();
        private string name = string.Empty;
        private string textTask = string.Empty;
        private string textDownload = string.Empty;
        private string selectClassTask = string.Empty;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }
        public string TextTask
        {
            get => textTask;
            set => this.RaiseAndSetIfChanged(ref textTask, value);
        }
        public string TextDownload
        {
            get => textDownload;
            set => this.RaiseAndSetIfChanged(ref textDownload, value);
        }
        public string SelectClassTask
        {
            get => selectClassTask;
            set => this.RaiseAndSetIfChanged(ref selectClassTask, value);
        }

        public BaseMath_S BaseMathTask { get; set; } = new();
        private ObservableCollection<string> typeTask = new();
        public ObservableCollection<string> TypeTask
        {
            get => typeTask;
            set => this.RaiseAndSetIfChanged(ref typeTask, value);
        }
        private string selectTypeTask = string.Empty;
        private string className = string.Empty;
        private string methodName = string.Empty;
        private string alfabet = string.Empty;
        private string text = string.Empty;
        private int testCount;
        private Visibility baseMath;
        private Visibility selectKey;
        public Visibility BaseMath
        {
            get => baseMath;
            set => this.RaiseAndSetIfChanged(ref baseMath, value);
        }
        public Visibility SelectKey
        {
            get => selectKey;
            set => this.RaiseAndSetIfChanged(ref selectKey, value);
        }
        public string SelectTypeTask
        {
            get => selectTypeTask;
            set => this.RaiseAndSetIfChanged(ref selectTypeTask, value);
        }
        public string ClassName
        {
            get => className;
            set => this.RaiseAndSetIfChanged(ref className, value);
        }
        public string MethodName
        {
            get => methodName;
            set => this.RaiseAndSetIfChanged(ref methodName, value);
        }
        public string Alfabet
        {
            get => alfabet;
            set => this.RaiseAndSetIfChanged(ref alfabet, value);
        }
        public string Text
        {
            get => text;
            set => this.RaiseAndSetIfChanged(ref text, value);
        }
        public int TestCount
        {
            get => testCount;
            set => this.RaiseAndSetIfChanged(ref testCount, value);
        }
        public void Collapsed()
        {
            BaseMath = Visibility.Collapsed;
            SelectKey = Visibility.Collapsed;
        }
        public CreatTask_Controls()
        {
            BaseMath = Visibility.Collapsed;
            SelectKey = Visibility.Collapsed;
        }
    }
}
