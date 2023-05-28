using Services.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ViewModels.Interfaces;
using ViewModels.Models.Tasks;
using WpfAnimatedGif;

namespace ViewModels.Researcher.Tasks
{
    public class CombLock_VM : TaskVM
    {
        public override string Name { get; set; }
        public CombLock_Controls Controls { get; set; }

        private Image? image;

        private void Set(ATask task)
        {
            BaseMath_S baseMath = (BaseMath_S)task;
            Controls.TextTask = baseMath.Text +
                "\n\nЕсли ответ превышиет длину в четыре символа, возьмите первые цифры";
            Controls.HtmlCode =
                Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\" +
                baseMath.CodeHTML;

            Controls.StartData =
                "Первый ключ: " + baseMath.Poly1 +
                "\nВторой ключ: " + baseMath.Poly2 +
                "\nМодуль: " + baseMath.Module;
            Controls.Answer = baseMath.Answer().ToString();
            while (Controls.Answer.Length < 4)
            {
                Controls.Answer = '0' + Controls.Answer;
            }
            if (Controls.Answer.Length > 4)
            {
                Controls.Answer = Controls.Answer.Substring(4);
            }

            if(baseMath.GetType() == typeof(MultPoly))
            {
                Controls.FuncTask = MultPoly.Answer;
            }
            if(baseMath.GetType() == typeof(DivPoly))
            {
                Controls.FuncTask = DivPoly.Answer;
            }
            if (baseMath.GetType() == typeof(SumPoly))
            {
                Controls.FuncTask = DivPoly.Answer;
            }
        }

        public CombLock_VM(
            CombLock_Controls controls,
            ATask task)
        {
            Name = typeof(CombLock_VM).Name;
            Controls = controls;
            Set(task);
        }

        private RelayCommand? setCode;
        private RelayCommand? enter;
        private RelayCommand? reset;

        public ICommand SetCode
        {
            get
            {
                return setCode ??= new RelayCommand(content =>
                {
                    if(Controls.GetLock.Field_1 == null)
                    {
                        Controls.GetLock.Field_1 = Convert.ToChar(content);
                        return;
                    }
                    if (Controls.GetLock.Field_2 == null)
                    {
                        Controls.GetLock.Field_2 = Convert.ToChar(content);
                        return;
                    }
                    if (Controls.GetLock.Field_3 == null)
                    {
                        Controls.GetLock.Field_3 = Convert.ToChar(content);
                        return;
                    }
                    if (Controls.GetLock.Field_4 == null)
                    {
                        Controls.GetLock.Field_4 = Convert.ToChar(content);
                        return;
                    }
                });
            }
        }
        public ICommand Enter
        {
            get
            {
                return enter ??= new RelayCommand(imageControl =>
                {
                    if(Controls.Answer == Controls.GetLock.ToString())
                    {
                        if (image == null) 
                        { 
                            image = (Image)imageControl; 
                        }
                        ImageBehavior.SetRepeatBehavior(image, new RepeatBehavior(3));
                        ImageBehavior.GetAnimationController(image).Play();
                        TaskCompleted = true;
                        InfoMessage(
                            "Правильный ответ!\n" +
                            $"Значение счетчика попыток: " +
                            $"{Counter.Value} / {Counter.end}",
                            "Внимание правильный ответ!");
                    }
                    else
                    {
                        if(Counter.Increase())
                        {
                            Controls.IsEnabledTool = true;
                            InfoMessage(
                              "Не правильный ответ!\n" +
                              $"Значение счетчика попыток: " +
                              $"{Counter.Value} / {Counter.end}" +
                              "\nОТКРЫТ ИНСТРУМЕНТ АВТОМАТИЧЕСКОГО РАСЧЕТА !!!",
                              "Внимание ошибка!");
                        }
                        else
                        {
                            InfoMessage(
                                "Не правильный ответ!\n" +
                                $"Значение счетчика попыток: {Counter.Value} / {Counter.end}",
                                "Внимание ошибка!");
                        }
                    }
                });
            }
        }
        public ICommand Reset
        {
            get
            {
                return reset ??= new RelayCommand(obj =>
                {
                    Controls.GetLock.Reset();
                });
            }
        }

        private RelayCommand? culcFuncMath;
        private RelayCommand? culcFuncTrans;
        public ICommand CulcFuncMath
        {
            get
            {
                return culcFuncMath ??= new RelayCommand(obj =>
                {
                    Controls.MyAnswer = Controls.FuncTask(Controls.Key1, Controls.Key2, Controls.Mod);
                });
            }
        }
        public ICommand CulcFuncTrans
        {
            get
            {
                return culcFuncTrans ??= new RelayCommand(obj =>
                {
                    Controls.BinaryForm = Convert.ToString(Controls.DecimalForm, 2);
                });
            }
        }

        private RelayCommand? complete;
        public ICommand GetComplete
        {
            get
            {
                return complete ??= new RelayCommand(obj =>
                {

                });
            }
        }
    }
}