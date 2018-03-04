namespace DiceExpressions.Model.AlgebraicStructure
{
    //Group for "multiplicative" operations (1, *, /)

    public interface IMultiplicativeGroup<M> :
        IMultiplicativeMonoid<M>
    {
        M Inverse(M a);
    }
}