using System;
using System.Linq;

namespace DotnetLisp
{
    public static class Lisp
    {
        #region Fields
        #endregion

        #region Methods
        public static IValue Evaluate(string input, LispEnvironment env)
        {
            var parsed = LispParser.ReadFromTokens(LispParser.Tokenize(input));
            return Evaluate(parsed, env);
        }

        public static IValue Evaluate(IValue input, LispEnvironment env)
        {
            if (input is SymbolValue symbolValue)
            {
                return env.Get(symbolValue.Value);
            }
            else if (input is ArrayValue arrayValue)
            {
                var first = arrayValue.Value[0];
                if (first is SymbolValue firstSymbol)
                {
                    switch (firstSymbol.Value)
                    {
                        case "lambda":
                            {
                                var lambdaArgs = ((ArrayValue)arrayValue.Value[1]).Value.Select(v => (SymbolValue)v).ToList();
                                var body = arrayValue.Value[2];
                                return new LambdaProcedureValue(lambdaArgs, body);
                            }
                        case "define":
                            {
                                env.Set(arrayValue.Value[1].ToString(), Lisp.Evaluate(arrayValue.Value[2], env));
                                return input;
                            }
                        case "when":
                            {
                                var test = arrayValue.Value[1];
                                var body = arrayValue.Value[2];
                                if (Evaluate(test, env).Equals(BoolValue.True))
                                {
                                    return Evaluate(body, env);
                                }
                                return NullValue.Value;
                            }
                        case "if":
                            {
                                var test = arrayValue.Value[1];
                                var ifTrue = arrayValue.Value[2];
                                var ifFalse = arrayValue.Value[3];
                                var compResult = Evaluate(test, env).Equals(BoolValue.True);

                                return Evaluate(compResult ? ifTrue : ifFalse, env);
                            }
                        case "loop":
                            {
                                var test = arrayValue.Value[1];
                                var body = arrayValue.Value[2];

                                IValue result = NullValue.Value;
                                while (Evaluate(test, env).Equals(BoolValue.True))
                                {
                                    result = Evaluate(body, env);
                                }
                                return result;
                            }
                        case "begin":
                            {
                                IValue result = NullValue.Value;
                                foreach (var item in arrayValue.Value)
                                {
                                    result = Evaluate(item, env);
                                }
                                return result;
                            }
                    }
                }

                var value = Evaluate(first, env);
                if (value is IProcedure proc)
                {
                    var args = arrayValue.Value.Skip(1).Select(v => Evaluate(v, env)).ToList();
                    return proc.Execute(new ArrayValue(args), env);
                }
                else
                {
                    return value;
                }
            }

            return input;
        }
        #endregion
    }
}