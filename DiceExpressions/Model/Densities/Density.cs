using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public class Density<M> :
        Density<FieldType<M>, M>
    {
        public Density(Density<M> density) : base(density) { }
        public Density(IDictionary<M, PType> dict, string name = null) : base(dict, name) { }
    }

    public class Density<G, M> :
        IEquatable<Density<G, M>>
        where G :
            IEqualityComparer<M>,
            new()
    {
        public static readonly G AlgebraicStructure = new G();
        public static readonly PFieldType<PType> PField = new PFieldType<PType>(); 
        public G GetAlgebraicStructure() => AlgebraicStructure;

        protected IDictionary<M, PType> _densityDict;
        // TODO: Make this more save, this should not be public
        public IReadOnlyDictionary<M, PType> ToDictionary()
        {
            return new ReadOnlyDictionary<M, PType>(_densityDict);
        }

        public Density(Density<G, M> density) : this(density._densityDict, density.Name) { }

        public Density(IDictionary<M, PType> dict, string name = null)
        {
            // TODO: We should use the equalitycomparer and comparer from the algebraic structure!!!
            _densityDict = dict;
            Name = name ?? DefaultName;
        }

        private static string DefaultName => "Density";
        public Density(string name) 
        {
            this.Name = name;
               
        }
                public string Name { get; set; }
        public string TrimmedName {
            get {
                var trimmedName = Name;
                while(trimmedName.StartsWith("(") && trimmedName.EndsWith(")"))
                {
                    trimmedName = trimmedName.Substring(1, Math.Max(1, trimmedName.Length-2));
                }
                return trimmedName;
            }
        }
        public PType this[M key] => _densityDict[key];
        virtual public IList<M> Keys => _densityDict.Keys.ToList();
        public IList<PType> Values => Keys.Select(k => _densityDict[k]).ToList();

        public bool Equals(Density<G, M> other)
        {
            var isNull = object.ReferenceEquals(other, null);
            if (isNull)
            {
                return false;
            }
            var countsDiffer = Keys.Count != other.Keys.Count;
            if (countsDiffer)
            {
                return false;
            }
            var keysDiffer = Keys.Except(other.Keys).Any();
            if (keysDiffer)
            {
                return false;
            }
            var valuesDiffer = Values.Except(other.Values).Any();
            if (valuesDiffer)
            {
                return false;
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            var dObj = obj as Density<G, M>;
            return Equals(dObj);
        }
        //TODO: The keys are not sorted! So it could happend that D1/D2 are equal but have different hashcodes!
        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                foreach (var key in Keys)
                {
                    hash *= 397;
                    hash ^= key.GetHashCode();
                    hash *= 397;
                    hash ^= this[key].GetHashCode();
                }
            }
            return hash;
        }

        protected static IDictionary<N, PType> GetOpDictionary<N>(
            Density<G, M> d,
            Func<M, N> op)
        {
            var resDensity = new Dictionary<N, PType>();
            foreach (var key in d.Keys)
            {
                var resKey = op(key);
                if (!resDensity.ContainsKey(resKey))
                {
                    resDensity[resKey] = default(PType);
                }
                resDensity[resKey] += d[key];
            }
            return resDensity;
        }

        virtual public Density<S, N> Op<S, N>(
            Func<M, N> op,
            Func<string, string> opStrFunc = null)
            where S :
                IEqualityComparer<N>,
                new()
        {
            var resDensity = GetOpDictionary(this, op);
            var name = opStrFunc == null
                ? DefaultName
                : opStrFunc(Name);
            return new Density<S, N>(resDensity, name);
        }

        protected static IDictionary<N, PType> GetBinaryOpDictionary<N>(
            Density<G, M> d1,
            Density<G, M> d2,
            Func<M, M, N> op)
        {
            var resDensity = new Dictionary<N, PType>();
            foreach (var key1 in d1.Keys)
            {
                foreach (var key2 in d2.Keys)
                {
                    var resKey = op(key1, key2);
                    if (!resDensity.ContainsKey(resKey))
                    {
                        resDensity[resKey] = default(PType);
                    }
                    resDensity[resKey] += d1[key1]*d2[key2];
                }
            }
            return resDensity;
        }

        public static Density<S, N> BinaryOp<S, N>(
            Density<G, M> d1,
            Density<G, M> d2,
            Func<M, M, N> op, 
            Func<string,string,string> binOpStrFunc = null)
            where S :
                IEqualityComparer<N>,
                new()
        {
            var resDensity = GetBinaryOpDictionary(d1, d2, op);
            var name = binOpStrFunc == null
                ? DefaultName
                : binOpStrFunc(d1.Name, d2.Name);
            return new Density<S, N>(resDensity, name);
        }

        private static IEnumerable<IEnumerable<N>> CartesianProduct<N>(IEnumerable<IEnumerable<N>> sequences)
        {
            IEnumerable<IEnumerable<N>> emptyProduct = new[] { Enumerable.Empty<N>() };
            var res = sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] {item})
                );
            return res;
        }

        protected static IDictionary<N, PType> GetMultiOpDictionary<N>(
            IEnumerable<Density<G, M>> densityList,
            Func<IEnumerable<M>,N> multiOp)
        {
            var dListList = densityList.Select(d => d._densityDict.ToList());
            var resDensity = new Dictionary<N, PType>();
            var productList = CartesianProduct(dListList);

            foreach (var product in productList)
            {
                var multiOpArgument = product.Select(p => p.Key).ToList();
                var resultKey = multiOp(multiOpArgument);
                var resultValue = PField.Product(product.Select(p => p.Value));
                if (!resDensity.ContainsKey(resultKey))
                {
                    resDensity[resultKey] = default(PType);
                }
                resDensity[resultKey] += resultValue;
            }
            return resDensity;
        }

        public static Density<S, N> MultiOp<S, N>(
            IEnumerable<Density<G, M>> densityList,
            Func<IEnumerable<M>,N> multiOp,
            Func<IEnumerable<string>,string> multiOpStrFunc = null)
            where S :
                IEqualityComparer<N>,
                new()
        {
            var resDensity = GetMultiOpDictionary(densityList, multiOp);
            var name = multiOpStrFunc != null
                ? multiOpStrFunc(densityList.Select(d => d.Name))
                : DefaultName;
            return new Density<S, N>(resDensity, name);
        }

        public PType Prob(Func<M, bool> cond)
        {
            var resSum = Keys.Where(cond).Select(k => this[k]).Sum();
            return resSum;
        }
        public static PType BinaryProb(Density<G, M> d1, Density<G, M> d2, Func<M, M, bool> cond)
        {
            var resSum = default(PType);
            foreach (var key1 in d1.Keys)
            {
                foreach (var key2 in d2.Keys)
                {
                    if (cond(key1, key2))
                    {
                        resSum += d1[key1]*d2[key2];
                    }
                }
            }
            return resSum;
        }
        public static PType operator ==(Density<G, M> d1, Density<G, M> d2)
        {
            return BinaryProb(d1, d2, (a,b) => a.Equals(b));
        }
        public static PType operator !=(Density<G, M> d1, Density<G, M> d2)
        {
            return BinaryProb(d1, d2, (a,b) => !a.Equals(b));
        }

        protected static IDictionary<M, PType> GetConditionalDensityDictionary(
            Density<G, M> d,
            Func<M, bool> cond)
        {
            var resDict = new Dictionary<M, PType>();
            foreach (var key in d.Keys.Where(cond))
            {
                resDict[key] = d[key];
            }
            var conditionalProbability = resDict.Values.Sum();
            foreach (var key in resDict.Keys)
            {
                resDict[key] /= conditionalProbability;
            }
            return resDict;
        }

        virtual public Density<G, M> ConditionalDensity(Func<M, bool> cond, Func<string,string> condStrFunc = null)
        {
            var resDict = GetConditionalDensityDictionary(this, cond);
            var name = condStrFunc != null
                ? condStrFunc(Name)
                : DefaultName;
            var newDensity = new Density<G, M>(resDict, name);
            return newDensity;
        }

        public override string ToString()
        {
            var res = "{" + string.Join(", ", _densityDict.Select(p => $"{{{p.Key}:{p.Value}}}")) + "}";
            return res;
        }
    }
}