using Cypher.Cypher_Algorithms;
using Cypher.Cypher_Method;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using trans = Services.Crypto.TransformData;

namespace Services.Crypto
{
    public class BlockEncryption
    {
        private BaseMethod method = null!;

        public void Registration(CypherTools tools, IEnumerable<int> data)
        {
            string solution = "Cypher";
            string className = "Cypher.Cypher_Algorithms." + tools.Alg.Class;
            object[] args = new object[1] { tools.Dchar };

            var myalg = Tools.CreateInstance<BaseAlg>(solution, className, args);
            myalg.Registration(data, trans.ToInt(tools.Key), tools.Vec);

            className = "Cypher.Cypher_Method." + tools.Mode.Class;
            args[0] = myalg;

            method = Tools.CreateInstance<BaseMethod>(solution, className, args);
        }
        public IEnumerable<byte> Encode()
        {
            return method.Encode().Select(el=>(byte)el);
        }

        public IEnumerable<byte> Decode()
        {
            return method.Decode().Select(el => (byte)el);
        }
    }
}
