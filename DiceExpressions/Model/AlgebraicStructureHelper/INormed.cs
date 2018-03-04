using System.Collections.Generic;

namespace DiceExpressions.Model.AlgebraicStructureHelper
{
    //This only makes sense for IVectorpace<M> but then we would need to define a NormedAlgebra/etc...
    public interface INormed<M, GP, RP> :
        IEqualityComparer<M>
        where GP :
            IProbabilityField<RP>,
            new()
    {
        RP Norm(M a);
    }
}