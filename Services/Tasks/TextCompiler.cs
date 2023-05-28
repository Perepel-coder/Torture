using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace Services.Tasks
{
    public class CompilerOut
    {
        public object Instance { get; private set; }
        public MethodInfo Metod { get; private set; }
        public CompilerOut(object Instance, MethodInfo Metod)
        {
            this.Instance = Instance;
            this.Metod = Metod;
        }
    }
    public class TextCompiler
    {
        private static readonly MetadataReference[] references = new[]
        {
                typeof(object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(
                    Path.GetDirectoryName(typeof(GCSettings).GetTypeInfo().Assembly.Location),
                    "System.Runtime.dll")
        }
        .Select(r => MetadataReference.CreateFromFile(r)).ToArray();

        public static CompilerOut Compiler(string programTXT, string className = "Class1", string method = "Write")
        {
            CSharpCompilation compilation = CSharpCompilation.Create(
               Path.GetRandomFileName(),
               syntaxTrees: new[] { CSharpSyntaxTree.ParseText(programTXT) },
               references: references,
               options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);
                    string msg = string.Empty;
                    foreach (Diagnostic diagnostic in failures)
                    {
                        msg += $"\n{diagnostic.Id}: {diagnostic.GetMessage()}";
                    }
                    throw new Exception(msg);
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    Type? type = assembly.GetType(className);
                    if (type != null)
                    {
                        var instance = Activator.CreateInstance(type);
                        var meth = type.GetMethods().SingleOrDefault(m => m.Name == method);
                        if (instance != null && meth != null)
                        {
                            return new CompilerOut(instance, meth);
                        }
                        throw new Exception($"Не удалось найти текущий метод {meth}");
                    }
                    else
                    {
                        throw new Exception($"Не удалось найти текущий тип {className}");
                    }
                }
            }
        }
    }

    public class Tester
    {
        
        public static void GetTest<TParam, TRes>(
            IEnumerable<TParam[]> testCases,
            IEnumerable<TRes[]> answerCases, 
            CompilerOut compiler)
        {
            Type retunType = compiler.Metod.ReturnType;
            var tests = testCases.ToList();
            var answers = answerCases.ToList();
            for(int i = 0; i < tests.Count; i++)
            {
                bool flag = false;
                var result = InvokeFun<TParam, TRes>(tests[i], compiler);
                if (retunType.IsArray)
                {
                    Type? elementType = retunType.GetElementType();
                    var answer = answers[i]
                        .Select(elm =>
                        {
                            return elementType.IsArray ?
                            CastArrays(elm as Array, elementType.GetElementType()) :
                            Convert.ChangeType(elm, elementType);
                        })
                        .ToArray();
                    if (!elementType.IsArray)
                    {
                        var res = (result as Array)?.Cast<object>().Select(s => s).ToArray();

                        for (int j = 0; j < answer.Length; j++)
                        {
                            if (!Tools.CompareEquals(res[j], answer[j], retunType.GetElementType()))
                            {
                                break;
                            }
                        }
                        flag = true;
                    }
                }
                else
                {
                    var answer = Convert.ChangeType(answers[i][0], retunType);
                    flag = Tools.CompareEquals(result, answer, retunType);
                }

                if (!flag)
                {
                    throw new Exception(
                        $"Ошибка теста № {i+1}" +
                        $"Тест: {string.Join(" ", tests[i][0..(tests[i].Length-1)])}");
                }
            }
        }
        
        public static IEnumerable<T> CastArrays<T>(Array array)
        {
            List<T> newArray = new();
            foreach (var el in array)
            {
                newArray.Add((T)el);
            }
            return newArray;
        }
        public static Array CastArrays(Array array, Type type)
        {
            var newArray = Array.CreateInstance(type, array.Length);
            foreach(var el in array)
            {
                newArray.SetValue(el, 
                    Array.IndexOf(
                        array, 
                        Convert.ChangeType(el, type)
                        )
                    );
            }
            return newArray;
        }
        public static TRes InvokeFun<TParam, TRes>(TParam[] parameters, CompilerOut compiler)
        {
            Type[] paramTypes = compiler.Metod
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();
            Type retunType = compiler.Metod.ReturnType;

            object?[] param = parameters
                .Zip(paramTypes, (test, type) =>
                {
                    return type.IsArray ?
                    CastArrays(test as Array, type.GetElementType()) :
                    Convert.ChangeType(test, type);
                })
                .ToArray();
            return (TRes)compiler.Metod.Invoke(compiler.Instance, param);
        }
        public static bool GetComparison<T>(T obj1, T obj2)
        {
            if (obj1 != null && obj2 != null)
            {
                return Tools.CompareEquals(obj1, obj2, typeof(T));
            }
            else
            {
                return false;
            }
        }
    }
}
