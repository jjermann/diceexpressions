using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class RealVectorspaceType<M> :
        GroupType<M>,
        IRealVectorspace<M, PType>
    {
        private static readonly IRealField<PType> _baseField = new RealFieldType<PType>();
        public IField<PType> BaseField => _baseField;

        public IRing<PType> BaseRing => _baseField;

        public IRealField<PType> BaseRealField => _baseField;

        // We provide a default implementation for Norm that just tries to cast to PType first...
        public PType Norm(M a)
        {
            return _baseField.Abs((PType)(dynamic)(a));  
        }

        virtual public M ScalarMult(PType r, M m)
        {
            return (M)((dynamic)r*(dynamic)m);
        }
    }
}