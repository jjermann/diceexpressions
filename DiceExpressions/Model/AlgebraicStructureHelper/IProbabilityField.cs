using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicStructureHelper
{
    //TODO: Don't rely on embeddings, get rid of PType except here
    public interface IProbabilityField<R> :
        IField<R>,
        IComparer<R>,
        IEmbedTo<R, PType>,
        IEmbedFrom<R, PType>
    { }
}