using System;
using System.Collections.Generic;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public interface IDensity<out G, M>
    {
        G BaseStructure { get; }
        string Name { get; set; }
        PType this[M key] { get; }
        IDictionary<M, PType> Dictionary { get; }
    }
}