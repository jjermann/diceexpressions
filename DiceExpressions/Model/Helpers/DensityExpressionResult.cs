using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.Densities;

namespace DiceExpressions.Model.Helpers
{
    public class DensityExpressionResult<G, M, RF>
        where RF :
            struct
    {
        public IDensity<G, M, RF> Density { get; set; }
        public RF? Probability { get; set; }
        public string ErrorString { get; set; }
    }
}