namespace DiceExpressions.Model.AlgebraicStructure
{
    //Group for "additive" operations (0, +, -)
    public interface IAdditiveGroup<M> :
        IAdditiveMonoid<M>
    {
        M Negate(M a);
    }
}