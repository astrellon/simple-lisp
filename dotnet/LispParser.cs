using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DotnetLisp
{
    public static class LispParser
    {
        #region Fields
        private static Regex TokenRegex = new Regex("[^\\s\"']+|\"([^\"]*)\"|'([^']*)'");
        #endregion

        #region Methods
        public static List<string> Tokenize(string input)
        {
            var cleaned = input.Replace("(", " ( ")
                .Replace(")", " ) ")
                .Trim();

            return TokenRegex.Matches(cleaned).Select(m => m.Value).ToList();
        }

        public static object ReadFromTokens(List<string> tokens)
        {
            if (tokens.Count == 0)
            {
                throw new ArgumentException("Unexpected end of tokens");
            }

            var token = tokens.PopFront();
            if (token == "(")
            {
                var list = new List<object>();
                while (tokens.First() != ")")
                {
                    list.Add(ReadFromTokens(tokens));
                }
                tokens.PopFront();
                return new List<object>(list);
            }
            else if (token == ")")
            {
                throw new ArgumentException("Unexpected )");
            }
            else
            {
                return Atom(token);
            }
        }

        public static object Atom(string input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            if (double.TryParse(input, out var number))
            {
                return number;
            }
            if (bool.TryParse(input, out var boolean))
            {
                return boolean;
            }
            if (input.First() == '"' && input.Last() == '"')
            {
                return input.Substring(1, input.Length - 2);
            }
            return new SymbolValue(input);
        }
        #endregion
    }
}