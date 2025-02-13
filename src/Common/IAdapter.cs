namespace StatementIQ
{
    public interface IAdapter<out TOuput, in TInput> where TInput : class
    {
        TOuput Adapt(TInput input);
    }
}