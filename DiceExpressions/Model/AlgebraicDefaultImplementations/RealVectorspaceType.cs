using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class RealVectorspaceType<M,RF> :
        GroupType<M>,
        IRealVectorspace<M, RF>
    {
        private static readonly IRealField<RF> _baseField = new RealFieldType<RF>();
        public IField<RF> BaseField => _baseField;

        public IRing<RF> BaseRing => _baseField;

        public IRealField<RF> BaseRealField => _baseField;

        // We provide a default implementation for Norm that just tries to cast to RF first...
        public RF Norm(M a)
        {
            return _baseField.Abs((RF)(dynamic)(a));  
        }

        virtual public M ScalarMult(RF r, M m)
        {
            return (M)((dynamic)r*(dynamic)m);
        }
    }
}