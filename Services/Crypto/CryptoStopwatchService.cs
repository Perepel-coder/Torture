using Cypher.Cypher_Algorithms;
using Cypher.Cypher_Method;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Services.Crypto
{
    public class CryptoStopwatchService
    {
        private readonly CryptoStopwatch stopwatch;
        private List<BaseAlg> cryptoAlgs = null!;
        private string mode = null!;
        private string orientation = null!;

        public CryptoStopwatchService(CryptoStopwatch stopwatch)
        {
            this.stopwatch = stopwatch;
        }
        public void Registration(
            IEnumerable<CryptoAlg_S> algs, 
            Mode_S mode,
            string orientation, 
            int dataSize, 
            int keySize, 
            int cycleCount)
        {
            cryptoAlgs = algs
                .Where(a => a.Flag)
                .Select(a => Tools.CreateInstance<BaseAlg>("Cypher", "Cypher.Cypher_Algorithms."+a.Class, new object[1] {'~'}))
                .ToList();
            this.orientation = orientation;
            this.mode = mode.Class != null ? 
                mode.Class : 
                throw new Exception("Режим шифрования не определен");
            stopwatch.Data_size = dataSize;
            stopwatch.Key_size = keySize;
            stopwatch.Cycle_count = cycleCount;
        }
        public IEnumerable<(IEnumerable<Point>, string)> GetResult()
        {
            List<(IEnumerable<Point>, string)> points = new();
            foreach(var alg in cryptoAlgs)
            {
                BaseMethod method = Tools.CreateInstance<BaseMethod>("Cypher", "Cypher.Cypher_Method." + mode, new object[1] { alg });
                if (alg.type == "symmetry")
                {
                    stopwatch.Registration(alg);
                }
                else if(alg.type == "asymmetry")
                {
                    if(orientation == "Зашифровать")
                    {
                        stopwatch.Registration(alg, true);

                    }
                    if(orientation == "Расшифровать")
                    {
                        stopwatch.Registration(alg, true, false);
                    }
                }
                if (orientation == "Зашифровать")
                {
                    points.Add((stopwatch.Run(() => method.Encode()), alg.name));
                }
                if (orientation == "Расшифровать")
                {
                    points.Add((stopwatch.Run(() => method.Decode()), alg.name));
                }
            }
            return points;
        }
    }
}
