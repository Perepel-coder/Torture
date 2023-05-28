using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class CreatQuestion_Controls : ReactiveObject
    {
        private ObservableCollection<Answer_S> answers = new();
        public ObservableCollection<Topic_S> Topics { get; set; } = new();
        public ObservableCollection<Question_S> Questions { get; set; } = new();

        public ObservableCollection<Answer_S> Answers
        {
            get => answers;
            set => this.RaiseAndSetIfChanged(ref answers, value);
        }

        private Topic_S selectTopic = new();
        private string textQuestion = string.Empty;
        private string textAnswer = string.Empty;
        private string mode = string.Empty;
        private Answer_S? selectAnswer;
        private Question_S? selectQuestion;

        public Topic_S SelectTopic
        {
            get => selectTopic;
            set => this.RaiseAndSetIfChanged(ref selectTopic, value);
        }
        public string TextQuestion
        {
            get => textQuestion;
            set => this.RaiseAndSetIfChanged(ref textQuestion, value);
        }
        public string TextAnswer
        {
            get => textAnswer;
            set => this.RaiseAndSetIfChanged(ref textAnswer, value);
        }
        public string Mode
        {
            get => mode;
            set => this.RaiseAndSetIfChanged(ref mode, value);
        }
        public Answer_S? SelectAnswer
        {
            get => selectAnswer;
            set => this.RaiseAndSetIfChanged(ref selectAnswer, value);
        }
        public Question_S? SelectQuestion
        {
            get => selectQuestion;
            set => this.RaiseAndSetIfChanged(ref selectQuestion, value);
        }
    }
}
