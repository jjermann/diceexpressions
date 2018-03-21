using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class FieldType<M> :
        ZModuleType<M>,
        IField<M>,
        IRealEmbedding<M>,
        IModuleWithExtension<M, int, PType, PType>
    {
        public PType EmbedToReal(M r)
        {
            return (PType)(dynamic)(r);
        }

        virtual public M Inverse(M a)
        {
            return One()/(dynamic)a;
        }
    }
}