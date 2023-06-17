using System.Windows.Input;
using ViewModels.Interfaces;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using ViewModels.Models.Tutor.TrainModule;
using ViewModels.Models.Tutor;
using ViewModels.Tutor.TrainModule;
using Autofac;
using System;

namespace ViewModels.Tutor
{
    public class Registration_VM : BaseViewModel
    {
        private readonly IUserService US;
        public Registration_Controls Controls { get; set; }
        public Registration_VM(
            Registration_Controls controls,
            IUserService userService)
        {
            Controls = controls;
            Controls.Login = "Tutor";
            Controls.Role = 0;
            Generator.Execute(null);
            US = userService;
        }
        private RelayCommand? registration;
        private RelayCommand? generator;
        public ICommand Generator
        {
            get
            {
                return generator ??= new RelayCommand(obj =>
                {
                    Controls.Password = Registration_Controls
                    .PasswordGenerator(DateTime.Now.Millisecond);
                });
            }
        }
        public ICommand Registration
        {
            get
            {
                return registration ??= new RelayCommand(obj =>
                {
                   if(Controls.Login.Replace(" ", "") != string.Empty &&
                    Controls.Login != string.Empty)
                    {
                        if(US.RegUser(Controls.Login, Controls.Password, (USER_ROLE)Controls.Role))
                        {
                            InfoMessage("Пользователь зарегистрирован!");
                        }
                        else
                        {
                            ErrorMessage("Регистрация пользователя не удалась!");
                        }
                    }
                });
            }
        }
    }
}