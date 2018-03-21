using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicStructure
{
    public interface IRealEmbedding<R>
    {
        PType EmbedToReal(R r);
    }
}