namespace DiceExpressions.Model.AlgebraicStructure
{
    //Vector space
    public interface IVectorspace<M, R> :
        IModule<M, R>
    {
        IField<R> BaseField { get; }
    }
}