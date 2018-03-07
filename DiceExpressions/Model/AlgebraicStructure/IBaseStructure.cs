using System.Collections.Generic;

namespace DiceExpressions.Model.AlgebraicStructure
{
    public interface IBaseStructure<M> :
        IEqualityComparer<M>,
        IComparer<M>
    { }
}