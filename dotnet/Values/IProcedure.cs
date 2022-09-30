namespace DotnetLisp
{
    public interface IProcedure
    {
        IValue Execute(ArrayValue input, ILispEnvironment env);
    }
}