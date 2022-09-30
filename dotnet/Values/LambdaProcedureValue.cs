using System;
using System.Collections.Generic;

namespace DotnetLisp
{
    public class LambdaProcedureValue : IProcedure
    {
        #region Fields
        public bool IsNull => false;
        public object RawValue => this.Body;

        public readonly IReadOnlyList<SymbolValue> Arguments;
        public readonly object Body;
        #endregion

        #region Constructor
        public LambdaProcedureValue(IReadOnlyList<SymbolValue> arguments, object body)
        {
            this.Arguments = arguments;
            this.Body = body;
        }
        #endregion

        #region Methods
        public object Execute(List<object> input, ILispEnvironment env)
        {
            var newEnv = new LispEnvironment(env);
            for (var i = 0; i < input.Count; i++)
            {
                newEnv.Set(this.Arguments[i].Value, input[i]);
            }
            return Lisp.Evaluate(this.Body, newEnv);
        }

        public override string ToString()
        {
            return "lambda-proc";
        }

        public override int GetHashCode()
        {
            return this.Body.GetHashCode();
        }
        #endregion
    }
}