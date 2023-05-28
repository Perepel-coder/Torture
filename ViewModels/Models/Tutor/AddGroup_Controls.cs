using ReactiveUI;
using Services.Models;
using System.Collections.ObjectModel;

namespace ViewModels.Models.Tutor
{
    public class AddGroup_Controls : ReactiveObject
    {
        public ObservableCollection<Researcher_S> FreeResearchers { get; set; } = new();
        public ObservableCollection<Researcher_S> CurrentResearchers { get; set; } = new();
        public Tutor_S Tutor { get; set; } = null!;
        private string? groupName;
        private string? login;
        public string? GroupName
        {
            get => groupName;
            set => this.RaiseAndSetIfChanged(ref groupName, value);
        }
        public string? Login
        {
            get => login;
            set 
            { 
                this.RaiseAndSetIfChanged(ref login, value);
                if(SelectedRes != null && SelectedRes.Login != login)
                {
                    SelectedRes = null;
                }
            }
        }

        private Researcher_S? selectedRes;
        private Researcher_S? currentRes;
        public Researcher_S? SelectedRes
        {
            get => selectedRes;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedRes, value);
                if(selectedRes != null)
                {
                    Login = selectedRes.Login;
                }
            }
        }
        public Researcher_S? CurrentRes
        {
            get => currentRes;
            set => this.RaiseAndSetIfChanged(ref currentRes, value);
        }
    }
}
