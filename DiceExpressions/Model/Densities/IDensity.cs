using System;
using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.Densities
{
    public interface IDensity<out G, M, RF>
    {
        IRealField<RF> RealField { get; }
        G BaseStructure { get; }
        string Name { get; set; }
        RF this[M key] { get; }
        IDictionary<M, RF> Dictionary { get; }
    }
}