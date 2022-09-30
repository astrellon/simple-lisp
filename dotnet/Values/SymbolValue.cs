#nullable enable

using System;

namespace DotnetLisp
{
    public struct SymbolValue
    {
        #region Fields
        public readonly string Value;
        #endregion

        #region Constructor
        public SymbolValue(string value)
        {
            this.Value = string.Intern(value);
        }
        #endregion

        #region Methods
        public override bool Equals(object? other)
        {
            if (other == null) return false;
            if (other is SymbolValue otherSymbol)
            {
                return otherSymbol.Value == this.Value;
            }
            return false;
        }

        public override string ToString()
        {
            return this.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(7, this.Value.GetHashCode());
        }

        public int CompareTo(object? other)
        {
            if (other == null) return 1;
            if (other is SymbolValue otherSymbol)
            {
                return this.Value.CompareTo(otherSymbol.Value);
            }

            return 1;
        }
        #endregion
    }
}