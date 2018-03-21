using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class RingType<M> :
        GroupType<M>,
        IRing<M>
    {
        virtual public M Multiply(M a, M b)
        {
            return (dynamic)a * (dynamic)b;
        }
        virtual public M One()
        {
            return (M)(dynamic)1;
        }
    }
}