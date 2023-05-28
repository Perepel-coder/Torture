using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor.UserDB;

namespace ViewModels.Tutor.UserDB
{
    public class UserInfoTest_VM : BaseViewModel
    {
        public UserInfoTest_Controls Controls { get; set; }
        public UserInfoTest_VM(
            UserInfoTest_Controls controls,
            ScriptUser_S script)
        {
            Controls = controls;
            Controls.Script = script;
        }
    }
}
