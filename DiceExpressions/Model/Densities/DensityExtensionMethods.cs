using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public static class DensityExtensionMethods
    {
        public static Density<G,M> Negate<G,M>(this Density<G,M> d)
            where G :
                IAdditiveGroup<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return d.Op<G,M>(a => g.Negate(a), s => $"(-{s})");
        }

        public static Density<G,M> Add<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
               IAdditiveMonoid<M>,
               new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryOp<G,M>(d, d2, (a,b) => g.Add(a, b), (s,t) => $"({s}+{t})");
        }
        public static Density<G,M> Subtract<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IAdditiveGroup<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryOp<G,M>(d, d2, (a,b) => g.Subtract(a, b), (s,t) => $"({s}-{t})");
        }
        public static Density<G,M> Multiply<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IMultiplicativeMonoid<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryOp<G,M>(d, d2, (a,b) => g.Multiply(a, b), (s,t) => $"({s}*{t})");
        }
        public static Density<G,M> Divide<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IMultiplicativeGroup<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryOp<G,M>(d, d2, (a,b) => g.Divide(a, b), (s,t) => $"({s}/{t})");
        }

        public static PType EqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IEqualityComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Equals(a, b));
        }
        public static PType NeqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IEqualityComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => !g.Equals(a, b));
        }
        public static PType LtProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) < 0);
        }
        public static PType GtProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) > 0);
        }
        public static PType LeqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) <= 0);
        }
        public static PType GeqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) >= 0);
        }

        public static IList<M> SortedKeys<G,M>(this Density<G,M> d)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                new()
        {
            return d.Keys.OrderBy(k => k, Density<G,M>.AlgebraicStructure).ToList();
        }

        public static IList<PType> SortedValues<G,M>(this Density<G,M> d)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                new()
        {
            return d.SortedKeys().Select(k => d[k]).ToList();
        }

        public static Density<G,M> ArithMult<G,M>(this Density<G,M> d, int n)
            where G :
                IAdditiveMonoid<M>,
                new()
        {
            if (n<0)
            {
                throw new ArgumentException();
            }
            if (n == 0)
            {
                return new Zero<G,M>();
            }
            var newDensity = d;
            foreach(var step in Enumerable.Range(1, n-1))
            {
                newDensity = newDensity.Add(d);
            }
            newDensity.Name = $"({d.TrimmedName}).ArithMult({n})";
            return newDensity;
        }

        public static Density<G,M> ArithMult<G,M>(this Density<G,M> d, Density<int> dInt)
            where G :
                IAdditiveMonoid<M>,
                new()
        {
            var resDict = new Dictionary<M, PType>();
            foreach (var key in dInt.Keys)
            {
                var mulDensity = d.ArithMult(key);
                foreach (var mulKey in mulDensity.Keys)
                {
                    if (!resDict.ContainsKey(mulKey))
                    {
                        resDict[mulKey] = default(PType);
                    }
                    resDict[mulKey] += mulDensity[mulKey]*dInt[key];
                }
            }
            var newDensity = new Density<G,M>(resDict, $"{d.TrimmedName}.ArithMult({dInt.TrimmedName})");
            return newDensity;
        }

        // // TODO Add IEnumerable interface to Density?
        // public static IEnumerator<KeyValuePair<M,PType>> GetEnumerator<G,M>(this Density<G,M> d)
        //     where G :
        //         IEqualityComparer<M>,
        //         IComparer<M>,
        //         new()
        // { }
        // public static IEnumerator IEnumerable.GetEnumerator<G,M>(this Density<G,M> d)
        //     where G :
        //         IEqualityComparer<M>,
        //         IComparer<M>,
        //         new()
        // {
        //     return d.GetEnumerator();
        // }

        public static MP Expected<GP,MP,GRP,RP>(this Density<GP,MP> d)
            where GP :
                IVectorspace<MP,GRP,RP>,
                new()
            where GRP :
                IProbabilityField<RP>,
                new()
        {
            var gp = Density<GP,MP>.AlgebraicStructure;
            var grp = gp.GetBaseRing();
            //TODO: Instead values should lie in a probability field (not PType)...
            var expected = gp.Sum(d.ToDictionary().Select(p => gp.ScalarMult(grp.EmbedFrom(p.Value), p.Key)));
            return expected;
        }

        public static RP Variance<GP,MP,GRP,RP>(this Density<GP,MP> d)
            where GP :
                IVectorspace<MP,GRP,RP>,
                INormed<MP,GRP,RP>,
                new()
            where GRP :
                IProbabilityField<RP>,
                new()
        {
            var gp = Density<GP,MP>.AlgebraicStructure;
            var grp = gp.GetBaseRing();
            var expected = d.Expected<GP,MP,GRP,RP>();
            Func<MP, RP> normFunc = mp => grp.ScalarPow(gp.Norm(gp.Subtract(expected, mp)), 2);
            //TODO: Instead values should lie in a probability field (not PType)...
            var squaredDistances = d.ToDictionary()
                .Select(p => grp.Multiply(grp.EmbedFrom(p.Value), normFunc(p.Key)))
                .ToList();
            var variance = grp.Sum(squaredDistances);
            return variance;
        }
        public static RP Stdev<GP,MP,GRP,RP>(this Density<GP,MP> d)
            where GP :
                IVectorspace<MP,GRP,RP>,
                INormed<MP,GRP,RP>,
                new()
            where GRP :
                IProbabilityField<RP>,
                new()
        {
            var grp = Density<GP,MP>.AlgebraicStructure.GetBaseRing();
            return grp.Sqrt(d.Variance<GP,MP,GRP,RP>());
        }
        public static PType Cdf<G,M>(this Density<G,M> d, PType x)
            where G :
                IEqualityComparer<M>,
                IComparer<M>,
                IEmbedTo<M, PType>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            var pf = Density<G,M>.PField;
            return d.Prob(a => pf.Compare(g.EmbedTo(a), x) <= 0);
        }

        public static MP Expected<G,M,GR,R,GP,MP,GRP,RP>(this Density<G,M> d)
            where G :
                IModuleWithProbabilityExtension<M,GR,R,GP,MP,GRP,RP>,
                new()
            where GR :
                IRing<R>,
                IEmbedTo<R, RP>,
                new()
            where GP :
                IVectorspace<MP, GRP, RP>,
                new()
            where GRP :
                IProbabilityField<RP>,
                new()
        {
            var embeddedDensity = d.EmbedTo<G,M,GR,R,GP,MP,GRP,RP>();
            return embeddedDensity.Expected<GP,MP,GRP,RP>();
        }
        public static RP Variance<G,M,GR,R,GP,MP,GRP,RP>(this Density<G,M> d)
            where G :
                IModuleWithProbabilityExtension<M,GR,R,GP,MP,GRP,RP>,
                new()
            where GR :
                IRing<R>,
                IEmbedTo<R, RP>,
                new()
            where GP :
                IVectorspace<MP, GRP, RP>,
                INormed<MP, GRP, RP>,
                new()
            where GRP :
                IProbabilityField<RP>,
                new()
        {
            var embeddedDensity = d.EmbedTo<G,M,GR,R,GP,MP,GRP,RP>();
            return embeddedDensity.Variance<GP,MP,GRP,RP>();
        }
        public static RP Stdev<G,M,GR,R,GP,MP,GRP,RP>(this Density<G,M> d)
            where G :
                IModuleWithProbabilityExtension<M,GR,R,GP,MP,GRP,RP>,
                new()
            where GR :
                IRing<R>,
                IEmbedTo<R, RP>,
                new()
            where GP :
                IVectorspace<MP, GRP, RP>,
                INormed<MP, GRP, RP>,
                new()
            where GRP :
                IProbabilityField<RP>,
                new()
        {
            var embeddedDensity = d.EmbedTo<G,M,GR,R,GP,MP,GRP,RP>();
            return embeddedDensity.Stdev<GP,MP,GRP,RP>();
        }

        public static MultiDensity<G,M> AsMultiDensity<G,M>(this Density<G,M> d, int n)
            where G :
                IAdditiveMonoid<M>,
                new()
        {
            var dList = Enumerable.Repeat(d, n).ToList();
            var multiDensity = new MultiDensity<G,M>(dList);
            return multiDensity;
        }

        public static Density<G,M> WithAdvantage<G,M>(this Density<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            if (n < 0)
            {
                return d.WithDisadvantage(-n);
            }
            if (n == 0)
            {
                return d;
            }
            if (n == 1)
            {
                return Density<G,M>.BinaryOp<G,M>(d, d, (a,b) => g.Max(a,b), (s,t) => $"a{s}");
            }
            var mDensity = d.AsMultiDensity(n).MultiOp<G,M>(e => g.Max(e), e => $"a{n}{e.First()}");
            return mDensity;
        }

        public static Density<G,M> WithDisadvantage<G,M>(this Density<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>,
                new()
        {
            var g = Density<G,M>.AlgebraicStructure;
            if (n < 0)
            {
                return d.WithAdvantage(-n);
            }
            if (n == 0)
            {
                return d;
            }
            if (n == 1)
            {
                return Density<G,M>.BinaryOp<G,M>(d, d, (a,b) => g.Min(a,b), (s,t) => $"d{s}");
            }
            var mDensity = d.AsMultiDensity(n).MultiOp<G,M>(e => g.Min(e), e => $"d{n}{e.First()}");
            return mDensity;
        }
    }
}