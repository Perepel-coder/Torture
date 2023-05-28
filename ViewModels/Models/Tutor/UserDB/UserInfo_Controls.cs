using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Interfaces;

namespace ViewModels.Models.Tutor.UserDB
{
    public class UserInfo_Controls : ReactiveObject
    {
        public Researcher_S Researcher { get; set; } = null!;
        public string? Group { get; set; }

        private BaseViewModel? currentVM;
        public BaseViewModel? CurrentVM
        {
            get => currentVM;
            set => this.RaiseAndSetIfChanged(ref currentVM, value);
        }

        private ScriptUser_S? currentScript;
        public ScriptUser_S? CurrentScript
        {
            get => currentScript;
            set => this.RaiseAndSetIfChanged(ref currentScript, value);
        }
    }
}
