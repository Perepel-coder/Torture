using ReactiveUI;

namespace Services.Models
{
    public class CypherTools : ReactiveObject
    {
        private CryptoAlg_S? alg;
        private Mode_S? mode;
        private char dchar;
        private string key;
        private int vec;
        private string way;
        public char Dchar
        {
            get => dchar;
            set => this.RaiseAndSetIfChanged(ref dchar, value);
        }
        public string Key
        {
            get => key;
            set => this.RaiseAndSetIfChanged(ref key, value);
        }
        public int Vec
        {
            get => vec;
            set => this.RaiseAndSetIfChanged(ref vec, value);
        }
        public string Way
        {
            get => way;
            set => this.RaiseAndSetIfChanged(ref way, value);
        }
        public CryptoAlg_S? Alg
        {
            get => alg;
            set => this.RaiseAndSetIfChanged(ref alg, value);
        }
        public Mode_S? Mode
        {
            get => mode;
            set => this.RaiseAndSetIfChanged(ref mode, value);
        }
        public CypherTools()
        {
            key = string.Empty;
            way = string.Empty;
            alg = new();
            mode = new();
        }
    }
}
