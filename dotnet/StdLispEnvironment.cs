using System;
using System.Linq;

namespace DotnetLisp
{
    public class StdLispEnvironment : ILispEnvironment
    {
        #region Fields
        private static readonly Random RandomGen = new Random();

        private static readonly object Print = new BuiltinProcedureValue((input, env) =>
        {
            Console.WriteLine(string.Join("", input.Select(v => v.ToString())));
            return input[0];
        });

        private static readonly object Rand = new BuiltinProcedureValue((input, env) =>
        {
            return RandomGen.NextDouble();
        });

        private static readonly object Add = new BuiltinProcedureValue((input, env) =>
        {
            var result = (double)(input.First());
            for (var i = 1; i < input.Count; i++)
            {
                result += (double)input[i];
            }
            return result;
        });

        private static readonly object Sub = new BuiltinProcedureValue((input, env) =>
        {
            var result = (double)input.First();
            for (var i = 1; i < input.Count; i++)
            {
                result -= (double)input[i];
            }
            return result;
        });

        private static readonly object GreaterThan = new BuiltinProcedureValue((input, env) =>
        {
            var left = input[0];
            var right = input[1];
            if (left is IComparable leftComp)
            {
                return leftComp.CompareTo(right) > 0;
            }
            return 1;
        });

        private static readonly object LessThan = new BuiltinProcedureValue((input, env) =>
        {
            var left = input[0];
            var right = input[1];
            if (left is IComparable leftComp)
            {
                return leftComp.CompareTo(right) < 0;
            }
            return 1;
        });

        private static readonly object ValueEquals = new BuiltinProcedureValue((input, env) =>
        {
            var left = input[0];
            var right = input[1];
            if (left is IComparable leftComp)
            {
                return leftComp.CompareTo(right) == 0;
            }
            return 1;
        });
        #endregion

        #region Methods
        public void Set(string key, object value)
        {
            throw new Exception("READONLY");
        }

        public object Get(string key)
        {
            switch (key)
            {
                case "print": return Print;
                case "rand": return Rand;

                case "+":
                case "add": return Add;

                case "-":
                case "sub": return Print;

                case ">": return GreaterThan;
                case "<": return LessThan;
                case "=": return ValueEquals;
            }

            return null;
        }

        #endregion
    }
}