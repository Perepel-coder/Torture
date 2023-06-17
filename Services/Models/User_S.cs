using System.Collections.Generic;

namespace Services.Models
{
    public enum USER_ROLE { Researcher, Tutor };
    public class User_S
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Discriminator { get; set; } = null!;
    }
    public class Tutor_S: User_S
    {
        public List<Group_S> Groups { get; set; } = null!;
    }
    public class Group_S
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TutorId { get; set; }
        public List<Researcher_S> Researchers { get; set; } = null!;
    }
    public class Researcher_S: User_S
    {
        public int? GroupId { get; set; }
        public List<ScriptUser_S>? Scripts { get; set; }
    }
    public class ScriptUser_S
    {
        public int ScriptId { get; set; }
        public string Name { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public int Score { get; set; }
        public TestUser_S Test { get; set; } = null!;
    }

    public class TestUser_S
    {
        public int TestNum { get; set; }
        public string Name { get; set; } = null!;
        public int QuestionsScore { get; set; }
        public int TasksScore { get; set; }
        public List<QuestionTestUser_S> Questions { get; set; } = null!;
        public List<TaskUser_S> Tasks { get; set; } = null!;
    }

    public class TaskUser_S
    {
        public TaskUser_S() { }
        public TaskUser_S(ATask task, int score)
        {
            TaskNum = task.Id;
            Name = task.Name;
            Score = score;
        }
        public int TaskNum { get; set; }
        public string Name { get; set; } = null!;
        public int Score { get; set; }
    }

    public class QuestionTestUser_S
    {
        public int QuestionNum { get; set; }
        public string Text { get; set; } = null!;
        public int Score { get; set; }
    }
}