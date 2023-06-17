using Services.Models;
using System.Collections.Generic;

namespace Services.RequestDB.InterfaceDB
{
    public interface IUserService
    {
        User_S? GetUser(string login, string password);
        Tutor_S? GetTutor(int id);
        Researcher_S? GetResearcher(int id);
        IEnumerable<Researcher_S> GetFreeResearchers();
        public IEnumerable<Group_S> GetGroups(int tutorID);
        string? GetGroupName(int id);
        Group_S? GetGroupName(string Name);
        ScriptUser_S GetScript(Script_S script, bool questions, bool tasks);
        bool SaveScript(ScriptUser_S script, int userId);
        bool SaveGroup(Group_S group);
        bool UpdateGroup(Group_S group);
        bool DeleteGroup(Group_S group);
        bool RegUser(string login, string password, USER_ROLE role);
    }
}
