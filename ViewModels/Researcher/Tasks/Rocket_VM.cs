using Services.Models;
using Services.Tasks;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ViewModels.Interfaces;
using ViewModels.Models.Tasks;
using WpfAnimatedGif;

namespace ViewModels.Researcher.Tasks
{
    public class Rocket_VM : TaskVM
    {
        public Rocket_Controls Controls { get; set; }

        public Rocket_VM(Rocket_Controls controls,
            ATask task)
        {
            Name = typeof(Rocket_VM).Name;
            Controls = controls;
            Set(task);
            Counter = new(start: 0, step: 1);
        }

        private void Set(ATask task)
        {
            MethodProgramm_S compiler_S = (MethodProgramm_S)task;
            Controls.TextTask = compiler_S.Text;
            Controls.ClassName = compiler_S.ClassName;
            Controls.MethodName = compiler_S.MethodName;
            Controls.HtmlCode = 
                Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\" + 
                compiler_S.CodeHTML;
            Controls.TestCasesTask = CreatTask_XML.ReadTestCases_XML<object>(
                Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\Files\" +
                compiler_S.TestCases);
            Controls.Answer = CreatTask_XML.ReadTestCases_XML<object>(
                Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\Files\" +
                compiler_S.Answer);
        }

        private RelayCommand? compiler;
        public ICommand Compiler
        {
            get
            {
                return compiler ??= new RelayCommand(imageControl =>
                {
                    Thread thread = new(() =>
                    {
                        try
                        {
                            Controls.Compiler = TextCompiler.Compiler(
                                Controls.Code,
                                Controls.ClassName,
                                Controls.MethodName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage(
                                ex.Message + $"\n\nЗначение счетчика попыток: {Counter.Value}"
                                , "Ошибка компиляции");
                            return;
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                Tester.GetTest(Controls.TestCasesTask, Controls.Answer, Controls.Compiler);
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage(ex.Message +
                                    $"\n\nЗначение счетчика попыток: {Counter.Value}",
                                    "Ошибка тестирования");
                                return;
                            }
                            if (Controls.Image == null)
                            {
                                Controls.Image = (Image)imageControl;
                            }
                            ImageBehavior.SetRepeatBehavior(Controls.Image, new RepeatBehavior(3));
                            ImageBehavior.GetAnimationController(Controls.Image).Play();
                            TaskCompleted = true;
                            InfoMessage(
                                "Правильный ответ!\n" +
                                $"Значение счетчика попыток: {Counter.Value}",
                                "Внимание правильный ответ!");
                        });
                    });
                    Counter.Increase();
                    thread.Start();
                });
            }
        }
    }
}
