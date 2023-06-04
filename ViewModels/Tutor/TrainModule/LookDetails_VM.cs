using Services.Crypto;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using Services.Tasks;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.TrainModule;

namespace ViewModels.Tutor.TrainModule
{
    public class LookDetails_VM : BaseViewModel
    {
        private readonly ITrainModuleService TMS;
        public LookDetails_Controls Controls { get; set; }
        public LookDetails_VM(
            LookDetails_Controls controls,
            ITrainModuleService trainModuleService,
            ATask task)
        {
            Controls = controls;
            TMS = trainModuleService;
            Controls.Task = task;
            Controls.HtmlCode = Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\" + task.CodeHTML;
            if(Controls.Task.Discriminator == "BaseMath")
            {
                Controls.BaseMath = Visibility.Visible;
                Controls.BaseMathTask = TMS.GetBasicMath(task.Id);
            }
            if(Controls.Task.Discriminator == "MethodProgramm")
            {
                Controls.MethodProgramm = Visibility.Visible;
                Controls.MethodProgrammTask = TMS.GetMethodP(task.Id);
                Controls.MethodProgrammTask.TestCases = StreamProcess.Read_XML(
                    Environment.CurrentDirectory +
                    @"\Researcher\Tasks\Resourses\Files\" +
                    Controls.MethodProgrammTask.TestCases);
                Controls.MethodProgrammTask.Answer = StreamProcess.Read_XML(
                    Environment.CurrentDirectory +
                    @"\Researcher\Tasks\Resourses\Files\" +
                    Controls.MethodProgrammTask.Answer);
            }
            if (Controls.Task.Discriminator == "SelectKey")
            {
                Controls.SelectKey = Visibility.Visible;
                Controls.SelectKeyTask = TMS.GetSelectKey(task.Id);
                var data = CreatTask_XML.ReadTestCases_XML<object>(
                    Environment.CurrentDirectory +
                    @"\Researcher\Tasks\Resourses\Files\" +
                    Controls.SelectKeyTask.Alphabet);
                Controls.SelectKeyTask.Alphabet = string.Join(" ",
                    Tester.CastArrays<char>(data.First()[0] as Array)
                    .ToArray());
                Controls.SelectKeyTask.EncryptMSG = TransformData.ToString(
                    StreamProcess.OpenFile(
                        new FileStream(Environment.CurrentDirectory +
                        @"\Researcher\Tasks\Resourses\Files\" +
                        Controls.SelectKeyTask.EncryptMSG, FileMode.Open)
                        )
                    );
            }
        }
    }
}
