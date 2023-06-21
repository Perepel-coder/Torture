using ReactiveUI;
using Services.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ViewModels.Models.Tasks
{
    public class Rocket_Controls : Task_Main_Controls
    {
        public Image? Image { get; set; }
        public string ClassName { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public IEnumerable<object[]>? Answer { get; set; } = null!;
        public IEnumerable<object[]>? TestCasesTask { get; set; } = null!;
        public CompilerOut Compiler{ get; set; } = null!;

        private string textTask;
        private string? htmlCode;
        private string code;
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
        public string Code
        {
            get => code;
            set => this.RaiseAndSetIfChanged(ref code, value);
        }
        public Rocket_Controls()
        {
            code = string.Empty;
            textTask = string.Empty;
            htmlCode = string.Empty;
            Rocket = Visibility.Visible;
            CombLock = Visibility.Collapsed;
            KeySelect = Visibility.Collapsed;
        }

        public IEnumerable<string[]> GetTests(string data)
        {
            return data.Split(new char[] { '\n', '\r' })
                    .Where(s => s != "")
                    .Select(s => s.Split(' '));
        }
        public IEnumerable<string> GetAnswers(string data)
        {
            return data.Split(new char[] { '\n', '\r' }).Where(s => s != "");
        }
    }
}
