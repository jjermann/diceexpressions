using System.Collections.Generic;

namespace DiceExpressions.Model.AlgebraicStructure
{
    //Monoid for "additive" operations (0, +)
    public interface IAdditiveMonoid<M> :
        IBaseStructure<M>
    {
        M Zero();
        M Add(M a, M b);
    }
}