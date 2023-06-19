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
    public class CreatTopic_VM : BaseViewModel
    {
        private readonly ITrainModuleService TMS;
        public CreatTopic_Controls Controls { get; set; }
        public CreatTopic_VM(
            CreatTopic_Controls controls,
            ITrainModuleService trainModuleService)
        {
            Controls = controls;
            TMS = trainModuleService;
            Controls.Topic = "Новая тема";
        }
        private RelayCommand? save;
        public ICommand Save
        {
            get
            {
                return save ??= new RelayCommand(obj =>
                {
                    if(Controls.Topic.Replace(" ", "") != string.Empty)
                    {
                        TMS.SaveTopic(Controls.Topic);
                    }
                });
            }
        }
    }
}
