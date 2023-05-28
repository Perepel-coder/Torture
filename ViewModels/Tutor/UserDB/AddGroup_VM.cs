using Services.Models;
using Services.RequestDB.InterfaceDB;
using System;
using System.Linq;
using System.Windows.Input;
using ViewModels.Interfaces;
using ViewModels.Models.Tutor;

namespace ViewModels.Tutor.UserDB
{
    public class AddGroup_VM : BaseViewModel
    {
        public AddGroup_Controls Controls { get; set; }
        private readonly IUserService US;
        public AddGroup_VM(
            AddGroup_Controls controls,
            IUserService userService)
        {
            Controls = controls;
            Controls.Tutor = (Tutor_S)User;
            US = userService;
            Controls.FreeResearchers = new(US.GetFreeResearchers());
        }
        private RelayCommand? addUser;
        private RelayCommand? save;
        private RelayCommand? delete;
        public ICommand AddUser
        {
            get
            {
                return addUser ??= new RelayCommand(obj =>
                {
                    try
                    {
                        if (Controls.SelectedRes != null && Controls.CurrentResearchers
                        .SingleOrDefault(r => r.Login == Controls.SelectedRes.Login) == null)
                        {
                            Controls.CurrentResearchers.Add(Controls.SelectedRes);
                            return;
                        }
                        if (Controls.CurrentResearchers
                        .SingleOrDefault(r => r.Login == Controls.Login) == null)
                        {
                            var researcher = new Researcher_S
                            {
                                Login = Controls.Login != null ?
                                Controls.Login :
                                throw new Exception("Недопустимый логин"),
                                Password = "User"
                            };
                            Controls.CurrentResearchers.Add(researcher);
                            return;
                        }
                        throw new Exception("Пользователь с таким логином уже добавлен");
                    }
                    catch(Exception ex)
                    {
                        ErrorMessage(ex.Message, "Ошибка при создании группы");
                    }
                });
            }
        }
        public ICommand Save
        {
            get
            {
                return save ??= new RelayCommand(obj =>
                {
                    if(Controls.GroupName!=null && 
                    Controls.GroupName.Replace(" ", "") != "" &&
                    US.GetGroupName(Controls.GroupName) == null &&
                    Controls.CurrentResearchers.Count != 0)
                    {
                        if(!US.SaveGroup(new Group_S
                        {
                            Name = Controls.GroupName,
                            TutorId = Controls.Tutor.ID,
                            Researchers = new(Controls.CurrentResearchers)
                        }))
                        {
                            ErrorMessage("Группа не была добавлена!", 
                                "Ошибка при добавлении группы");
                            return;
                        }
                        InfoMessage("Группа успешно добавлена!");
                    }
                    else
                    {
                        ErrorMessage("Группа с текущими праметрами не может быть добавлена!", 
                            "Ошибка при добавлении группы");
                        return;
                    }
                });
            }
        }
        public ICommand Delete
        {
            get
            {
                return delete ??= new RelayCommand(obj =>
                {
                    if (Controls.CurrentRes != null)
                    {
                        Controls.CurrentResearchers.Remove(Controls.CurrentRes);
                    }
                    Controls.CurrentRes = null;
                });
            }
        }
    }
}
