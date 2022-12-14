using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable enable

namespace DotnetLisp
{
    public struct ArrayValue : IValue
    {
        #region Fields
        public readonly IReadOnlyList<IValue> Value;
        public object RawValue => this.Value;
        public bool IsNull => false;
        #endregion

        #region Constructor
        public ArrayValue(IReadOnlyList<IValue> value)
        {
            this.Value = value;
        }
        #endregion

        #region Methods
        public override bool Equals(object? other)
        {
            if (other == null) return false;
            if (other is ArrayValue otherArray)
            {
                if (this.Value.Count != otherArray.Value.Count)
                {
                    return false;
                }

                for (var i = 0; i < this.Value.Count; i++)
                {
                    if (!this.Value[i].Equals(otherArray.Value[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append('[');
            var first = true;
            foreach (var value in this.Value)
            {
                if (!first)
                {
                    result.Append(',');
                }
                first = false;

                result.Append(value.ToString());
            }

            result.Append(']');
            return result.ToString();
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetIndex(int index)
        {
            if (index < 0)
            {
                return this.Value.Count + index;
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayValue Sublist(int index, int length = -1)
        {
            index = this.GetIndex(index);
            if (length < 0)
            {
                return new ArrayValue(this.Value.Skip(index).ToList());
            }
            return new ArrayValue(this.Value.Skip(index).Take(length).ToList());
        }

        public int CompareTo(IValue? other)
        {
            if (other == null) return 1;
            if (other is ArrayValue otherArray)
            {
                var compareLength = this.Value.Count.CompareTo(otherArray.Value.Count);
                if (compareLength != 0)
                {
                    return compareLength;
                }

                for (var i = 0; i < this.Value.Count; i++)
                {
                    var compare = this.Value[i].CompareTo(otherArray.Value[i]);
                    if (compare != 0)
                    {
                        return compare;
                    }
                }

                return 0;
            }

            return 1;
        }
        #endregion
    }
}