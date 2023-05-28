using Autofac;
using Services.RequestDB.InterfaceDB;
using System;
using System.Linq;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.TrainModule;

namespace ViewModels.Tutor.TrainModule
{
    public class CreatScript_VM : BaseViewModel
    {
        private readonly ITrainModuleService TMS;
        public CreatScrip_Controls Controls { get; set; }
        public CreatScript_VM(
            CreatScrip_Controls controls,
            ITrainModuleService trainModuleService)
        {
            Controls = controls;
            TMS = trainModuleService;
            Controls.GetTasks += () => GetTasks();
            Controls.GetQuestions += () => GetQuestions();
            Clear.Execute(null);
        }
        private RelayCommand? download;
        private RelayCommand? addQueation;
        private RelayCommand? addTask;
        private RelayCommand? deleteQueation;
        private RelayCommand? deleteTask;
        private RelayCommand? getAnswers;
        private RelayCommand? getDetails;
        private RelayCommand? creatQuestion;
        private RelayCommand? creatTask;
        private RelayCommand? save;
        private RelayCommand? clear;
        private RelayCommand? creatTopic;
        public ICommand Download
        {
            get
            {
                return download ??= new RelayCommand(obj =>
                {
                    
                });
            }
        }
        public ICommand AddQueation
        {
            get
            {
                return addQueation ??= new RelayCommand(obj =>
                {
                    try
                    {
                        if (Controls.SelectQuestion != null &&
                        Controls.SelectQuestions.SingleOrDefault(q =>
                        q.Text == Controls.SelectQuestion.Text) == null)
                        {
                            Controls.SelectQuestions.Add(Controls.SelectQuestion);
                            return;
                        }
                        throw new Exception("Подобный вопрос уже добавлен!");
                    }
                    catch(Exception ex)
                    {
                        ErrorMessage(ex.Message, "Ошибка при добавлении вопроса");
                    }
                });
            }
        }
        public ICommand DeleteQueation
        {
            get
            {
                return deleteQueation ??= new RelayCommand(obj =>
                {
                    if (Controls.CurrentSelectQuestion != null &&
                        Controls.SelectQuestions.SingleOrDefault(q =>
                        q.Text == Controls.CurrentSelectQuestion.Text) != null)
                    {
                        Controls.SelectQuestions.Remove(Controls.CurrentSelectQuestion);
                    }
                });
            }
        }
        public ICommand AddTask
        {
            get
            {
                return addTask ??= new RelayCommand(obj =>
                {
                    try
                    {
                        if (Controls.SelectTask != null &&
                        Controls.SelectTasks.SingleOrDefault(t =>
                        t.Name == Controls.SelectTask.Name && 
                        t.Discriminator == Controls.SelectTask.Discriminator) == null)
                        {
                            Controls.SelectTasks.Add(Controls.SelectTask);
                            return;
                        }
                        throw new Exception("Задача с подобным названием уже добавлена!");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex.Message, "Ошибка при добавлении задачи");
                    }
                    
                });
            }
        }
        public ICommand DeleteTask
        {
            get
            {
                return deleteTask ??= new RelayCommand(obj =>
                {
                    if (Controls.CurrentSelectTask != null &&
                        Controls.SelectTasks.SingleOrDefault(t =>
                        t.Name == Controls.CurrentSelectTask.Name) != null)
                    {
                        Controls.SelectTasks.Remove(Controls.CurrentSelectTask);
                    }
                });
            }
        }
        private void GetTasks()
        {
            Controls.Tasks = new(TMS.GetTasks(Controls.SelectDiscriminator));
            Controls.SelectTask = Controls.Tasks.FirstOrDefault();
        }
        private void GetQuestions()
        {
            if(Controls.SelectTopicForQuest != null)
            {
                Controls.Questions = new(TMS.GetQuestions(Controls.SelectTopicForQuest.Id));
                Controls.SelectQuestion = Controls.Questions.LastOrDefault();
            }
        }
        public ICommand GetAnswers
        {
            get
            {
                return getAnswers ??= new RelayCommand(obj =>
                {
                    if(Controls.CurrentSelectQuestion != null)
                    {
                        AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "LookAnswers",
                            Builder.Resolve<LookAnswers_VM>(
                                new NamedParameter("Question",
                                Controls.CurrentSelectQuestion))
                            ));
                    }
                });
            }
        }
        public ICommand GetDetails
        {
            get
            {
                return getDetails ??= new RelayCommand(obj =>
                {
                    if (Controls.CurrentSelectTask != null)
                    {
                        AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "LookDetails",
                            Builder.Resolve<LookDetails_VM>(
                                new NamedParameter("Task",
                                Controls.CurrentSelectTask))
                            ));
                    }
                });
            }
        }
        public ICommand CreatQuestion
        {
            get
            {
                return creatQuestion ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "CreatQuestion",
                            Builder.Resolve<CreatQuestion_VM>()
                            )
                        );
                    GetQuestions();
                });
            }
        }
        public ICommand CreatTask
        {
            get
            {
                return creatTask ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "CreatTask",
                            Builder.Resolve<CreatTask_VM>()
                            )
                        );
                    GetQuestions();
                });
            }
        }
        public ICommand Save
        {
            get
            {
                return save ??= new RelayCommand(obj =>
                {

                });
            }
        }
        public ICommand Clear
        {
            get
            {
                return clear ??= new RelayCommand(obj =>
                {
                    Controls.ScriptName = "Новый скрипт!";
                    Controls.TestName = "Новый тест!";
                    Controls.Topics = new(TMS.GetTopics());
                    Controls.SelectTopicForScript = Controls.Topics.First();
                    Controls.SelectTopicForQuest = Controls.SelectTopicForScript;
                    Controls.Discriminators = new(TMS.GetDiscriminators());
                    Controls.SelectDiscriminator = Controls.Discriminators.First();
                    Controls.SelectQuestions.Clear();
                    Controls.SelectTasks.Clear();
                });
            }
        }
        public ICommand CreatTopic
        {
            get
            {
                return creatTopic ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "CreatTopic",
                            Builder.Resolve<CreatTopic_VM>()));
                    Controls.Topics = new(TMS.GetTopics());
                });
            }
        }
    }
}
