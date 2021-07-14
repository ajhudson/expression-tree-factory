namespace FunWithExpressionTrees
{
    public interface IConsumerFactory
    {
        IConsumer CreateInstance(string typeName);
    }
}