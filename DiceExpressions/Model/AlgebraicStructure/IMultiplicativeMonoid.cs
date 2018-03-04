using System.Collections.Generic;

namespace DiceExpressions.Model.AlgebraicStructure
{
    //Monoid for "multiplicative" operations (1, *)
    public interface IMultiplicativeMonoid<M> :
        IEqualityComparer<M>
    {
        M One();
        M Multiply(M a, M b);
    }
}