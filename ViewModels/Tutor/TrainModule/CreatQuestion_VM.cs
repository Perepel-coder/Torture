using Autofac;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TMS = trainModuleService;
            Controls.Topics = new(TMS.GetTopics());
            Controls.SelectTopic = Controls.Topics.First();
            Controls.Mode = "Неправильный";
            Controls.Questions = new(TMS.GetQuestions());
        }
        private RelayCommand? addAnswer;
        private RelayCommand? deleteAnswer;
        private RelayCommand? creatQuestion;
        private RelayCommand? detailsQuestion;
        private RelayCommand? deleteQuestion;
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
                        Topic = Controls.SelectTopic.Name,
                        Answers = new(Controls.Answers)
                    };

                    if (!TMS.SaveQuestion(question))
                    {
                        ErrorMessage(
                            "Добавление вопроса с текущими параметрами не возможно!\n" +
                            "Попробуйте изменить формулировку вопроса или ответов",
                            "Ошибка при добавлении вопроса");
                    }
                    else
                    {
                        InfoMessage("Вопрос добавлен", "Удачное добавление вопроса");
                    }
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
                        Controls.SelectTopic = TMS.GetTopic(Controls.SelectQuestion.Topic);
                        Controls.TextQuestion = Controls.SelectQuestion.Text;
                        Controls.Answers = new(Controls.SelectQuestion.Answers);

                        //AppWindows.ShowDialog(AppWindows.SetDataContext(
                        //    "LookAnswers",
                        //    Builder.Resolve<LookAnswers_VM>(
                        //        new NamedParameter("Question",
                        //        Controls.SelectQuestion))
                        //    ));
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
                        Controls.Questions.Remove(Controls.SelectQuestion);
                    }
                });
            }
        }
    }
}
