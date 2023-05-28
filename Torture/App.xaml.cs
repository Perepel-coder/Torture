using Autofac;
using System;
using System.Windows;
using System.Windows.Threading;
using View;
using View.Autofac;
using View.Researcher;
using View.Researcher.Scripts;
using View.Tutor;
using View.Tutor.Resourses;
using ViewModels;
using ViewModels.Interfaces;

namespace Torture
{
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private static void ShowDialog<T>(Window window, bool dialog, T? viewModel = null) where T : class
        {
            if (viewModel != null) 
            { 
                window.DataContext = viewModel; 
            }
            Current.MainWindow = window;
            if (dialog)
            {
                Current.MainWindow.ShowDialog();
            }
            else
            {
                Current.MainWindow.Show();
            }
        }

        public static bool Authorization<T>(string login, T password)
        {
            var model = Current.MainWindow.DataContext as IAuthorization<T>;
            if(model != null)
            {
                return model.Authorization(login, password);
            }
            throw new Exception("Авторизация не возможна");
        }
        public static bool Registration<T>(string login, T password)
        {
            var model = Current.MainWindow.DataContext as IAuthorization<T>;
            if (model != null)
            {
                return model.Registration(login, password);
            }
            throw new Exception("Авторизация не возможна");
        }

        private void WindowsClose()
        {
            foreach (Window window in Current.Windows)
            {
                window.Close();
            }
        }
        private void WindowsHide()
        {
            foreach (Window window in Current.Windows)
            {
                window.Hide();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builderBase = new ContainerBuilder();
            builderBase.RegisterModule(new ViewAuto());
            IContainer builder = builderBase.Build();
            BaseViewModel.Builder = builder;

            AppWindows.Add("ScriptTask", typeof(ScriptTask_Window));
            AppWindows.Add("UserInfo", typeof(UserInfoWindow));
            AppWindows.Add("AddGroup", typeof(AddGroupWindow));
            AppWindows.Add("LookAnswers", typeof(LookAnswersWindow));
            AppWindows.Add("CreatQuestion", typeof(CreatQuestionWindow));
            AppWindows.Add("LookDetails", typeof(LookDetailsWindow));
            AppWindows.Add("CreatTask", typeof(CreatTaskWindow));
            AppWindows.Add("CreatTopic", typeof(CreatTopicWindow));

            var autho = builder.Resolve<Authorization>();
            var autho_vm = builder.Resolve<Authorization_VM>();
            ShowDialog(autho, true, autho_vm);

            if(autho_vm.CurrentVM != null)
            {
                Window? window = null;
                if(autho_vm.CurrentVM.Name == "UserMW_VM")
                {
                    window = builder.Resolve<UserMainWindow>();
                }
                else if(autho_vm.CurrentVM.Name == "TutorMW_VM")
                {
                    window = builder.Resolve<TutorMainWindow>();
                }
                if(window != null)
                {
                    ShowDialog(window, false, autho_vm.CurrentVM);
                    Current.Windows[0].Close();
                }      
            }
            else
            {
                WindowsClose();
            }
        }
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ошибка\n" + e.Exception.StackTrace + " " + "Исключение: "
                            + e.Exception.GetType().ToString() + " " + e.Exception.Message);

            e.Handled = true;
        }
    }
}
