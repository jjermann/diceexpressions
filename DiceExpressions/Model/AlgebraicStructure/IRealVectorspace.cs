namespace DiceExpressions.Model.AlgebraicStructure
{
    public interface IRealVectorspace<M,R> :
        IVectorspace<M,R>
    {
        IRealField<R> BaseRealField { get; }
        R Norm(M a);
    }
}