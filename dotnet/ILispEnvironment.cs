using System;

namespace DotnetLisp
{
    public interface ILispEnvironment
    {
        void Set(string key, object value);
        object Get(string key);
    }
}