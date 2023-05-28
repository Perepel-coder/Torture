using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Services.Tasks
{
    public static class CreatTask_XML
    {
        private static List<int> ModulePoly = new() 
        { 7, 11, 19, 55, 61, 37, 103, 67, 157, 143, 137, 355, 283, 285, 487 };
        public static bool Serializer_XML<T>(string fileName, IEnumerable<T[]> tests)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<T[]>));
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, tests);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static IEnumerable<T[]>? ReadTestCases_XML<T>(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<T[]>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
               return formatter.Deserialize(fs) as List<T[]>;
            }
        }
        public static bool MethodProgrammTestCases(
            string Type, int count, string file_TC, string file_A)
        {
            Random rand = new();
            List<int[]> tests = new();
            List<int[]> answers = new();
            Action<List<int[]>, List<int[]>> action = null!;
            if(Type == "MultPoly")
            {
                action = (result, answers) =>
                {
                    int module, poly1, poly2;
                    module = ModulePoly[rand.Next(0, ModulePoly.Count)];
                    if (module < 255)
                    {
                        poly1 = rand.Next(0, module);
                        poly2 = rand.Next(0, module);
                    }
                    else
                    {
                        poly1 = rand.Next(0, 255);
                        poly2 = rand.Next(0, 255);
                    }
                    int answer = Mathematics.MathematicsGF.MultOfPolyGF(poly1, poly2, module);
                    result.Add(new int[3] { poly1, poly2, module });
                    answers.Add(new int[1] { answer });
                };
            }
            else if (Type == "DivPoly" || Type == "SumPoly")
            {
                action = (tests, answers) =>
                {
                    int poly1 = rand.Next(0, 255);
                    int poly2 = rand.Next(0, 255);
                    tests.Add(new int[2] { poly1, poly2 });
                    if (Type == "DivPoly")
                    {
                        int[] answer = Mathematics.MathematicsGF.DivOfPolyGF(poly1, poly2);
                        answers.Add(answer);
                    }
                    else
                    {
                        int answer = Mathematics.MathematicsGF.XorPolyGF(poly1, poly2);
                        answers.Add(new int[1] { answer });
                    }
                };
            }
            else
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                action.Invoke(tests, answers);
            }
            return
                Serializer_XML(file_TC, tests) &
                Serializer_XML(file_A, answers);
        }
    }
}