using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;

namespace DiceExpressions.Model.AlgebraicStructure
{
    //To allow mapping from a given IModule<R, M> to GP = IVectorspace<RP, MP>
    public interface IModuleWithExtension<M, GR, R, GP, MP, GRP, RP> :
        IModule<M, GR, R>,
        IEmbedTo<M, MP>
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
    { }
}