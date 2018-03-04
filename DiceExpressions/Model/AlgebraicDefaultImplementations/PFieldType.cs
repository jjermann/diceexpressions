using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class PFieldType<R> :
        FieldType<R>,
        IProbabilityField<R>
    {
        virtual public R EmbedFrom(PType p)
        {
            return (R)(dynamic)p;
        }
    }
}