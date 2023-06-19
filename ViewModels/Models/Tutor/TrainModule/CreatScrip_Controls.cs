using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class CreatScrip_Controls : ReactiveObject
    {
        public event Action? SetScriptTest;
        public event Action? GetTasks;
        public event Action? GetQuestions;

        public List<Script_S> DeleteScripts = new();
        public ObservableCollection<string> Discriminators { get; set; } = new();
        public ObservableCollection<string> Topics { get; set; } = new();


        private ObservableCollection<Script_S> scripts = new();
        private ObservableCollection<ATask> selectTasks = new();
        private ObservableCollection<ATask> tasks = new();
        private ObservableCollection<Question_S> selectQuestions = new();
        private ObservableCollection<Question_S> questions = new();

        public ObservableCollection<Script_S> Scripts
        {
            get => scripts;
            set => this.RaiseAndSetIfChanged(ref scripts, value);
        }
        public ObservableCollection<ATask> SelectTasks
        {
            get => selectTasks;
            set => this.RaiseAndSetIfChanged(ref selectTasks, value);
        }
        public ObservableCollection<Question_S> SelectQuestions
        {
            get => selectQuestions;
            set => this.RaiseAndSetIfChanged(ref selectQuestions, value);
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



        private Script_S? selectScript;
        private string scriptName = string.Empty;
        private string? selectTopic = string.Empty;
        private string infoFile = string.Empty;
        private string? testName = string.Empty;
        private Topic_S selectTopicForScript = new();
        private string selectTopicForQuest = string.Empty;
        private string selectDiscriminator = string.Empty;
        private ATask? selectTask = new();
        private Question_S? selectQuestion = new();

        public string ScriptName
        {
            get => scriptName;
            set => this.RaiseAndSetIfChanged(ref scriptName, value);
        }
        public string InfoFile
        {
            get => infoFile;
            set => this.RaiseAndSetIfChanged(ref infoFile, value);
        }
        public Script_S? SelectScript
        {
            get=>selectScript;
            set 
            {
                this.RaiseAndSetIfChanged(ref selectScript, value);
                if(SetScriptTest != null)
                {
                    SetScriptTest.Invoke();
                }
            }
        }
        public string? SelectTopic
        {
            get => selectTopic;
            set => this.RaiseAndSetIfChanged(ref selectTopic, value);
        }


        public string? TestName
        {
            get => testName;
            set => this.RaiseAndSetIfChanged(ref testName, value);
        }
        public Topic_S SelectTopicForScript
        {
            get => selectTopicForScript;
            set => this.RaiseAndSetIfChanged(ref selectTopicForScript, value);
        }
        public string SelectTopicForQuest
        {
            get => selectTopicForQuest;
            set 
            { 
                this.RaiseAndSetIfChanged(ref selectTopicForQuest, value);
                if (GetQuestions != null)
                {
                    GetQuestions.Invoke();
                }
            }
        }
        public string SelectDiscriminator
        {
            get => selectDiscriminator;
            set 
            {
                this.RaiseAndSetIfChanged(ref selectDiscriminator, value);
                if (GetTasks != null)
                {
                    GetTasks.Invoke();
                }
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
