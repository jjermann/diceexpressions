namespace DiceExpressions.Model.AlgebraicStructure
{
    //Associative algebra with 1
    public interface IAlgebra<M, R> : 
        IVectorspace<M, R>,
        IMultiplicativeGroup<M>
    { }
}