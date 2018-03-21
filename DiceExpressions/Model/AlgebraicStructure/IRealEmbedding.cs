namespace DiceExpressions.Model.AlgebraicStructure
{
    public interface IRealEmbedding<R,RF>
    {
        RF EmbedToReal(R r);
    }
}