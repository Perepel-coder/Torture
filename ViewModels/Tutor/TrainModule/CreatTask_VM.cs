using Autofac;
using Services;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using Services.Tasks;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.TrainModule;

namespace ViewModels.Tutor.TrainModule
{
    public class CreatTask_VM : BaseViewModel
    {
        public CreatTask_Controls Controls { get; set; }
        private readonly DialogService DS;
        private readonly ITrainModuleService TMS;
        public CreatTask_VM(
            CreatTask_Controls controls,
            ITrainModuleService trainModuleService,
            DialogService dialogService)
        {
            Controls = controls;
            TMS = trainModuleService;
            DS = dialogService;
            Controls.ClassTask = new(TMS.GetDiscriminators());
        }
        private RelayCommand? download;
        private RelayCommand? _continue;
        private RelayCommand? save;
        public ICommand Download
        {
            get
            {
                return download ??= new RelayCommand(obj =>
                {
                    if (DS.OpenFileDialog() == true)
                    {
                        File.Copy(DS.FilePath, Environment.CurrentDirectory +
                            @"\Researcher\Tasks\Resourses\" + 
                            DS.FileName, 
                            true);
                        Controls.TextDownload = DS.FileName;
                    }
                    else
                    {
                        Controls.TextDownload = string.Empty;
                    }
                });
            }
        }
        public ICommand Continue
        {
            get
            {
                return _continue ??= new RelayCommand(obj =>
                {
                    Controls.Collapsed();
                    ATask task = new ATask
                    {
                        Name = Controls.Name,
                        Text = Controls.TextTask,
                        Discriminator = Controls.SelectClassTask,
                        CodeHTML = Controls.TextDownload
                    };
                    if(Controls.SelectClassTask == "BaseMath")
                    {
                        Controls.BaseMathTask = new BaseMath_S(task);
                        Controls.TypeTask = new(TMS.GetTypeTask(task.Discriminator));
                        Controls.SelectTypeTask = Controls.TypeTask.First();
                        Controls.BaseMath = Visibility.Visible;
                    }
                    if (Controls.SelectClassTask == "SelectKey")
                    {
                        Controls.TypeTask = new(TMS.GetTypeTask(task.Discriminator));
                        Controls.SelectTypeTask = Controls.TypeTask.First();
                        Controls.SelectKey = Visibility.Visible;
                    }
                });
            }
        }
        public ICommand Save
        {
            get
            {
                return save ??= new RelayCommand(obj =>
                {
                    
                });
            }
        }
    }
}
