using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class FieldType<M> :
        ZModuleType<M>,
        IField<M>
    {
        virtual public M Inverse(M a)
        {
            return One()/(dynamic)a;
        }
    }
}