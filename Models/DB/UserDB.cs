using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Discriminator { get; set; } = null!;
    }
    public class Group : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public int TutorId { get; set; }
        public List<Researcher> Researchers { get; set; } = null!;
    }
    public class Tutor : User
    {
        public List<Group> Groups { get; set; } = null!;
    }
    public class Researcher : User
    {
        public int? GroupId { get; set; }
        public List<ScriptUser>? Scripts { get; set; }
    }
    public class ScriptUser: BaseEntity
    {
        public int ScriptId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Topic { get; set; } = null!;
        public int ScriptScore { get; set; }
        public TestUser? Test { get; set; }

        public int ResearcherId { get; set; }

        [Required]
        public Researcher Researcher { get; set; } = null!;
    }
    public class TestUser : BaseEntity
    {
        public int TestNum { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public int QuestionsScore { get; set; }
        public int TasksScore { get; set; }
        public List<QuestionTestUser> Questions { get; set; } = null!;
        public List<TaskUser> Tasks { get; set; } = null!;

        public int ScriptUserId { get; set; }

        [Required]
        public ScriptUser ScriptUser { get; set; } = null!;
    }
    public class QuestionTestUser : BaseEntity
    {
        public int QuestionNum { get; set; }

        [Required]
        public string Text { get; set; } = null!;
        public int QuestionScore { get; set; }

        public int TestUserId { get; set; }

        [Required]
        public TestUser TestUser { get; set; } = null!;
    }
    public class TaskUser : BaseEntity
    {
        public int TaskNum { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public int TaskScore { get; set; }

        public int TestUserId { get; set; }
        public TestUser? TestUser { get; set; }
    }
}