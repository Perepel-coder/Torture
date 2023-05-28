using Services.Crypto;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using Services.Tasks;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using trans = Services.Crypto.TransformData;
using ViewModels.Interfaces;
using ViewModels.Models.Tasks;
using Services;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModels.Researcher.Tasks
{
    public class KeySelect_VM : TaskVM
    {
        private readonly BlockEncryption encrypt;
        private Thread thread = null!;
        private bool threadFlag = false;
        public KeySelect_Controls Controls { get; set; }
        public KeySelect_VM(
            KeySelect_Controls controls,
            ICryptoAlgService CAS,
            BlockEncryption encrypt,
            ATask task)
        {
            Controls = controls;
            this.encrypt = encrypt;
            Name = typeof(KeySelect_VM).Name;
            Controls.ListOfAlgMode = new(CAS.GetModes());
            Controls.ListOfAlg = new(CAS.GetAlgs("Одноключевой"));
            Controls.Tools.Alg = Controls.ListOfAlg.FirstOrDefault();
            Controls.Tools.Mode = Controls.ListOfAlgMode.FirstOrDefault();
            Set((SelectKey_S)task);
        }
        private void Set(SelectKey_S task)
        {
            Controls.TextTask = task.Text;
            Controls.ClassName = task.ClassName;
            Controls.MethodName = task.MethodName;
            Controls.HtmlCode =
                Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\" +
                task.CodeHTML;
            string filepath = Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\Files\" +
                task.EncryptMSG;
            FileStream stream = new(filepath, FileMode.Open);
            Controls.EncryptMSG = trans.ToString(StreamProcess.OpenFile(stream));
            Controls.TrueAnswer = task.Answer;
            Controls.Tools.Dchar = '~';
            Controls.Tools.Vec = 1234;
            var temp = CreatTask_XML.ReadTestCases_XML<object>(
                Environment.CurrentDirectory +
                @"\Researcher\Tasks\Resourses\Files\" +
                task.Alphabet);
            Controls.Alphabet[0] = Tester
                .CastArrays<char>(temp.First().First() as Array)
                .ToArray();
        }

        private RelayCommand? compiler;
        private RelayCommand? start;
        private RelayCommand? stop;
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
                            InfoMessage("Успешно скомпилировано! Далее, нажмите кнопку Start!",
                                "Успешная компиляция");
                        }
                        catch (Exception ex)
                        {

                            ErrorMessage(
                                ex.Message + $"\n\nЗначение счетчика попыток: {Counter.Value}"
                                , "Ошибка компиляции");
                            return;
                        }
                    });
                    Counter.Increase();
                    thread.Start();
                });
            }
        }

        public ICommand Start
        {
            get
            {
                return start ??= new RelayCommand(imageControl =>
                {
                    try
                    {
                        thread = new Thread(() =>
                        {
                            while (threadFlag)
                            {
                                Controls.Tools.Key = Tester.InvokeFun<object, string>(
                                Controls.Alphabet,
                                Controls.Compiler);
                                encrypt.Registration(Controls.Tools, Controls.DataCode);
                                Controls.Answer = trans.ToString(encrypt.Decode());

                                if (Tester.GetComparison(Controls.TrueAnswer, Controls.Answer))
                                {
                                    InfoMessage("Загадка разгадана!", "Удачное решение");
                                    threadFlag = false;
                                }
                            }
                        });
                        if(Controls.Compiler == null)
                        {
                            return;
                        }
                        threadFlag = true;
                        thread.Start();
                    }
                    catch
                    {
                        ErrorMessage("Проверьте корректность введенных параметров!",
                            "Ошибка в вычислениях");
                    }
                });
            }
        }
        public ICommand Stop
        {
            get
            {
                return stop ??= new RelayCommand(imageControl =>
                {
                    threadFlag = false;
                });
            }
        }
    }
}
