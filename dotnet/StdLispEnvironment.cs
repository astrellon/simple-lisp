using System;
using System.Linq;

namespace DotnetLisp
{
    public class StdLispEnvironment : ILispEnvironment
    {
        #region Fields
        private static readonly Random RandomGen = new Random();

        private static readonly IValue Print = new BuiltinProcedureValue((input, env) =>
        {
            Console.WriteLine(string.Join("", input.Value.Select(v => v.ToString())));
            return input.Value[0];
        });

        private static readonly IValue Rand = new BuiltinProcedureValue((input, env) =>
        {
            return new NumberValue(RandomGen.NextDouble());
        });

        private static readonly IValue Add = new BuiltinProcedureValue((input, env) =>
        {
            var result = ((NumberValue)input.Value.First()).Value;
            for (var i = 1; i < input.Value.Count; i++)
            {
                result += ((NumberValue)input.Value[i]).Value;
            }
            return new NumberValue(result);
        });

        private static readonly IValue Sub = new BuiltinProcedureValue((input, env) =>
        {
            var result = ((NumberValue)input.Value.First()).Value;
            for (var i = 1; i < input.Value.Count; i++)
            {
                result -= ((NumberValue)input.Value[i]).Value;
            }
            return new NumberValue(result);
        });

        private static readonly IValue GreaterThan = new BuiltinProcedureValue((input, env) =>
        {
            var left = input.Value[0];
            var right = input.Value[1];
            return new BoolValue(left.CompareTo(right) > 0);
        });

        private static readonly IValue LessThan = new BuiltinProcedureValue((input, env) =>
        {
            var left = input.Value[0];
            var right = input.Value[1];
            return new BoolValue(left.CompareTo(right) < 0);
        });

        private static readonly IValue ValueEquals = new BuiltinProcedureValue((input, env) =>
        {
            var left = input.Value[0];
            var right = input.Value[1];
            return new BoolValue(left.CompareTo(right) == 0);
        });
        #endregion

        #region Methods
        public void Set(string key, IValue value)
        {
            throw new Exception("READONLY");
        }

        public IValue Get(string key)
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

            return NullValue.Value;
        }

        #endregion
    }
}