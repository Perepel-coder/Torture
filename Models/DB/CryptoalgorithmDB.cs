using System.Collections.Generic;

namespace Models
{
    public class Cryptoalgorithm: BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Class { get; set; }

        public int NumOfKeyId { get; set; }
        public NumOfKey Classification { get; set; } = null!;
    }
    public class NumOfKey: BaseEntity
    {
        public string TypeKey { get; set; } = null!;
        public string TypeProcess { get; set; } = null!;
        public string? SubtypeProcess { get; set; }
        public string? SpecificProcess { get; set; }

        public List<Mode>? Mode { get; set; }
        public List<Cryptoalgorithm>? Cryptoalgorithms { get; set; }
    }
    public class Mode: BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Class { get; set; }

        public List<NumOfKey>? NumOfKeys { get; set; }
    }
}
