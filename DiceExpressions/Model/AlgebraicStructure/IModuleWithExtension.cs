using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicStructure
{
    //To allow mapping from a given IModule<M, R> to IProbabilityVectorspace<MP, RP>
    public interface IModuleWithExtension<M, R, MP, RP> :
        IModule<M, R>
    {
        MP ModuleEmbedding(M m);
        RP RingEmbedding(R r);
        IRealVectorspace<MP, RP> ExtensionVectorspace { get; }
    }
}