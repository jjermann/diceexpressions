using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public partial class Density<G, M> : IEnumerable<KeyValuePair<M, PType>>
    {
        public IEnumerator<KeyValuePair<M, PType>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}