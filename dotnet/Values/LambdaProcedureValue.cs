using System;
using System.Collections.Generic;

namespace DotnetLisp
{
    public class LambdaProcedureValue : IValue, IProcedure
    {
        #region Fields
        public bool IsNull => false;
        public object RawValue => this.Body;

        public readonly IReadOnlyList<SymbolValue> Arguments;
        public readonly IValue Body;
        #endregion

        #region Constructor
        public LambdaProcedureValue(IReadOnlyList<SymbolValue> arguments, IValue body)
        {
            this.Arguments = arguments;
            this.Body = body;
        }
        #endregion

        #region Methods
        public IValue Execute(ArrayValue input, LispEnvironment env)
        {
            var newEnv = new LispEnvironment(env);
            for (var i = 0; i < input.Value.Count; i++)
            {
                newEnv.Set(this.Arguments[i].Value, input.Value[i]);
            }
            return Lisp.Evaluate(this.Body, newEnv);
        }

        public int CompareTo(IValue? other)
        {
            return 0;
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