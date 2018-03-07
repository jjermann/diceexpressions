namespace DiceExpressions.Model.AlgebraicStructure
{
    //Commutative Module
    public interface IModule<M, R> :
        IAdditiveAbelianGroup<M>
    {
        IRing<R> BaseRing { get; }
        M ScalarMult(R r, M m);
    }
}