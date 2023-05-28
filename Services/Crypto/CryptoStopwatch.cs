using System;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using Cypher.Cypher_Algorithms;

namespace Services.Crypto
{
    public class CryptoStopwatch
    {
        private List<int> data;
        private List<int> key;
        private int initVec;
        private Random random = null!;
        private readonly int startRandVec;

        public int Cycle_count { get; set; }
        public int Data_size { get; set; }
        public int Key_size { get; set; }

        public CryptoStopwatch()
        {
            data = new();
            key = new();
            startRandVec = DateTime.Now.Second;
        }
        private void FormingStartData(double count)
        {
            key.Clear();
            data.Clear();
            while (data.Count < count)
            {
                data.Add(random.Next(0, 255));
            }
            while (key.Count < Key_size)
            {
                key.Add(random.Next(0, 255));
            }
            initVec = random.Next(0, 1000);
        }

        public void Registration(BaseAlg baseAlg, bool assymetry = false, bool openKey = true)
        {
            random = new(startRandVec);
            FormingStartData(1);
            if (baseAlg == null)
            {
                throw new Exception("Сборка не найдена");
            }
            if (!assymetry)
            {
                baseAlg.Registration(data, key, initVec);
            }
            else if (assymetry)
            {
                baseAlg.Registration(data, openKey, initVec);
            }
        }
        public IEnumerable<Point> Run(Action action)
        {
            List<Point> result = new();
            try
            {
                var stopwatch = new Stopwatch();
                result = new();
                double step = Data_size / Cycle_count;
                for (int i = 0; i <= Cycle_count; i++)
                {
                    stopwatch.Start();
                    action.Invoke();
                    stopwatch.Stop();
                    result.Add(new(data.Count, stopwatch.ElapsedMilliseconds));
                    if (data.Count + step <= Data_size)
                    {
                        FormingStartData(data.Count + step);
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}