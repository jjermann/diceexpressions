using System.Collections.Generic;

namespace DiceExpressions.Model.AlgebraicStructure
{
    public interface IBaseStructure<in M> :
        IEqualityComparer<M>,
        IComparer<M>
    { }
}