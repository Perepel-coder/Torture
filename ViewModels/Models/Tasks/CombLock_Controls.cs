using ReactiveUI;
using System;
using System.Windows;

namespace ViewModels.Models.Tasks
{
    public class Lock<T> : ReactiveObject
    {
        private T? field_1;
        private T? field_2;
        private T? field_3;
        private T? field_4;

        public T? Field_1
        {
            get => field_1;
            set => this.RaiseAndSetIfChanged(ref field_1, value);
        }
        public T? Field_2
        {
            get => field_2;
            set => this.RaiseAndSetIfChanged(ref field_2, value);
        }
        public T? Field_3
        {
            get => field_3;
            set => this.RaiseAndSetIfChanged(ref field_3, value);
        }
        public T? Field_4
        {
            get => field_4;
            set => this.RaiseAndSetIfChanged(ref field_4, value);
        }
        public void Reset()
        {
            Field_1 = default(T);
            Field_2 = default(T);
            Field_3 = default(T);
            Field_4 = default(T);
        }
        public override string ToString()
        {
            return 
                Convert.ToString(field_1) + 
                Convert.ToString(field_2) + 
                Convert.ToString(field_3) + 
                Convert.ToString(field_4);
        }
    }

    public class CombLock_Controls : Task_Main_Controls 
    {
        public string Answer { get; set; } = null!;
        public Func<int?, int?, int?, int> FuncTask { get; set; } = null!;

        private string textTask;
        private string? htmlCode;
        private string startData;
        private int key1;
        private int key2;
        private int mod;
        private int myAnswer;

        public string TextTask
        {
            get => textTask;
            set => this.RaiseAndSetIfChanged(ref textTask, value);
        }
        public string? HtmlCode
        {
            get => htmlCode;
            set => this.RaiseAndSetIfChanged(ref htmlCode, value);
        }
        public string StartData
        {
            get => startData;
            set => this.RaiseAndSetIfChanged(ref startData, value);
        }
        
        public int Key1
        {
            get => key1;
            set => this.RaiseAndSetIfChanged(ref key1, value);
        }
        public int Key2
        {
            get => key2;
            set => this.RaiseAndSetIfChanged(ref key2, value);
        }
        public int Mod
        {
            get => mod;
            set => this.RaiseAndSetIfChanged(ref mod, value);
        }
        public int MyAnswer
        {
            get => myAnswer;
            set => this.RaiseAndSetIfChanged(ref myAnswer, value);
        }

        private int decimalForm;
        private string? binaryForm;
        public int DecimalForm
        {
            get => decimalForm;
            set => this.RaiseAndSetIfChanged(ref decimalForm, value);
        }
        public string? BinaryForm
        {
            get => binaryForm;
            set => this.RaiseAndSetIfChanged(ref binaryForm, value);
        }

        public Lock<char?> GetLock { get; set; }

        private bool isEnabledTool;
        public bool IsEnabledTool
        {
            get => isEnabledTool;
            set => this.RaiseAndSetIfChanged(ref isEnabledTool, value);
        }

        public CombLock_Controls()
        {
            GetLock = new();
            textTask = string.Empty;
            startData = string.Empty;
            htmlCode = string.Empty;
            IsEnabledTool = false;
            Rocket = Visibility.Collapsed;
            KeySelect = Visibility.Collapsed;
            CombLock = Visibility.Visible;
        }
    }
}
