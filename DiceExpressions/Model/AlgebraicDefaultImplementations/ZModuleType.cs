using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{

    // A commonly used default class, we base on RingType<M> to have a default multiplication already defined
    public class ZModuleType<M,RF> :
        RingType<M>,
        IModuleWithExtension<M, int, RF, RF>
    {
        private static readonly IRing<int> _baseRing = new RingType<int>();
        private static readonly IRealVectorspace<RF, RF> _extensionVectorSpace = new RealVectorspaceType<RF, RF>();

        public IRealVectorspace<RF, RF> ExtensionVectorspace => _extensionVectorSpace;
        public IRing<int> BaseRing => _baseRing;

        public RF ModuleEmbedding(M m)
        {
            return (RF)(dynamic)(m);
        }

        public RF RingEmbedding(int r)
        {
            return (RF)(dynamic)(r);
        }

        virtual public M ScalarMult(int r, M m)
        {
            return AlgebraicExtensionMethods.ScalarMult(this, r, m);
        }
    }
}