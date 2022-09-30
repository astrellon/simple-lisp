namespace DotnetLisp
{
    public interface ILispEnvironment
    {
        void Set(string key, IValue value);
        IValue Get(string key);

    }
}