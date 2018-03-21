using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public class Density<M> :
        Density<FieldType<M>, M>
    {
        public Density(Density<M> density) : base(density) { }
        public Density(IDictionary<M, PType> dict, string name = null) : base(dict, new FieldType<M>(), name) { }
    }

    public partial class Density<G, M> : IDensity<G,M>
    {
        public G BaseStructure { get; }
        public string Name { get; set; }

        protected IDictionary<M, PType> _densityDict;
        public IDictionary<M,PType> Dictionary => _densityDict;

        public Density(IDensity<G, M> density) : this(density.Dictionary, density.BaseStructure, density.Name) { }

        public Density(IDictionary<M, PType> dict, G baseStructure, string name = null)
        {
            BaseStructure = baseStructure;
            // TODO: We should use the equalitycomparer and comparer from the algebraic structure!!!
            _densityDict = dict;
            Name = name ?? DefaultName;
        }

        public PType this[M key] => _densityDict[key];
        // virtual public IList<M> Keys => _densityDict.Keys.ToList();
        // public IList<PType> Values => Keys.Select(k => _densityDict[k]).ToList();

        public override string ToString()
        {
            var res = "{" + string.Join(", ", _densityDict.Select(p => $"{{{p.Key}:{p.Value}}}")) + "}";
            return res;
       }
    }
}