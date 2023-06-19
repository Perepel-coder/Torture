using ReactiveUI;
using Services.Models;
using System;
using System.Collections.ObjectModel;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class CreatQuestion_Controls : ReactiveObject
    {
        private ObservableCollection<Answer_S> answers = new();
        private ObservableCollection<Question_S> questions = new();
        private ObservableCollection<Topic_S> topics = new();
        private ObservableCollection<string> topics_quest = new();

        public event Action? GetListQuestions;
        public ObservableCollection<Topic_S> Topics
        {
            get => topics;
            set=>this.RaiseAndSetIfChanged(ref topics, value);
        }
        public ObservableCollection<string> Topics_quest
        {
            get=> topics_quest;
            set => this.RaiseAndSetIfChanged(ref topics_quest, value);
        }

        public ObservableCollection<Answer_S> Answers
        {
            get => answers;
            set => this.RaiseAndSetIfChanged(ref answers, value);
        }

        public ObservableCollection<Question_S> Questions
        {
            get => questions;
            set => this.RaiseAndSetIfChanged(ref questions, value);
        }

        private Topic_S selectTopic = new();
        private string topic = string.Empty;
        private string textQuestion = string.Empty;
        private string textAnswer = string.Empty;
        private string mode = string.Empty;
        private Answer_S? selectAnswer;
        private Question_S? selectQuestion;

        public Topic_S SelectTopic
        {
            get => selectTopic;
            set 
            {
                this.RaiseAndSetIfChanged(ref selectTopic, value);
                if (GetListQuestions != null)
                {
                    GetListQuestions.Invoke();
                }
            }
        }
        public string Topic
        {
            get => topic;
            set => this.RaiseAndSetIfChanged(ref topic, value);
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
