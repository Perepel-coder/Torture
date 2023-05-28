using Repo;
using Services.RequestDB.InterfaceDB;
using Services.Models;
using System.Collections.Generic;
using Models;
using System.Linq;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Tutor> tutorRepo;
        private readonly IRepository<Researcher> researcherRepo;
        private readonly IRepository<Group> groupRepo;
        private readonly IRepository<TestUser> testRepo;
        private readonly AssessmentSystem assessment;

        public UserService(
            IRepository<User> userRepo,
            IRepository<Tutor> tutorRepo, 
            IRepository<Group> groupRepo,
            IRepository<Researcher> researcherRepo,
            IRepository<TestUser> testRepo,
            AssessmentSystem assessment)
        {
            this.userRepo = userRepo;
            this.tutorRepo = tutorRepo;
            this.groupRepo = groupRepo;
            this.researcherRepo = researcherRepo;
            this.testRepo = testRepo;

            this.assessment = assessment;
        }

        public IEnumerable<Researcher_S> GetFreeResearchers()
        {
            return researcherRepo
                .GetAll()
                .Where(r => r.GroupId == null)
                .Select(r => new Researcher_S
                {
                    ID = r.ID,
                    Login = r.Login,
                    Discriminator = r.Discriminator
                });
        }
        public User_S? GetUser(string login, string password)
        {
            User? user = userRepo
                .GetAll()
                .SingleOrDefault(u => u.Login == login && u.Password == password);
            if(user != null)
            {
                if(user.Discriminator == typeof(Researcher).Name)
                {
                    return GetResearcher(user.ID);
                }
                if(user.Discriminator == typeof(Tutor).Name)
                {
                    return GetTutor(user.ID);
                }
            }
            return null;
        }
        public Researcher_S? GetResearcher(int id)
        {
            var researcher = researcherRepo.Include("Scripts").Single(r=>r.ID == id);
            if(researcher != null)
            {
                return new Researcher_S
                {
                    ID = researcher.ID,
                    Login = researcher.Login,
                    Discriminator = researcher.Discriminator,
                    GroupId = researcher.GroupId,
                    Scripts = GetScripts(researcher.Scripts)
                };
            }
            return null;
        }
        public Tutor_S? GetTutor(int id)
        {
            var tutor = tutorRepo.Get(id);
            if (tutor != null)
            {
                return new Tutor_S
                {
                    ID = tutor.ID,
                    Login = tutor.Login,
                    Discriminator = tutor.Discriminator,
                    Groups = GetGroups(tutor.ID).ToList()
                };
            }
            return null;
        }
        public IEnumerable<Group_S> GetGroups(int tutorID)
        {
            return groupRepo.Include("Researchers")
                .Where(g => g.TutorId == tutorID)
                .Select(g => new Group_S
                {
                    Id = g.ID,
                    Name = g.Name,
                    TutorId = g.TutorId,
                    Researchers = GetResearchers(g.ID)
                });
        }
        public List<Researcher_S?>? GetResearchers(int groupID)
        {
            var group = groupRepo.Include("Researchers").Single(g=>g.ID == groupID);
            if(group != null)
            {
                var res = group.Researchers;
                if (res != null)
                {
                    return res.Select(r => GetResearcher(r.ID)).ToList();
                } 
            }
            return null;
        }
        public Group_S? GetGroupName(string Name)
        {
            var group = groupRepo
                .GetAll()
                .SingleOrDefault(g => g.Name.ToLower() == Name.ToLower());
            if (group == null)
            {
                return null;
            }
            return new Group_S
            {
                Id = group.ID,
                Name = group.Name,
                TutorId = group.TutorId,
                Researchers = GetResearchers(group.ID).ToList()
            };
        }
        public string? GetGroupName(int id)
        {
            var group = groupRepo.Get(id);
            if (group != null)
            {
                return group.Name;
            }
            return null;
        }
        public ScriptUser_S GetScript(Script_S script, bool questions, bool tasks)
        {
            var res = new ScriptUser_S
            {
                ScriptId = script.Id,
                Name = script.Name,
                Topic = script.Topic ??= "Без темы",
                Test = new TestUser_S
                {
                    TestNum = script.Test.Id,
                    Name = script.Test.Name
                }
            };
            if (questions)
            {
                res.Test.Questions = GetQuestions(script.Test.Questions).ToList();
            }
            if (tasks)
            {
                res.Test.Tasks = GetTasks(script.Test.Tasks).ToList();
            }
            return res;
        }
        private List<ScriptUser_S>? GetScripts(IEnumerable<ScriptUser> scripts)
        {
            if(scripts != null)
            {
                return scripts.Select(s => new ScriptUser_S
                {
                    ScriptId = s.ScriptId,
                    Name = s.Name,
                    Topic = s.Topic,
                    Score = s.ScriptScore,
                    Test = GetTest(s.ID)
                }).ToList();
            }
            return null;
        }
        private TestUser_S GetTest(int scriptID)
        {
            testRepo.Include("Questions");
            testRepo.Include("Tasks");
            var test = testRepo.GetAll().Single(t => t.ScriptUserId == scriptID);
            return new TestUser_S
            {
                TestNum = test.TestNum,
                Name = test.Name,
                QuestionsScore = test.QuestionsScore,
                TasksScore = test.TasksScore,
                Questions = GetQuestions(test.Questions).ToList(),
                Tasks = GetTasks(test.Tasks).ToList()
            };
        }
        private IEnumerable<QuestionTestUser_S> GetQuestions(IEnumerable<QuestionTestUser> questions)
        {
            return questions.Select(q => new QuestionTestUser_S
            {
                QuestionNum = q.QuestionNum,
                Text = q.Text,
                Score = q.QuestionScore
            });
        }
        private IEnumerable<TaskUser_S> GetTasks(IEnumerable<TaskUser> tasks)
        {
            return tasks.Select(t => new TaskUser_S
            {
                TaskNum = t.TaskNum,
                Name = t.Name,
                Score = t.TaskScore
            });
        }
        private IEnumerable<QuestionTestUser_S> GetQuestions(IEnumerable<Question_S> questions)
        {
            return questions.Select(q => new QuestionTestUser_S
            {
                QuestionNum = q.Id,
                Text = q.Text,
                Score = assessment.Assessment(q) ? 1 : 0
            });
        }
        private IEnumerable<TaskUser_S> GetTasks(IEnumerable<ATask> tasks)
        {
            return tasks.Select(t => new TaskUser_S
            {
                TaskNum = t.Id,
                Name = t.Name
            });
        }




        public bool SaveTutor(string login, string password)
        {
            try
            {
                int count = tutorRepo.GetAll().Where(t => t.Login == login).Count();
                if(count != 0)
                {
                    return false;
                }
                tutorRepo.Insert(new Tutor
                {
                    Login = login,
                    Password = password,
                    Discriminator = typeof(Tutor).Name
                });
                tutorRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SaveScript(ScriptUser_S script, int userID)
        {
            var res = researcherRepo.Get(userID);
            var Scripts = researcherRepo.Get(userID).Scripts;
            if (Scripts == null)
            {
                Scripts = new();
            }
            Scripts.Add(new ScriptUser
            {
                ScriptId = script.ScriptId,
                Name = script.Name,
                Topic = script.Topic,
                ScriptScore = script.Score,
                ResearcherId = userID,
                Test = new TestUser
                {
                    TestNum = script.Test.TestNum,
                    Name = script.Test.Name,
                    QuestionsScore = script.Test.QuestionsScore,
                    TasksScore = script.Test.TasksScore,
                    Questions = script.Test.Questions.Select(q => new QuestionTestUser
                    {
                        QuestionNum = q.QuestionNum,
                        Text = q.Text,
                        QuestionScore = q.Score
                    }).ToList(),
                    Tasks = script.Test.Tasks.Select(t => new TaskUser
                    {
                        TaskNum = t.TaskNum,
                        Name = t.Name,
                        TaskScore = t.Score
                    }).ToList()
                }
            });

            try
            {
                researcherRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SaveGroup(Group_S group)
        {
            try
            {
                var mygroup = new Group
                {
                    Name = group.Name,
                    TutorId = group.TutorId
                };
                groupRepo.Insert(mygroup);
                foreach (var r in group.Researchers)
                {
                    if (r.ID == 0)
                    {
                        var res = new Researcher
                        {
                            Login = r.Login,
                            Password = r.Password,
                            Discriminator = typeof(Researcher).Name,
                            GroupId = mygroup.ID
                        };
                        researcherRepo.Insert(res);
                    }
                    else
                    {
                        var res = researcherRepo.Get(r.ID);
                        res.GroupId = mygroup.ID;
                    }
                }
                researcherRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateGroup(Group_S group)
        {
            try
            {
                var groupR = groupRepo
                    .Include("Researchers")
                    .SingleOrDefault(g => g.ID == group.Id);
                groupR.Name = group.Name;

                for (int i = 0; i < groupR.Researchers.Count; i++)
                {
                    if (group.Researchers.SingleOrDefault(gr =>
                    gr.ID == groupR.Researchers[i].ID) == null)
                    {
                        groupR.Researchers.Remove(groupR.Researchers[i]);
                        i--;
                    }
                }
                foreach (var r in groupR.Researchers)
                {
                    if (group.Researchers.SingleOrDefault(gr => gr.ID == r.ID) == null)
                    {
                        groupR.Researchers.Remove(r);
                    }
                }

                foreach (var r in group.Researchers)
                {
                    if (r.ID == 0)
                    {
                        var res = new Researcher
                        {
                            Login = r.Login,
                            Password = r.Password,
                            Discriminator = typeof(Researcher).Name,
                            GroupId = groupR.ID
                        };
                        researcherRepo.Insert(res);
                    }
                    else
                    {
                        var res = researcherRepo.Get(r.ID);
                        res.GroupId = groupR.ID;
                    }
                }
                groupRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteGroup(Group_S group)
        {
            try
            {
                groupRepo.Delete(groupRepo.Get(group.Id));
                groupRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
