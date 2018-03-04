using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class RingType<M> :
        GroupType<M>,
        IRing<M>,
        IEmbedTo<M, PType>
    {
        virtual public M Multiply(M a, M b)
        {
            return (dynamic)a * (dynamic)b;
        }
        virtual public M One()
        {
            return (M)(dynamic)1;
        }
        virtual public PType EmbedTo(M r)
        {
            return (PType)(dynamic)r;
        }
    }
}