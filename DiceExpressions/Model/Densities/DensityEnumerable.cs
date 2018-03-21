using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiceExpressions.Model.Densities
{
    public partial class Density<G, M, RF> : IEnumerable<KeyValuePair<M, RF>>
    {
        public IEnumerator<KeyValuePair<M, RF>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}