using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetLisp
{
    public static class ListExtensions
    {
        #region Methods
        public static T PopBack<T>(this List<T> input)
        {
            if (input.Any())
            {
                var result = input.Last();
                input.RemoveAt(input.Count - 1);
                return result;
            }

            throw new ArgumentException("Unable to pop empty list");
        }

        public static T PopFront<T>(this List<T> input)
        {
            if (input.Any())
            {
                var result = input.First();
                input.RemoveAt(0);
                return result;
            }

            throw new ArgumentException("Unable to pop empty list");
        }
        #endregion
    }
}