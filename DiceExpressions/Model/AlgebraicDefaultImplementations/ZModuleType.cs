using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{

    public class ZModuleType<M> :
        RingType<M>,
        IModuleWithProbabilityExtension<M, RingType<int>, int, PVectorspaceType<PType>, PType, PFieldType<PType>, PType>
    {
    virtual public M ScalarMult(int r, M m)
        {
            return AlgebraicExtensionMethods.ScalarMult(this, r, m);
        }
    }
}