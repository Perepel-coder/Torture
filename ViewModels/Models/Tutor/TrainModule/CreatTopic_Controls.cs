using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models.Tutor.TrainModule
{
    public class CreatTopic_Controls : ReactiveObject
    {
        private string topic = string.Empty;
        public string Topic
        {
            get => topic;
            set => this.RaiseAndSetIfChanged(ref topic, value);
        }
    }
}
