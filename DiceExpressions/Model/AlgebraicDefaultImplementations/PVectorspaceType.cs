using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class PVectorspaceType<M> :
        GroupType<M>,
        IVectorspace<M, PFieldType<PType>, PType>
    {
        virtual public M ScalarMult(PType r, M m)
        {
            return (M)((dynamic)r*(dynamic)m);
        }
    }
}