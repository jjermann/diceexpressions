using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public static class DensityModuleExtensionMethods
    {
        public static Density<GP,MP> EmbedTo<G,M,GR,R,GP,MP,GRP,RP>(this Density<G,M> d)
            where G :
                IModuleWithExtension<M,GR,R,GP,MP,GRP,RP>,
                new()
            where GR :
                IRing<R>,
                IEmbedTo<R, RP>,
                new()
            where GP :
                IVectorspace<MP, GRP, RP>,
                new()
            where GRP :
                IField<RP>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            var resDict = d.ToDictionary().ToDictionary(
                x => g.EmbedTo(x.Key),
                x => x.Value
            );
            var resDensity = new Density<GP,MP>(resDict, d.Name);
            return resDensity;
        }
    }
}