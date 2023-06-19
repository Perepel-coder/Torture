using Models;
using Repo;
using Services.Models;
using Services.RequestDB.InterfaceDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.RequestDB
{
    public class TrainModuleService: ITrainModuleService
    {
        private readonly IRepository<Task> taskRepo;
        private readonly IRepository<BaseMath> basicMathRepo;
        private readonly IRepository<Compiler> compilerRepo;
        private readonly IRepository<MethodProgramm> methodPRepo;

        private readonly IRepository<Script> scriptRepo;
        private readonly IRepository<Topic> topicRepo;
        private readonly IRepository<Answer> answerRepo;
        private readonly IRepository<Question> questionRepo;
        private readonly IRepository<Test> testRepo;
        private readonly IRepository<SelectKey> selectkeyRepo;
        public TrainModuleService(
            IRepository<BaseMath> basicMathRepo,
            IRepository<Compiler> compilerRepo,
            IRepository<Script> scriptRepo,
            IRepository<Topic> topicRepo,
            IRepository<Answer> answerRepo,
            IRepository<Question> questionRepo,
            IRepository<Test> testRepo,
            IRepository<Task> taskRepo, 
            IRepository<MethodProgramm> methodPRepo,
            IRepository<SelectKey> selectkeyRepo)
        {
            this.basicMathRepo = basicMathRepo;
            this.compilerRepo = compilerRepo;
            this.scriptRepo = scriptRepo;
            this.topicRepo = topicRepo;
            this.answerRepo = answerRepo;
            this.questionRepo = questionRepo;
            this.testRepo = testRepo;
            this.taskRepo = taskRepo;
            this.methodPRepo = methodPRepo;
            this.selectkeyRepo = selectkeyRepo;
            scriptRepo.Include("Test");
            testRepo.Include("Questions");
            testRepo.Include("Tasks");
        }
        public IEnumerable<ATask> GetTasks()
        {
            return taskRepo.GetAll().Select(t => new ATask
            {
                Id = t.ID,
                Name = t.Name,
                Discriminator = t.Discriminator,
                Type = t.Type
            });
        }
        public BaseMath_S GetBasicMath(int Id, string? type = null)
        {
            var task = basicMathRepo.Get(Id);
            var basemath = new BaseMath_S
            {
                Id = task.ID,
                Name = task.Name,
                Text = task.Text,
                Type = task.Type,
                Poly1 = task.Key_One,
                Poly2 = task.Key_Two,
                Discriminator = task.Discriminator,
                Module = task.Module,
                CodeHTML = (task.CodeHTML != null) ? task.CodeHTML : "NULL"
            };
            if (type == null)
            {
                return basemath;
            }
            if (Type.GetType("Services.Models." + type) == typeof(MultPoly))
            {
                return new MultPoly(basemath);
            }
            if (Type.GetType("Services.Models." + type) == typeof(DivPoly))
            {
                return new DivPoly(basemath);
            }
            if (Type.GetType("Services.Models." + type) == typeof(SumPoly))
            {
                return new SumPoly(basemath);
            }
            throw new Exception("Тип задачи не определен");
        }
        public IEnumerable<MethodProgramm_S> GetMethodPrograms()
        {
            return methodPRepo.GetAll().Select(t =>
            new MethodProgramm_S
            {
                Id = t.ID,
                Name = t.Name,
                Text = t.Text,
                Type = t.Type,
                ClassName = t.ClassName,
                MethodName = t.MethodName,
                Answer = t.Answer,
                CodeHTML = (t.CodeHTML != null) ? t.CodeHTML : "NULL",
                TestCases = t.TestCases
            });
        }
        public MethodProgramm_S GetMethodProgram(int id)
        {
            var task = methodPRepo.Get(id);
            return new MethodProgramm_S
            {
                Id = task.ID,
                Name = task.Name,
                Text = task.Text,
                Type = task.Type,
                ClassName = task.ClassName,
                MethodName = task.MethodName,
                Answer = task.Answer,
                Discriminator = task.Discriminator,
                CodeHTML = (task.CodeHTML != null) ? task.CodeHTML : "NULL",
                TestCases = task.TestCases
            };
        }
        public SelectKey_S GetSelectKey(int id)
        {
            var task = selectkeyRepo.Get(id);
            return new SelectKey_S
            {
                Id = task.ID,
                Name = task.Name,
                Text = task.Text,
                Type = task.Type,
                ClassName = task.ClassName,
                MethodName = task.MethodName,
                Discriminator= task.Discriminator,
                Answer = task.Answer,
                CodeHTML = (task.CodeHTML != null) ? task.CodeHTML : "NULL",
                EncryptMSG = task.EncryptMSG,
                Alphabet = task.Alphabet
            };
        }
        
        //----------------------------------------------------------------

        public Topic_S GetTopic(int id)
        {
            var topic = topicRepo.Get(id);
            return new Topic_S
            {
                Id = topic.ID,
                Name = topic.Name
            };
        }
        public IEnumerable<Topic_S> GetTopics()
        {
            return topicRepo.GetAll().Select(t => new Topic_S
            {
                Id = t.ID,
                Name = t.Name
            });
        }
        public Topic_S GetTopic(string topic)
        {
            var t = topicRepo.GetAll().Where(t => t.Name == topic).SingleOrDefault();
            return new Topic_S
            {
                Id = t.ID,
                Name = t.Name
            };
        }
        public IEnumerable<string> GetDiscriminators()
        {
            return taskRepo.GetAll().Select(t => t.Discriminator).Distinct();
        }
        public IEnumerable<string> GetTypeTask(string Discriminator)
        {
            return taskRepo.GetAll()
                .Where(t=>t.Discriminator == Discriminator)
                .Select(t => t.Type)
                .Distinct();
        }
        public IEnumerable<ATask> GetTasks(string Discriminator)
        {
            return GetTasks(taskRepo.GetAll()
                .Where(t => t.Discriminator == Discriminator)
                .Select(t=>t));
        }
        public IEnumerable<Answer_S> GetAnswers()
        {
            return answerRepo.GetAll().Select(a =>
            new Answer_S
            {
                Id = a.ID,
                Answer = a.Answer_One,
                Flag = a.Flag
            });
        }
        public IEnumerable<Answer_S> GetAnswers(int QuestionID, bool clearAnswer)
        {
            if (clearAnswer)
            {
                return answerRepo
                    .GetAll()
                    .Where(q => q.QuestionId == QuestionID)
                    .Select(a =>
                    new Answer_S
                    {
                        Id = a.ID,
                        Answer = a.Answer_One,
                        Flag = false
                    });
            }
            return answerRepo
                .GetAll()
                .Where(q=>q.QuestionId == QuestionID)
                .Select(a =>
                new Answer_S
                {
                    Id = a.ID,
                    Answer = a.Answer_One,
                    Flag = a.Flag
                });
        }
        private IEnumerable<Question_S> GetQuestions(IEnumerable<Question> questions, bool clearAnswer)
        {
            return questions.Select(q => new Question_S
            {
                Id = q.ID,
                Text = q.Text,
                Topic = GetTopic(q.TopicId).Name,
                Answers = GetAnswers(q.ID, clearAnswer).ToList()
            });
        }
        public IEnumerable<Question_S> GetQuestions(int Topic)
        {
            return GetQuestions(
                questionRepo
                .GetAll()
                .Where(q => q.TopicId == Topic), 
                false);
        }
        public IEnumerable<Question_S> GetQuestions()
        {
            return GetQuestions(questionRepo.GetAll(), false);
        }
        private IEnumerable<ATask> GetTasks(IEnumerable<Task> tasks)
        {
            return tasks.Select(t => new ATask
            {
                Id = t.ID,
                Name = t.Name,
                Type = t.Type,
                Text = t.Text,
                CodeHTML = t.CodeHTML,
                Discriminator = t.Discriminator
            });
        }
        public IEnumerable<Test_S> GetTests(bool clearAnswer)
        {
            return testRepo.GetAll().Select(t =>
            new Test_S
            {
                Id = t.ID,
                Name = t.Name,
                Questions = GetQuestions(t.Questions, clearAnswer).ToList(),
            });
        }
        public Test_S GetTest(int? Id, bool clearAnswer)
        {
            if(Id != null)
            {
                var test = testRepo.Get((int)Id);
                return new Test_S
                {
                    Id = test.ID,
                    Name = test.Name,
                    Questions = test.Questions != null ? GetQuestions(test.Questions, clearAnswer).ToList() : new(),
                    Tasks = test.Tasks != null ? GetTasks(test.Tasks).ToList() : new()
                };
            }
            return new();
        }
        public IEnumerable<Script_S> GetScripts()
        {

            return scriptRepo.GetAll().Select(s =>
            new Script_S
            {
                Id = s.ID,
                Name = s.Name,
                Information = s.Information,
                Topic = topicRepo.Get(s.TopicId).Name,
                Test = GetTest(s.Test?.ID, false)
            });
        }
        public Script_S GetScript(int id, bool clearAnswer)
        {
            var script = scriptRepo.Get(id);
            return new Script_S
            {
                Id = script.ID,
                Name = script.Name,
                Information = script.Information,
                Test = GetTest(script.Test.ID, clearAnswer),
                Topic = topicRepo.Get(script.TopicId).Name
            };
        }       



        public Question_S? UpdateQuestion(Question_S question)
        {
            try
            {
                if (questionRepo.GetAll()
                   .Where(q => q.Text == question.Text && q.ID != question.Id)
                   .Count() != 0)
                {
                    return null;
                }
                var quest = questionRepo
                        .Include("Answers")
                        .Single(q => q.ID == question.Id);
                quest.Text = question.Text;
                quest.TopicId = SaveTopic(question.Topic).Id;

                for (int i = 0; i < quest.Answers.Count; i++)
                {
                    if (question.Answers.SingleOrDefault(an =>
                     an.Id == quest.Answers[i].ID) == null)
                    {
                        quest.Answers.Remove(quest.Answers[i]);
                        i--;
                    }
                }
                foreach (var answer in question.Answers)
                {
                    if (answer.Id == 0)
                    {
                        answerRepo.Insert(new Answer
                        {
                            QuestionId = question.Id,
                            Answer_One = answer.Answer,
                            Flag = answer.Flag
                        });
                    }
                }
                answerRepo.Save();
                questionRepo.Save();
                return question;
            }
            catch
            {
                return null;
            }
        }
        public Question_S? SaveQuestion(Question_S question)
        {
            try
            {
                if (questionRepo.GetAll()
                    .Where(q => q.Text == question.Text)
                    .Count() != 0)
                {
                    return null;
                }

                Question quest = new Question
                {
                    Text = question.Text,
                    TopicId = SaveTopic(question.Topic).Id
                };

                questionRepo.Insert(quest);
                questionRepo.Save();
                question.Id = quest.ID;
                if (UpdateQuestion(question) == null)
                {
                    questionRepo.Delete(quest);
                    return null;
                }
                return question;
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteQuestion(Question_S question) 
        {
            try
            {
                questionRepo?.Delete(questionRepo.Get(question.Id));
                questionRepo?.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Topic_S SaveTopic(string topic)
        {
            var topic_new = topicRepo.GetAll().Where(t =>
            t.Name.Replace(" ", "").ToLower() ==
            topic.Replace(" ", "").ToLower()).SingleOrDefault();
            if (topic_new != null)
            {
                return new Topic_S
                {
                    Id = topic_new.ID,
                    Name = topic_new.Name
                };
            }
            topic_new = new Topic
            {
                Name = topic
            };
            topicRepo.Insert(topic_new);
            topicRepo.Save();
            return new Topic_S
            {
                Id = topic_new.ID,
                Name = topic_new.Name
            };
        }

        public bool UpdateScript(Script_S script)
        {
            try
            {
                testRepo.Include("Questions");
                var test = testRepo.Include("Tasks").Single(t => t.ID == script.Test.Id);
                if (scriptRepo
                .GetAll()
                .Where(s => s.Name == script.Name && s.ID != script.Id)
                .Count() != 0)
                {
                    return false;
                }
                var questions = script.Test.Questions.Select(q => q.Id);
                var tasks = script.Test.Tasks.Select(t => t.Id);

                var Script = scriptRepo.Get(script.Id);
                Script.Name = script.Name;
                Script.Information = script.Information;
                Script.TopicId = SaveTopic(script.Topic).Id;

                test.Questions.RemoveRange(0, test.Questions.Count);
                test.Tasks.RemoveRange(0, test.Tasks.Count);
                testRepo.Save();

                test.Name = script.Test.Name;
                test.Questions = questionRepo
                    .GetAll()
                    .Where(q => questions.Contains(q.ID))
                    .Select(q => q)
                    .ToList();
                test.Tasks = taskRepo
                    .GetAll()
                    .Where(t => tasks.Contains(t.ID))
                    .Select(t => t)
                    .ToList();
                Script.Test = test;

                scriptRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveScript(Script_S script_in)
        {
            try
            {
                if (scriptRepo.GetAll().Where(s => s.Name == script_in.Name).Count() != 0)
                {
                    return false;
                }

                var questions = script_in.Test.Questions.Select(q => q.Id);
                var tasks = script_in.Test.Tasks.Select(t => t.Id);

                Script script = new()
                {
                    Name = script_in.Name,
                    Information = script_in.Information,
                    TopicId = SaveTopic(script_in.Topic).Id
                };

                scriptRepo.Insert(script);
                scriptRepo.Save();

                Test test = new()
                {
                    Name = script_in.Test.Name,
                    ScriptId = script.ID,
                    Questions = questionRepo
                        .Include("Answers")
                        .Where(q => questions.Contains(q.ID))
                        .Select(q => q)
                        .ToList(),
                    Tasks = taskRepo
                        .GetAll()
                        .Where(t => tasks.Contains(t.ID))
                        .Select(t => t)
                        .ToList()
                };
                script.Test = test;
                scriptRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteScripts(IEnumerable<Script_S> scripts)
        {
            try
            {
                foreach (var script in scripts)
                {
                    scriptRepo.Delete(scriptRepo.Get(script.Id));
                }
                scriptRepo.Save();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }
    }
}