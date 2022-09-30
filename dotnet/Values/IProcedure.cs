using System;
using System.Collections.Generic;

namespace DotnetLisp
{
    public interface IProcedure
    {
        object Execute(List<object> input, ILispEnvironment env);
    }
}