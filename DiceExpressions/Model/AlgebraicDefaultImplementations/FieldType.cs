using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class FieldType<M,RF> :
        ZModuleType<M,RF>,
        IField<M>,
        IRealEmbedding<M,RF>,
        IModuleWithExtension<M, int, RF, RF>
    {
        public RF EmbedToReal(M r)
        {
            return (RF)(dynamic)(r);
        }

        virtual public M Inverse(M a)
        {
            return One()/(dynamic)a;
        }
    }
}