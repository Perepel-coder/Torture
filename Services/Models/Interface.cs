using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class ATask
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Discriminator { get; set; } = null!;
        public string? CodeHTML { get; set; }
    }
    public interface IBaseMath
    {
        public int? Poly1 { get; set; }
        public int? Poly2 { get; set; }
        public int? Module { get; set; }
        public int Answer();
    }
    public interface ICompiler_S
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Answer { get; set; }
    }
}
