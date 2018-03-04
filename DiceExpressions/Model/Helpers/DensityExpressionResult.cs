using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructureHelper;
using DiceExpressions.Model.Densities;
using PType = System.Double;

namespace DiceExpressions.Model.Helpers
{
    public class DensityExpressionResult<G, M>
        where G :
            IEqualityComparer<M>,
            new()
    {
        public Density<G, M> Density { get; set; }
        public PType? Probability { get; set; }
        public string ErrorString { get; set; }
    }
}