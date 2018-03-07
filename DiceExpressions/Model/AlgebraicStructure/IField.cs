namespace DiceExpressions.Model.AlgebraicStructure
{
    //Field
    public interface IField<R> :
        IRing<R>,
        IMultiplicativeGroup<R>
    { }
}