﻿using Autofac;
using Services;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System.Linq;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Script;
using ViewModels.Researcher.Tasks;

namespace ViewModels.Researcher.Script
{
    public class TaskScript_Panel_VM : BaseViewModel
    {
        public TaskScript_Panel_Controls Controls { get; set; }
        private readonly ITrainModuleService TMS;
        private readonly IUserService US;
        private readonly AssessmentSystem score;
        private readonly ScriptUser_S userScript;
        public TaskScript_Panel_VM(
            TaskScript_Panel_Controls controls,
            ITrainModuleService trainModuleService,
            IUserService userService,
            AssessmentSystem assessment,
            AScript script,
            ScriptUser_S userScript)
        {
            Controls = controls;
            TMS = trainModuleService;
            US = userService;
            this.score = assessment;
            Controls.Script = script as Script_S;
            this.userScript = userScript;
        }
        private RelayCommand? getTask;
        private RelayCommand? saveResult;
        public ICommand GetTask
        {
            get
            {
                return getTask ??= new RelayCommand(obj =>
                {
                    if(Controls.SelectTask != null)
                    {
                        TaskVM model = null!;
                        ATask task = null!;
                        if (Controls.SelectTask.Discriminator == "BaseMath")
                        {
                            task = TMS.GetBasicMath(
                                Controls.SelectTask.Id, 
                                Controls.SelectTask.Type);
                            model = Builder.Resolve<CombLock_VM>(
                                new NamedParameter("Task", task));

                        }
                        if (Controls.SelectTask.Discriminator == "MethodProgramm")
                        {
                            task = TMS.GetMethodP(Controls.SelectTask.Id);
                            model = Builder.Resolve<Rocket_VM>(
                                new NamedParameter("Task", task));
                            
                        }
                        AppWindows.ShowDialog(AppWindows.SetDataContext("ScriptTask", model));

                        if(userScript.Test.Tasks == null)
                        {
                            userScript.Test.Tasks = new();
                        }
                        userScript.Test.Tasks.Single(t => t.TaskNum == task.Id).Score = 
                        score.Assessment(model.TaskCompleted, model.Counter);
                    }
                });
            }
        }
        public ICommand SaveResult
        {
            get
            {
                return saveResult ??= new RelayCommand(obj =>
                {
                    score.Assessment(userScript);
                    US.SaveScript(userScript, User.ID);
                });
            }
        }
    }
}