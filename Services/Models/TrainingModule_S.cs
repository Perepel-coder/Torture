using ReactiveUI;
using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class BaseMath_S : ATask, IBaseMath
    {
        public BaseMath_S() { }
        public BaseMath_S(ATask task)
        {
            Id = task.Id;
            Name = task.Name;
            Text = task.Text;
            Type = task.Type;
            Discriminator = task.Discriminator;
            CodeHTML = task.CodeHTML;
        }
        public int? Poly1 { get; set; }
        public int? Poly2 { get; set; }
        public int? Module { get; set; }

        public virtual int Answer()
        {
            throw new Exception("Функция не определена");
        }
    }
    public class MethodProgramm_S : ATask, ICompiler_S
    {
        public MethodProgramm_S() { }
        public MethodProgramm_S(ATask task)
        {
            Id = task.Id;
            Name = task.Name;
            Text = task.Text;
            Type = task.Type;
            Discriminator = task.Discriminator;
            CodeHTML = task.CodeHTML;
        }
        public string ClassName { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string TestCases { get; set; } = null!;
    }
    public class SelectKey_S : ATask, ICompiler_S
    {
        public SelectKey_S() { }
        public SelectKey_S(ATask task)
        {
            Id = task.Id;
            Name = task.Name;
            Text = task.Text;
            Type = task.Type;
            Discriminator = task.Discriminator;
            CodeHTML = task.CodeHTML;
        }
        public string ClassName { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string Alphabet { get; set; } = null!;
        public string EncryptMSG { get; set; } = null!;
    }

    #region BaseMath
    public class MultPoly: BaseMath_S
    {
        public MultPoly(BaseMath_S @base)
        {
            Id = @base.Id;
            Name = @base.Name;
            Text = @base.Text;
            Type = @base.Type;
            Poly1 = @base.Poly1;
            Poly2 = @base.Poly2;
            Module = @base.Module;
            CodeHTML = (@base.CodeHTML != null) ? @base.CodeHTML : "NULL";
        }
        public static  int Answer(int? poly1 = null, int? poly2 = null, int? module = null)
        {
            if (poly1 != null && poly2 != null && module != null)
            {
                return Mathematics.MathematicsGF.MultOfPolyGF((int)poly1, (int)poly2, (int)module);
            }
            throw new Exception("Один или несколько аргументов не определен.");
        }
        public override int Answer()
        {
            if (Poly1 != null && Poly2 != null && Module != null)
            {
                return Mathematics.MathematicsGF.MultOfPolyGF((int)Poly1, (int)Poly2, (int)Module);
            }
            throw new Exception("Один или несколько аргументов не определен.");
        }
    }
    public class DivPoly : BaseMath_S
    {
        public DivPoly(BaseMath_S @base)
        {
            Id = @base.Id;
            Name = @base.Name;
            Text = @base.Text;
            Type = @base.Type;
            Poly1 = @base.Poly1;
            Poly2 = @base.Poly2;
            Module = @base.Module;
            CodeHTML = (@base.CodeHTML != null) ? @base.CodeHTML : "NULL";
        }
        public static int Answer(int? poly1 = null, int? poly2 = null, int? module = null)
        {
            if (poly1 != null && poly2 != null)
            {
                return Mathematics.MathematicsGF.DivOfPolyGF((int)poly1, (int)poly2)[0];
            }
            throw new Exception("Один или несколько аргументов не определен.");
        }
        public override int Answer()
        {
            if (Poly1 != null && Poly2 != null)
            {
                return Mathematics.MathematicsGF.DivOfPolyGF((int)Poly1, (int)Poly2)[0];
            }
            throw new Exception("Один или несколько аргументов не определен.");
        }
    }
    public class SumPoly : BaseMath_S
    {
        public SumPoly(BaseMath_S @base)
        {
            Id = @base.Id;
            Name = @base.Name;
            Text = @base.Text;
            Type = @base.Type;
            Poly1 = @base.Poly1;
            Poly2 = @base.Poly2;
            Module = @base.Module;
            CodeHTML = (@base.CodeHTML != null) ? @base.CodeHTML : "NULL";
        }
        public static int Answer(int? poly1 = null, int? poly2 = null, int? module = null)
        {
            if (poly1 != null && poly2 != null)
            {
                return Mathematics.MathematicsGF.XorPolyGF((int)poly1, (int)poly2);
            }
            throw new Exception("Один или несколько аргументов не определен.");
        }
        public override int Answer()
        {
            if (Poly1 != null && Poly2 != null)
            {
                return Mathematics.MathematicsGF.XorPolyGF((int)Poly1, (int)Poly2);
            }
            throw new Exception("Один или несколько аргументов не определен.");
        }
    }
    #endregion

    public class Topic_S
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class Answer_S : ReactiveObject
    {
        public int Id { get; set; }
        public string Answer { get; set; } = null!;
        private bool flag = false;
        public bool Flag
        {
            get => flag;
            set => this.RaiseAndSetIfChanged(ref flag, value);
        }
    }
    public class Question_S : ReactiveObject
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public List<Answer_S> Answers { get; set; } = null!;
    }
    public class Test_S
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Question_S> Questions { get; set; } = null!;
        public List<ATask> Tasks { get; set; } = null!;
    }
    public class Script_S : AScript
    {
        public Test_S Test { get; set; } = null!;
    }
}