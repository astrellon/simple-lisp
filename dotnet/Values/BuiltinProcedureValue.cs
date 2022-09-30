using System;
using System.Linq;

namespace DotnetLisp
{
    public class BuiltinProcedureValue : IValue, IProcedure
    {
        #region Fields
        public bool IsNull => false;
        public object RawValue => this.Proc;

        public readonly Func<ArrayValue, LispEnvironment, IValue> Proc;
        #endregion

        #region Constructor
        public BuiltinProcedureValue(Func<ArrayValue, LispEnvironment, IValue> proc)
        {
            this.Proc = proc;
        }
        #endregion

        #region Methods
        public IValue Execute(ArrayValue input, LispEnvironment env)
        {
            return this.Proc.Invoke(input, env);
        }

        public int CompareTo(IValue? other)
        {
            if (other == null || !(other is BuiltinProcedureValue otherBuiltin)) return -1;
            return this.Proc == otherBuiltin.Proc ? 0 : 1;
        }

        public override string ToString()
        {
            return "builtin-proc";
        }

        public override int GetHashCode()
        {
            return this.Proc.GetHashCode();
        }
        #endregion
    }
}