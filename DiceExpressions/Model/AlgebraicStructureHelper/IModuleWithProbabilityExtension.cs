using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicStructureHelper
{
    public interface IModuleWithProbabilityExtension<M, GR, R, GP, MP, GRP, RP> :
        IModuleWithExtension<M, GR, R, GP, MP, GRP, RP>
        where GR :
            IRing<R>,
            IEmbedTo<R, RP>,
            new()
        where GP :
            IVectorspace<MP, GRP, RP>,
            new()
        where GRP :
            IProbabilityField<RP>,
            new()
    { }
}