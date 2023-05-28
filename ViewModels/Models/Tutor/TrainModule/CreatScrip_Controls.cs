using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class CreatScrip_Controls : ReactiveObject
    {
        public event Action GetTasks = null!;
        public event Action GetQuestions = null!;

        public ObservableCollection<Question_S> SelectQuestions { get; set; } = new();
        public ObservableCollection<ATask> SelectTasks { get; set; } = new();
        public ObservableCollection<string> Discriminators { get; set; } = new();
        private ObservableCollection<ATask> tasks = new();
        private ObservableCollection<Question_S> questions = new();
        private ObservableCollection<Topic_S> topics = new();
        public ObservableCollection<Topic_S> Topics
        {
            get => topics;
            set=> this.RaiseAndSetIfChanged(ref topics, value);
        }
        public ObservableCollection<ATask> Tasks
        {
            get => tasks;
            set=> this.RaiseAndSetIfChanged(ref tasks, value);
        }
        public ObservableCollection<Question_S> Questions
        {
            get => questions;
            set=> this.RaiseAndSetIfChanged(ref questions, value);
        }

        private string scriptName = string.Empty;
        private string testName = string.Empty;
        private Topic_S selectTopicForScript = new();
        private Topic_S selectTopicForQuest = new();
        private string selectDiscriminator = string.Empty;
        private ATask? selectTask = new();
        private Question_S? selectQuestion = new();
        public string ScriptName
        {
            get => scriptName;
            set => this.RaiseAndSetIfChanged(ref scriptName, value);
        }
        public string TestName
        {
            get => testName;
            set => this.RaiseAndSetIfChanged(ref testName, value);
        }
        public Topic_S SelectTopicForScript
        {
            get => selectTopicForScript;
            set => this.RaiseAndSetIfChanged(ref selectTopicForScript, value);
        }
        public Topic_S SelectTopicForQuest
        {
            get => selectTopicForQuest;
            set 
            { 
                this.RaiseAndSetIfChanged(ref selectTopicForQuest, value);
                GetQuestions();
            }
        }
        public string SelectDiscriminator
        {
            get => selectDiscriminator;
            set 
            {
                this.RaiseAndSetIfChanged(ref selectDiscriminator, value);
                GetTasks();
            } 
        }
        public ATask? SelectTask
        {
            get => selectTask;
            set => this.RaiseAndSetIfChanged(ref selectTask, value);
        }
        public Question_S? SelectQuestion
        {
            get => selectQuestion;
            set => this.RaiseAndSetIfChanged(ref selectQuestion, value);
        }

        private Question_S? currentSelectQuestion;
        public Question_S? CurrentSelectQuestion
        {
            get => currentSelectQuestion;
            set => this.RaiseAndSetIfChanged(ref currentSelectQuestion, value);
        }
        private ATask? currentSelectTask;
        public ATask? CurrentSelectTask
        {
            get => currentSelectTask;
            set => this.RaiseAndSetIfChanged(ref currentSelectTask, value);
        }
    }
}
