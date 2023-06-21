using ReactiveUI;
using Services.Models;
using Services.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using trans = Services.Crypto.TransformData;

namespace ViewModels.Models.Tasks
{
    public class KeySelect_Controls : Task_Main_Controls
    {
        public ObservableCollection<Mode_S> ListOfAlgMode { get; set; } = new();
        public ObservableCollection<CryptoAlg_S> ListOfAlg { get; set; } = new();
        public IEnumerable<int> DataCode = null!;
        public CypherTools Tools { get; } = new();
        public string TrueAnswer { get; set; } = string.Empty;
        public object[] Alphabet { get; set; } = new object[1];
        public CompilerOut? Compiler { get; set; }
        public string ClassName { get; set; } = null!;
        public string MethodName { get; set; } = null!;

        private string textTask = string.Empty;
        private string htmlCode = string.Empty;
        private string code = string.Empty;
        private string encryptMSG = string.Empty;
        private string answer = string.Empty;
        public string TextTask
        {
            get => textTask;
            set => this.RaiseAndSetIfChanged(ref textTask, value);
        }
        public string HtmlCode
        {
            get => htmlCode;
            set => this.RaiseAndSetIfChanged(ref htmlCode, value);
        }
        public string Code
        {
            get => code;
            set => this.RaiseAndSetIfChanged(ref code, value);
        }
        public string EncryptMSG
        {
            get => encryptMSG;
            set
            {
                this.RaiseAndSetIfChanged(ref encryptMSG, value);
                DataCode = trans.ToInt(value);
            }
        }
        public string Answer
        {
            get => answer;
            set => this.RaiseAndSetIfChanged(ref answer, value);
        }

        public KeySelect_Controls()
        {
            Rocket = Visibility.Collapsed;
            CombLock = Visibility.Collapsed;
            KeySelect = Visibility.Visible;
        }
    }
}
