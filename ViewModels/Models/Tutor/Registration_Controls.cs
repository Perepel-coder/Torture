using ReactiveUI;
using Services.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModels.Models.Tutor
{
    public class Registration_Controls : ReactiveObject
    {
        private string login = string.Empty;
        private string password = string.Empty;
        private int role;
        public string Login
        {
            get => login;
            set => this.RaiseAndSetIfChanged(ref login, value);
        }
        public string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }
        public int Role
        {
            get => role;
            set => this.RaiseAndSetIfChanged(ref role, value);
        }
        public static string PasswordGenerator(int start)
        {
            var rand = new Random(start);
            List<byte> bytes = new();
            for(int i = 0; i < rand.Next(5, 11); i++)
            {
                bytes.Add((byte)rand.Next(65, 90));
            }
            return TransformData.ToString(bytes);
        }
    }
}