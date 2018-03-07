using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class RealVectorspaceType<M> :
        GroupType<M>,
        IRealVectorspace<M, PType>
    {
        private static readonly IField<PType> _baseField = new FieldType<PType>();
        public IField<PType> BaseField => _baseField;

        public IRing<PType> BaseRing => throw new System.NotImplementedException();

        public IRealField<PType> BaseRealField => throw new System.NotImplementedException();

        public PType Norm(M a)
        {
            throw new System.NotImplementedException();  
        }

        virtual public M ScalarMult(PType r, M m)
        {
            return (M)((dynamic)r*(dynamic)m);
        }
    }
}