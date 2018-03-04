using System.Collections.Generic;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.AlgebraicDefaultImplementations
{
    public class GroupType<M> :
        IAdditiveAbelianGroup<M>,
        IEqualityComparer<M>,
        IComparer<M>
    {
        private static readonly IEqualityComparer<M> _equalityComparer = EqualityComparer<M>.Default;
        private static readonly IComparer<M> _comparer = Comparer<M>.Default;

        virtual public M Negate(M a)
        {
            return -(dynamic)a;
        }

        virtual public M Zero()
        {
            return (M)(dynamic)0;
        }

        virtual public M Add(M a, M b)
        {
            return (dynamic)a + (dynamic)b;
        }

        virtual public bool Equals(M x, M y)
        {
            return _equalityComparer.Equals(x, y);
        }

        virtual public int GetHashCode(M obj)
        {
            return _equalityComparer.GetHashCode(obj);
        }

        virtual public int Compare(M x, M y)
        {
            return _comparer.Compare(x, y);
        }
    }
}