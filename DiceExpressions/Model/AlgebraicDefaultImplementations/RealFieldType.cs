using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class RealFieldType<R> :
        FieldType<R,PType>,
        IRealField<R>
    {
        virtual public R EmbedFromReal(PType p)
        {
            return (R)(dynamic)p;
        }
    }
}