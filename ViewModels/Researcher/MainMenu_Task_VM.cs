﻿using Autofac;
using Services.RequestDB.InterfaceDB;
using System.Windows;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models;
using ViewModels.Researcher.Tasks;

namespace ViewModels.Researcher
{
    public class MainMenu_Task_VM : BaseViewModel
    {
        private readonly ITrainModuleService TMS;
        public MainMenu_Task_Controls Controls { get; set; }

        public MainMenu_Task_VM (
            MainMenu_Task_Controls controls,
            ITrainModuleService TMS)
        {
            Name = typeof(MainMenu_Task_VM).Name;
            Controls = controls;
            this.TMS = TMS;
            Controls.Tasks = new(TMS.GetTasks());
            Controls.VisibilityControl();
        }

        private RelayCommand? getTask;
        public ICommand GetTask
        {
            get
            {
                return getTask ??= new RelayCommand(obj =>
                {
                    if (Controls.SelectTask.Discriminator == "BaseMath")
                    {
                        var task = TMS.GetBasicMath(
                            Controls.SelectTask.Id,
                            Controls.SelectTask.Type);
                        Controls.VisibilityControl(combLock_V: Visibility.Visible);
                        Controls.CurrentVM_MenuTasks = Builder.Resolve<CombLock_VM>(
                            new NamedParameter("Task", task));
                    }
                    if (Controls.SelectTask.Discriminator == "MethodProgramm")
                    {
                        var task = TMS.GetMethodP(Controls.SelectTask.Id);
                        Controls.VisibilityControl(rocket_V: Visibility.Visible);
                        Controls.CurrentVM_MenuTasks = Builder.Resolve<Rocket_VM>(
                            new NamedParameter("Task", task));
                    }
                    if(Controls.SelectTask.Discriminator == "SelectKey")
                    {
                        var task = TMS.GetSelectKey(Controls.SelectTask.Id);
                        Controls.VisibilityControl(keySelection_V: Visibility.Visible);
                        Controls.CurrentVM_MenuTasks = Builder.Resolve<KeySelect_VM>(
                            new NamedParameter("Task", task));
                    }
                });
            }
        }
    }
}