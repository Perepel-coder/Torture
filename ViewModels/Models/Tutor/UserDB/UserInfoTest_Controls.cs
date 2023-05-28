using ReactiveUI;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models.Tutor.UserDB
{
    public class UserInfoTest_Controls : ReactiveObject
    {
        public ScriptUser_S Script { get; set; } = null!;
    }
}
