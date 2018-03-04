namespace DiceExpressions.Model.AlgebraicStructure
{
    //Depending on the context the assumption is made that the embedding is functorial
    public interface IEmbedTo<R,F>
    {
         F EmbedTo(R r);
    }
}