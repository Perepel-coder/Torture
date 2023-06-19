using Autofac;
using DynamicData;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System.Linq;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.TrainModule;

namespace ViewModels.Tutor.TrainModule
{
    public class CreatQuestion_VM : BaseViewModel
    {
        public CreatQuestion_Controls Controls { get; set; }
        private readonly ITrainModuleService TMS;
        public CreatQuestion_VM(
            CreatQuestion_Controls controls,
            ITrainModuleService trainModuleService)
        { 
            Controls = controls;
            Controls.GetListQuestions += GetListQuestions;
            TMS = trainModuleService;
            Controls.Topics = new(TMS.GetTopics());
            Controls.Topics_quest = new(Controls.Topics.Select(t=>t.Name));
            Controls.SelectTopic = Controls.Topics.First();
            Controls.Topic = Controls.Topics_quest.First();
            Controls.Mode = "Неправильный";
        }

        private RelayCommand? creatTopic;
        private RelayCommand? addAnswer;
        private RelayCommand? deleteAnswer;
        private RelayCommand? creatQuestion;
        private RelayCommand? updateQuestion;
        private RelayCommand? detailsQuestion;
        private RelayCommand? deleteQuestion;

        public ICommand CreatTopic
        {
            get
            {
                return creatTopic ??= new RelayCommand(obj =>
                {
                    AppWindows.ShowDialog(AppWindows.SetDataContext("AddTopic", 
                        Builder.Resolve<CreatTopic_VM>()));
                    Controls.Topics = new(TMS.GetTopics());
                    Controls.Topics_quest = new(Controls.Topics.Select(t => t.Name));
                    Controls.SelectTopic = Controls.Topics.First();
                    Controls.Topic = Controls.Topics_quest.First();
                });
            }
        }
        public ICommand AddAnswer
        {
            get
            {
                return addAnswer ??= new RelayCommand(obj =>
                {
                    if (Controls.TextAnswer.Replace(" ", "") != "" && Controls.Answers
                    .SingleOrDefault(a => a.Answer == Controls.TextAnswer) == null)
                    {
                        Controls.Answers.Add(new Answer_S
                        {
                            Answer = Controls.TextAnswer,
                            Flag = Controls.Mode == "Неправильный" ? false : true
                        });
                        return;
                    }
                    ErrorMessage("Ответ с подобной формулировкой уже добавлен!",
                        "Ошибка при редактировании списка ответов");
                });
            }
        }
        public ICommand DeleteAnswer
        {
            get
            {
                return deleteAnswer ??= new RelayCommand(obj =>
                {
                    if (Controls.SelectAnswer != null)
                    {
                        Controls.Answers.Remove(Controls.SelectAnswer);
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
                    var question = new Question_S
                    {
                        Text = Controls.TextQuestion,
                        Topic = Controls.Topic,
                        Answers = new(Controls.Answers)
                    };

                    question = TMS.SaveQuestion(question);
                    if (question == null)
                    {
                        ErrorMessage(
                            "Добавление вопроса с текущими параметрами не возможно!\n" +
                            "Попробуйте изменить формулировку вопроса или ответов",
                            "Ошибка при добавлении вопроса");
                    }
                    else
                    {
                        InfoMessage("Вопрос добавлен", "Удачное добавление вопроса");
                        Controls.Questions.Add(question);
                        Controls.Topics = new(TMS.GetTopics());
                        Controls.SelectTopic = Controls.Topics.Single(q => q.Name == question.Topic);
                        GetListQuestions();
                        Controls.Topics_quest = new(Controls.Topics.Select(t => t.Name));
                    }
                });
            }
        }
        public ICommand UpdateQuestion
        {
            get
            {
                return updateQuestion ??= new RelayCommand(obj =>
                {
                    if(Controls.SelectQuestion == null) 
                    { 
                        return; 
                    }
                    var question = new Question_S
                    {
                        Id = Controls.SelectQuestion.Id,
                        Text = Controls.TextQuestion,
                        Topic = Controls.Topic,
                        Answers = new(Controls.Answers)
                    };

                    question = TMS.UpdateQuestion(question);
                    if (question == null)
                    {
                        ErrorMessage(
                            "Обновление вопроса с текущими параметрами не возможно!\n" +
                            "Попробуйте изменить формулировку вопроса или ответов",
                            "Ошибка при обновлении вопроса");
                    }
                    else
                    {
                        InfoMessage("Вопрос обновлен", "Удачное обновление вопроса");
                        Controls.Questions.Replace(Controls.SelectQuestion, question);
                        Controls.SelectQuestion = question;
                    }
                    Controls.Topics = new(TMS.GetTopics());
                    GetListQuestions();
                    Controls.Topics_quest = new(Controls.Topics.Select(t => t.Name));
                });
            }
        }
        public ICommand DetailsQuestion
        {
            get
            {
                return detailsQuestion ??= new RelayCommand(obj =>
                {
                    if (Controls.SelectQuestion != null)
                    {
                        Controls.Topic = Controls.SelectQuestion.Topic;
                        Controls.TextQuestion = Controls.SelectQuestion.Text;
                        Controls.Answers = new(Controls.SelectQuestion.Answers);
                    }
                });
            }
        }
        public ICommand DeleteQuestion
        {
            get
            {
                return deleteQuestion ??= new RelayCommand(obj =>
                {
                    if (Controls.SelectQuestion != null)
                    {
                        TMS.DeleteQuestion(Controls.SelectQuestion);
                        Controls.Questions.Remove(Controls.SelectQuestion);
                    }
                });
            }
        }

        private void GetListQuestions()
        {
            if (Controls.SelectTopic != null)
            {
                Controls.Questions = new(TMS.GetQuestions()
                    .Where(q => q.Topic == Controls.SelectTopic.Name));
            }
        }
    }
}
