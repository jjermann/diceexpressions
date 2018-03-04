namespace DiceExpressions.Model.AlgebraicStructure
{
    //Depending on the context the assumption is made that the embedding is functorial
    public interface IEmbedFrom<R,F>
    {
         R EmbedFrom(F r);
    }
}