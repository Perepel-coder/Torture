using Autofac;
using ReactiveUI;
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
    public class ChangeScriptName_VM:BaseViewModel
    {
        public CreatScrip_Controls Controls { get; set; }
        public string Name { get; set; } = string.Empty;
        public ChangeScriptName_VM(CreatScrip_Controls controls)
        {
            Controls = controls;
            Name = Controls.SelectScript.Name;
        }

        private RelayCommand? save;
        public ICommand Save
        {
            get
            {
                return save ??= new RelayCommand(obj =>
                {
                    Controls.SelectScript.Name = Name;
                    
                });
            }
        }
    }
}
