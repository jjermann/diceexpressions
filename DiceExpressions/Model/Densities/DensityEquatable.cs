using System;
using System.Collections.Generic;
using System.Linq;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public partial class Density<G, M> : IEquatable<IDensity<G,M>>
    {
        public bool Equals(IDensity<G, M> other)
        {
            var isNull = object.ReferenceEquals(other, null);
            if (isNull)
            {
                return false;
            }
            var countsDiffer = Dictionary.Count != other.Dictionary.Count;
            if (countsDiffer)
            {
                return false;
            }
            var keysDiffer = Dictionary.Keys.Except(other.Dictionary.Keys).Any();
            if (keysDiffer)
            {
                return false;
            }
            var valuesDiffer = Dictionary.Values.Except(other.Dictionary.Values).Any();
            if (valuesDiffer)
            {
                return false;
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            var dObj = obj as IDensity<G, M>;
            return Equals(dObj);
        }
        //TODO: The keys are not sorted! So it could happend that D1/D2 are equal but have different hashcodes!
        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                foreach (var key in this.GetKeys())
                {
                    hash *= 397;
                    hash ^= key.GetHashCode();
                    hash *= 397;
                    hash ^= this[key].GetHashCode();
                }
            }
            return hash;
        }
    }
}