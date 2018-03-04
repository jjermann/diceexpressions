namespace DiceExpressions.Model.AlgebraicStructure
{
    //Associative algebra with 1
    public interface IAlgebra<M, GR, R> : 
        IVectorspace<M, GR, R>,
        IMultiplicativeGroup<M>
        where GR :
            IField<R>,
            new()
    { }
}