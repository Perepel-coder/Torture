using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models.Tutor.UserDB;

namespace ViewModels.Tutor.UserDB
{
    public class CuratorRegistration_VM
    {
        public CuratorRegistration_Controls Controls { get; set; }
        public CuratorRegistration_VM(CuratorRegistration_Controls controls)
        {
            Controls = controls;
        }
    }
}
