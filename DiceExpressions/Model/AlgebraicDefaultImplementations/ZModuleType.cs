using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{

    // A commonly used default class, we base on RingType<M> to have a default multiplication already defined
    public class ZModuleType<M> :
        RingType<M>,
        IModuleWithExtension<M, int, PType, PType>
    {
        private static readonly IRing<int> _baseRing = new RingType<int>();
        private static readonly IRealVectorspace<PType, PType> _extensionVectorSpace = new RealVectorspaceType<PType>();

        public IRealVectorspace<PType, PType> ExtensionVectorspace => _extensionVectorSpace;
        public IRing<int> BaseRing => _baseRing;

        public PType ModuleEmbedding(M m)
        {
            return (PType)(dynamic)(m);
        }

        public PType RingEmbedding(int r)
        {
            return (PType)(dynamic)(r);
        }

        virtual public M ScalarMult(int r, M m)
        {
            return AlgebraicExtensionMethods.ScalarMult(this, r, m);
        }
    }
}