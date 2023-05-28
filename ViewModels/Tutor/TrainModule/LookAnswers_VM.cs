using Services.Models;
using System.Collections.ObjectModel;
using ViewModels.Interfaces;

namespace ViewModels.Tutor.TrainModule
{
    public class LookAnswers_VM: BaseViewModel
    {
        public ObservableCollection<Answer_S> Answers { get; set; } = new();
        public Question_S Question { get; set; } = new();
        public LookAnswers_VM(Question_S question)
        {
            Question = question;
            Answers = new(question.Answers);
        }
    }
}
