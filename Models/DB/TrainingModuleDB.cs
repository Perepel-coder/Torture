using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Script: BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Information { get; set; } = null!;

        public int TopicId { get; set; }
        public Test Test { get; set; } = null!;
    }

    public class Test: BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public int ScriptId { get; set; }
        public List<Question> Questions { get; set; } = null!;
        public List<Task> Tasks { get; set; } = null!;
    }
    public class Question: BaseEntity
    {
        [Required]
        public string Text { get; set; } = null!;
        public int TopicId { get; set; }
        public List<Answer> Answers { get; set; } = null!;
        public List<Test> Tests { get; set; } = null!;
    }
    public class Answer : BaseEntity
    {
        [Required]
        public string Answer_One { get; set; } = null!;
        public bool Flag { get; set; }
        public int QuestionId { get; set; }
    }
    public class Topic : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public List<Script>? Scripts { get; set; }
        public List<Question>? Questions { get; set; }
    }

    public class Task : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public string Text { get; set; } = null!;
        public string? CodeHTML { get; set; }
        [Required]
        public string Discriminator { get; set; } = null!;
        public List<Test> Tests { get; set; } = null!;
    }
    public class BaseMath : Task
    {
        public int? Key_One { get; set; }
        public int? Key_Two { get; set; }
        public int? Module { get; set; }
    }
    public class Compiler : Task
    {
        [Required]
        public string ClassName { get; set; } = null!;
        [Required]
        public string MethodName { get; set; } = null!;
        [Required]
        public string Answer { get; set; } = null!;
    }

    public class MethodProgramm: Compiler
    {
        [Required]
        public string TestCases { get; set; } = null!;
    }

    public class SelectKey: Compiler
    {
        [Required]
        public string Alphabet { get; set; } = null!;
        [Required]
        public string EncryptMSG { get; set; } = null!;
    }
}