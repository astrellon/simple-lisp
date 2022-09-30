using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetLisp
{
    using ArrayValue = List<object>;
    public static class Lisp
    {
        #region Fields
        #endregion

        #region Methods
        public static object Evaluate(string input, ILispEnvironment env)
        {
            var parsed = LispParser.ReadFromTokens(LispParser.Tokenize(input));
            return Evaluate(parsed, env);
        }

        public static object Evaluate(object input, ILispEnvironment env)
        {
            if (input is SymbolValue symbolValue)
            {
                return env.Get(symbolValue.Value);
            }
            else if (input is ArrayValue arrayValue)
            {
                var first = arrayValue[0];
                if (first is SymbolValue firstSymbol)
                {
                    switch (firstSymbol.Value)
                    {
                        case "lambda":
                            {
                                var lambdaArgs = ((ArrayValue)arrayValue[1]).Select(v => (SymbolValue)v).ToList();
                                var body = arrayValue[2];
                                return new LambdaProcedureValue(lambdaArgs, body);
                            }
                        case "define":
                            {
                                env.Set(arrayValue[1].ToString(), Lisp.Evaluate(arrayValue[2], env));
                                return input;
                            }
                        case "when":
                            {
                                var test = arrayValue[1];
                                var body = arrayValue[2];
                                if (Evaluate(test, env).Equals(true))
                                {
                                    return Evaluate(body, env);
                                }
                                return null;
                            }
                        case "if":
                            {
                                var test = arrayValue[1];
                                var ifTrue = arrayValue[2];
                                var ifFalse = arrayValue[3];
                                var compResult = Evaluate(test, env).Equals(true);

                                return Evaluate(compResult ? ifTrue : ifFalse, env);
                            }
                        case "loop":
                            {
                                var test = arrayValue[1];
                                var body = arrayValue[2];

                                object result = null;
                                while (Evaluate(test, env).Equals(true))
                                {
                                    result = Evaluate(body, env);
                                }
                                return result;
                            }
                        case "begin":
                            {
                                object result = null;
                                for (var i = 0; i < arrayValue.Count; i++)
                                {
                                    result = Evaluate(arrayValue[i], env);
                                }
                                return result;
                            }
                    }
                }

                var value = Evaluate(first, env);
                if (value is IProcedure proc)
                {
                    var args = new object[arrayValue.Count - 1];
                    for (var i = 1; i < arrayValue.Count; i++)
                    {
                        args[i - 1] = Evaluate(arrayValue[i], env);
                    }
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