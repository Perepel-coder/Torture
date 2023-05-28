using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models.Tutor.UserDB
{
    public class UserDB_Controls : ReactiveObject
    {
        public Tutor_S Tutor { get; set; } = null!;

        private List<Group_S> groups = new();
        public List<Group_S> Groups
        {
            get => groups;
            set => this.RaiseAndSetIfChanged(ref groups, value);
        }

        private Researcher_S? currentResearcher;
        private Group_S? currentGroup;
        public Researcher_S? CurrentResearcher
        {
            get => currentResearcher;
            set => this.RaiseAndSetIfChanged(ref currentResearcher, value);
        }

        public Group_S? CurrentGroup
        {
            get => currentGroup;
            set => this.RaiseAndSetIfChanged(ref currentGroup, value);
        }
    }
}
