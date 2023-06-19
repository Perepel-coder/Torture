using Autofac;
using Services;
using Services.Crypto;
using Services.Models;
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
        private readonly DialogService DS;
        public CreatScrip_Controls Controls { get; set; }
        public CreatScript_VM(
            CreatScrip_Controls controls,
            DialogService dialogService,
            ITrainModuleService trainModuleService)
        {
            Controls = controls;
            TMS = trainModuleService;
            DS = dialogService;

            Controls.GetTasks += GetTasks;
            Controls.GetQuestions += GetQuestions;

            TransitionUpdate.Execute(null);
        }
        private RelayCommand? download;
        private RelayCommand? changeName;
        private RelayCommand? addQueation;
        private RelayCommand? addTask;
        private RelayCommand? deleteQueation;
        private RelayCommand? deleteTask;
        private RelayCommand? getAnswers;
        private RelayCommand? getDetails;
        private RelayCommand? transitionUpdate;
        private RelayCommand? transitionSave;
        private RelayCommand? deleteScript;
        private RelayCommand? update;
        private RelayCommand? save;
        private RelayCommand? delete;


        public ICommand Download
        {
            get
            {
                return download ??= new RelayCommand(obj =>
                {
                    if(DS.OpenFileDialog(DS.pdfFilter) == true)
                    {
                        try
                        {
                            StreamProcess.CopyFile(DS.FilePath,
                                Environment.CurrentDirectory +
                                @"\Researcher\Scripts\Resourses\" + DS.FileName);
                            Controls.InfoFile = DS.FileName;
                            InfoMessage($"Файл {DS.FilePath} успешно загружен");
                        }
                        catch
                        {
                            ErrorFileMessage($"Не удалось загрузить файл {DS.FilePath}");
                        }
                    }
                });
            }
        }
        public ICommand ChangeName
        {
            get
            {
                return changeName ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext(
                            "ChangeName",
                            Builder.Resolve<ChangeScriptName_VM>(
                                new NamedParameter("Controls", Controls))
                            ));
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
                    Controls.SelectTasks.Remove(Controls.CurrentSelectTask);
                });
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

        public ICommand TransitionUpdate
        {
            get
            {
                return transitionUpdate ??= new RelayCommand(obj =>
                {
                    Controls.SetScriptTest += SetScriptTest;

                    Controls.Scripts = new(TMS.GetScripts());
                    Controls.SelectScript = Controls.Scripts.First();

                    Controls.InfoFile = Controls.SelectScript.Information;
                    Controls.Topics = new(TMS.GetTopics().Select(t => t.Name));

                    Controls.SelectTopicForQuest = Controls.Topics.First();
                    Controls.Discriminators = new(TMS.GetDiscriminators());
                    Controls.SelectDiscriminator = Controls.Discriminators.First();
                });
            }
        }
        public ICommand TransitionSave
        {
            get
            {
                return transitionSave ??= new RelayCommand(obj =>
                {
                    Controls.SetScriptTest -= SetScriptTest;

                    Controls.Scripts = new(TMS.GetScripts());
                    Controls.Topics = new(TMS.GetTopics().Select(t => t.Name));

                    Controls.ScriptName = "Новый сценарий";
                    Controls.InfoFile = "Загрузите файл";
                    Controls.SelectTopic = Controls.Topics.First();
                    Controls.TestName = "Новый тест";

                    Controls.SelectQuestions.Clear();
                    Controls.SelectTasks.Clear();

                    Controls.SelectTopicForQuest = Controls.Topics.First();
                    Controls.Discriminators = new(TMS.GetDiscriminators());
                    Controls.SelectDiscriminator = Controls.Discriminators.First();
                });
            }
        }

        public ICommand DeleteScript
        {
            get
            {
                return deleteScript ??= new RelayCommand(obj =>
                {
                    if(Controls.SelectScript != null)
                    {
                        Controls.DeleteScripts.Add(Controls.SelectScript);
                        Controls.Scripts.Remove(Controls.SelectScript);
                    }
                });
            }
        }

        public ICommand Update
        {
            get
            {
                return update ??= new RelayCommand(obj =>
                {
                    Controls.SelectScript.Topic = Controls.SelectTopic;
                    Controls.SelectScript.Test.Name = Controls.TestName;
                    Controls.SelectScript.Test.Questions = Controls.SelectQuestions.ToList();
                    Controls.SelectScript.Test.Tasks = Controls.SelectTasks.ToList();

                    if(Controls.SelectScript.Information != null)
                    {
                        StreamProcess.DeleteFile(Controls.SelectScript.Information);
                    }
                    Controls.SelectScript.Information = Controls.InfoFile;
                    if (TMS.UpdateScript(Controls.SelectScript))
                    {
                        InfoMessage("Сценарий успешно изменен");
                    }
                    else
                    {
                        ErrorMessage("Не удалось изменить сценарий");
                    }
                });
            }
        }
        public ICommand Save
        {
            get
            {
                return save ??= new RelayCommand(obj =>
                {
                    if (Controls.InfoFile.Replace(" ", "").ToLower() == string.Empty)
                    {
                        ErrorMessage("Не возможно добавить сценарий " +
                            "без файла тестовой информации");
                    }
                    if(Controls.ScriptName.Replace(" ", "").ToLower() == string.Empty)
                    {
                        ErrorMessage("Не возможно добавить сценарий " +
                            "без названия");
                    }
                    Script_S script = new();
                    script.Test = new();
                    script.Name = Controls.ScriptName;
                    script.Topic = Controls.SelectTopic;
                    script.Information = Controls.InfoFile;
                    script.Test.Name = Controls.TestName ??= "New test";
                    script.Test.Questions = Controls.SelectQuestions.ToList();
                    script.Test.Tasks = Controls.SelectTasks.ToList();
                    if (TMS.SaveScript(script))
                    {
                        InfoMessage("Сценарий успешно добавлен");
                    }
                    else
                    {
                        ErrorMessage("Не удалось добавить сценарий");
                    }
                });
            }
        }

        public ICommand Delete
        {
            get
            {
                return delete ??= new RelayCommand(obj =>
                {
                    if (TMS.DeleteScripts(Controls.DeleteScripts))
                    {
                        InfoMessage("Сценарии успешно удалены");
                    }
                    else
                    {
                        ErrorMessage("Не удалось удалить сценарии");
                    }
                    Controls.DeleteScripts.Clear();
                });
            }
        }

        private void SetScriptTest()
        {
            if(Controls.SelectScript != null)
            {
                Controls.InfoFile = Controls.SelectScript.Information;
                Controls.SelectTopic = Controls.SelectScript?.Topic;
                Controls.TestName = Controls.SelectScript?.Test?.Name;

                var questions = Controls.SelectScript?.Test?.Questions;
                var tasks = Controls.SelectScript?.Test?.Tasks;
                if (questions != null)
                {
                    Controls.SelectQuestions = new(questions);
                }
                if (tasks != null)
                {
                    Controls.SelectTasks = new(tasks);
                }
            }
        }
        private void GetTasks()
        {
            Controls.Tasks = new(TMS.GetTasks(Controls.SelectDiscriminator));
            Controls.SelectTask = Controls.Tasks.FirstOrDefault();
        }
        private void GetQuestions()
        {
            if (Controls.SelectTopicForQuest != null)
            {
                Controls.Questions = new(TMS.GetQuestions(TMS.GetTopic(Controls.SelectTopicForQuest).Id));
                Controls.SelectQuestion = Controls.Questions.LastOrDefault();
            }
        }
    }
}
