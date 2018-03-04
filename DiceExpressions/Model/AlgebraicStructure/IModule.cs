namespace DiceExpressions.Model.AlgebraicStructure
{
    //Commutative Module
    public interface IModule<M, GR, R> :
        IAdditiveAbelianGroup<M>
        where GR :
            IRing<R>,
            new()
    { 
        M ScalarMult(R r, M m);
    }
}