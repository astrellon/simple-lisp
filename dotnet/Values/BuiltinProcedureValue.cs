using System;
using System.Collections.Generic;

namespace DotnetLisp
{
    public class BuiltinProcedureValue : IProcedure
    {
        #region Fields
        public readonly Func<List<object>, ILispEnvironment, object> Proc;
        #endregion

        #region Constructor
        public BuiltinProcedureValue(Func<List<object>, ILispEnvironment, object> proc)
        {
            this.Proc = proc;
        }
        #endregion

        #region Methods
        public object Execute(List<object> input, ILispEnvironment env)
        {
            return this.Proc.Invoke(input, env);
        }

        public override string ToString()
        {
            return "builtin-proc";
        }

        public override int GetHashCode()
        {
            return this.Proc.GetHashCode();
        }

        public int CompareTo(object? other)
        {
            if (other == null || !(other is BuiltinProcedureValue otherBuiltin)) return -1;
            return this.Proc == otherBuiltin.Proc ? 0 : 1;
        }
        #endregion
    }
}