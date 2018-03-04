namespace DiceExpressions.Model.AlgebraicStructure
{
    //Commutative ring with 1
    public interface IRing<R> :
        IAdditiveAbelianGroup<R>,
        IMultiplicativeMonoid<R>
    { }
}