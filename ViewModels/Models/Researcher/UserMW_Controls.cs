using ReactiveUI;
using ViewModels.Interfaces;

namespace ViewModels.Models
{
    public class UserMW_Controls : ReactiveObject
    {
        private BaseViewModel currentVM_UserVM = null!;
        public BaseViewModel CurrentVM_UserVM
        {
            get => currentVM_UserVM;
            set => this.RaiseAndSetIfChanged(ref currentVM_UserVM, value);
        }
    }
}
