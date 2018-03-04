namespace DiceExpressions.Model.AlgebraicStructure
{
    //Vector space
    public interface IVectorspace<M, GR, R> :
        IModule<M, GR, R>
        where GR :
            IField<R>,
            new()
    { }
}