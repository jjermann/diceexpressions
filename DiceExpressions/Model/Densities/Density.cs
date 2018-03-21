using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.Densities
{
    public partial class Density<G, M, RF> : IDensity<G,M,RF>
    {
        public G BaseStructure { get; }
        public string Name { get; set; }

        protected IDictionary<M, RF> _densityDict;
        public IDictionary<M,RF> Dictionary => _densityDict;

        public IRealField<RF> RealField { get; }

        public Density(IDensity<G, M, RF> density) : this(density.Dictionary, density.BaseStructure, density.RealField, density.Name) { }

        public Density(IDictionary<M, RF> dict, G baseStructure, IRealField<RF> realField, string name = null)
        {
            BaseStructure = baseStructure;
            RealField = realField;
            // TODO: We should use the equalitycomparer and comparer from the algebraic structure!!!
            _densityDict = dict;
            Name = name ?? DefaultName;
        }

        public RF this[M key] => _densityDict[key];
        // virtual public IList<M> Keys => _densityDict.Keys.ToList();
        // public IList<RF> Values => Keys.Select(k => _densityDict[k]).ToList();

        public override string ToString()
        {
            var res = "{" + string.Join(", ", _densityDict.Select(p => $"{{{p.Key}:{p.Value}}}")) + "}";
            return res;
       }
    }
}