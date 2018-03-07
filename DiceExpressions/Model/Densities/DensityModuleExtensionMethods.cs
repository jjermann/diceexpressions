using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public static class DensityModuleExtensionMethods
    {
        public static Density<IRealVectorspace<MP,RP>,MP> EmbedTo<M,R,MP,RP>(this Density<IModuleWithExtension<M,R,MP,RP>,M> d)
        {
            var g = d.BaseStructure;
            var resDict = d.ToDictionary().ToDictionary(
                x => g.ModuleEmbedding(x.Key),
                x => x.Value
            );
            var resDensity = new Density<IRealVectorspace<MP,RP>,MP>(resDict, g.ExtensionVectorspace, d.Name);
            return resDensity;
        }
    }
}