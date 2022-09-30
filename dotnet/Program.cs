using System;
using System.IO;
using System.Diagnostics;

namespace DotnetLisp
{
    public static class Program
    {
        private static Random Rand = new Random();
        private static int Counter = 0;
        private static double Total = 0.0;

        #region Methods
        public static void Main(string[] args)
        {
            var env = new LispEnvironment();
            env.Set("isNotDone", IsNotDoneProc);
            env.Set("addTotal", AddTotalProc);
            env.Set("add", AddProc);
            env.Set("rand", RandProc);
            env.Set("done", DoneProc);

            var sw = Stopwatch.StartNew();
            Lisp.Evaluate(File.ReadAllText("perfTest.lisp"), env);
            sw.Stop();
            Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        }
        #endregion

        private static IValue RandProc = new BuiltinProcedureValue((i, e) =>
        {
            return new NumberValue(Rand.NextDouble());
        });

        private static IValue AddProc = new BuiltinProcedureValue((i, e) =>
        {
            var num1 = (NumberValue)i.Value[0];
            var num2 = (NumberValue)i.Value[1];
            return new NumberValue(num1.Value + num2.Value);
        });

        private static IValue AddTotalProc = new BuiltinProcedureValue((i, e) =>
        {
            Total += ((NumberValue)i.Value[0]).Value;
            return NullValue.Value;
        });

        private static IValue IsNotDoneProc = new BuiltinProcedureValue((i, e) =>
        {
            Counter++;
            return new BoolValue(Counter < 1_000_000);
        });

        private static IValue DoneProc = new BuiltinProcedureValue((i, e) =>
        {
            Console.WriteLine($"Done: {Total}");
            return NullValue.Value;
        });
    }
}