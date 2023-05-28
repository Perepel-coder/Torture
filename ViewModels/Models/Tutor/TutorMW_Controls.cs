using ReactiveUI;
using Services.Models;
using ViewModels.Interfaces;

namespace ViewModels.Models
{
    public class TutorMW_Controls : ReactiveObject
    {
        public Tutor_S Tutor { get; set; } = null!;
        private BaseViewModel currentVM_TutorVM = null!;
        public BaseViewModel CurrentVM_TutorVM
        {
            get => currentVM_TutorVM;
            set => this.RaiseAndSetIfChanged(ref currentVM_TutorVM, value);
        }
    }
}
