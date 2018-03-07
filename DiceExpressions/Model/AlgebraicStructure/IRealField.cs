using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicStructure
{
    //TODO: Don't rely on embeddings, get rid of PType except here
    public interface IRealField<R> :
        IField<R>,
        IComparer<R>,
        IRealEmbedding<R, PType>
    { 
        R EmbedFromReal(PType p);
    }
}