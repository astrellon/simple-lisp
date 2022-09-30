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
            // var stdEnv = new StdLispEnvironment();
            // var env = new LispEnvironment(stdEnv);

            var sw = Stopwatch.StartNew();
            Lisp.Evaluate(File.ReadAllText("perfTest.lisp"), env);
            sw.Stop();
            Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        }
        #endregion

        private static object RandProc = new BuiltinProcedureValue((i, e) =>
        {
            return Rand.NextDouble();
        });

        private static object AddProc = new BuiltinProcedureValue((i, e) =>
        {
            var num1 = (double)i[0];
            var num2 = (double)i[1];
            return num1 + num2;
        });

        private static object AddTotalProc = new BuiltinProcedureValue((i, e) =>
        {
            Total += (double)i[0];
            return null;
        });

        private static object IsNotDoneProc = new BuiltinProcedureValue((i, e) =>
        {
            Counter++;
            return Counter < 1_000_000;
        });

        private static object DoneProc = new BuiltinProcedureValue((i, e) =>
        {
            Console.WriteLine($"Done: {Total}");
            return null;
        });
    }
}