namespace DiceExpressions.Model.AlgebraicStructure
{
    public interface IRealEmbedding<R, PType>
    {
        PType EmbedToReal(R r);
    }
}