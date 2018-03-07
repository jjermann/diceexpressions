using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public static class DensityExtensionMethods
    {
        public static Density<G,M> Negate<G,M>(this Density<G,M> d)
            where G :
                IAdditiveGroup<M>
        {
            var g = d.BaseStructure;
            return d.Op<G,M>(g, a => g.Negate(a), s => $"(-{s})");
        }

        public static Density<G,M> Add<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
               IAdditiveMonoid<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Add(a, b), (s,t) => $"({s}+{t})");
        }
        public static Density<G,M> Subtract<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IAdditiveGroup<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Subtract(a, b), (s,t) => $"({s}-{t})");
        }
        public static Density<G,M> Multiply<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IMultiplicativeMonoid<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Multiply(a, b), (s,t) => $"({s}*{t})");
        }
        public static Density<G,M> Divide<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IMultiplicativeGroup<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Divide(a, b), (s,t) => $"({s}/{t})");
        }

        public static PType EqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IBaseStructure<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Equals(a, b));
        }
        public static PType NeqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IBaseStructure<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => !g.Equals(a, b));
        }
        public static PType LtProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) < 0);
        }
        public static PType GtProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) > 0);
        }
        public static PType LeqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) <= 0);
        }
        public static PType GeqProb<G,M>(this Density<G,M> d, Density<G,M> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) >= 0);
        }

        public static IList<M> SortedKeys<G,M>(this Density<G,M> d)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            return d.Keys.OrderBy(k => k, d.BaseStructure).ToList();
        }

        public static IList<PType> SortedValues<G,M>(this Density<G,M> d)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            return d.SortedKeys().Select(k => d[k]).ToList();
        }

        public static Density<G,M> ArithMult<G,M>(this Density<G,M> d, int n)
            where G :
                IAdditiveMonoid<M>
        {
            if (n<0)
            {
                throw new ArgumentException();
            }
            if (n == 0)
            {
                return new Zero<G,M>(d.BaseStructure);
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
                IAdditiveMonoid<M>
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
            var newDensity = new Density<G,M>(resDict, d.BaseStructure, $"{d.TrimmedName}.ArithMult({dInt.TrimmedName})");
            return newDensity;
        }

        // // TODO Add IEnumerable interface to Density?
        // public static IEnumerator<KeyValuePair<M,PType>> GetEnumerator<G,M>(this Density<G,M> d)
        //     where G :
        //         IBaseStructure<M>,
        //         IComparer<M>
        // { }
        // public static IEnumerator IEnumerable.GetEnumerator<G,M>(this Density<G,M> d)
        //     where G :
        //         IBaseStructure<M>,
        //         IComparer<M>
        // {
        //     return d.GetEnumerator();
        // }

        public static MP Expected<MP,RP>(this Density<IRealVectorspace<MP,RP>,MP> d)
        {
            var gp = d.BaseStructure;
            var grp = gp.BaseRealField;
            //TODO: Instead values should lie in a probability field (not PType)...
            var expected = gp.Sum(d.ToDictionary().Select(p => gp.ScalarMult(grp.EmbedFromReal(p.Value), p.Key)));
            return expected;
        }

        public static RP Variance<MP,RP>(this Density<IRealVectorspace<MP,RP>,MP> d)
        {
            var gp = d.BaseStructure;
            var grp = gp.BaseRealField;
            var expected = d.Expected<MP,RP>();
            Func<MP, RP> normFunc = mp => grp.ScalarPow(gp.Norm(gp.Subtract(expected, mp)), 2);
            //TODO: Instead values should lie in a probability field (not PType)...
            var squaredDistances = d.ToDictionary()
                .Select(p => grp.Multiply(grp.EmbedFromReal(p.Value), normFunc(p.Key)))
                .ToList();
            var variance = grp.Sum(squaredDistances);
            return variance;
        }
        public static RP Stdev<MP,RP>(this Density<IRealVectorspace<MP,RP>,MP> d)
        {
            var grp = d.BaseStructure.BaseRealField;
            return grp.Sqrt(d.Variance<MP,RP>());
        }
        public static PType Cdf<G,M>(this Density<G,M> d, PType x)
            where G :
                IBaseStructure<M>,
                IComparer<M>,
                IRealEmbedding<M, PType>
        {
            var g = d.BaseStructure;
            var pf = Density<G,M>.PField;
            return d.Prob(a => pf.Compare(g.EmbedToReal(a), x) <= 0);
        }

        public static MP Expected<M,R,MP,RP>(this Density<IModuleWithExtension<M,R,MP,RP>,M> d)
        {
            var g = d.BaseStructure;
            var embeddedDensity = d.EmbedTo<M,R,MP,RP>();
            return embeddedDensity.Expected();
        }
        public static RP VarianceEmbedded<M,R,MP,RP>(this Density<IModuleWithExtension<M,R,MP,RP>,M> d)
        {
            var embeddedDensity = d.EmbedTo<M,R,MP,RP>();
            return embeddedDensity.Variance<MP,RP>();
        }
        public static RP StdevEmbedded<M,R,MP,RP>(this Density<IModuleWithExtension<M,R,MP,RP>,M> d)
        {
            var embeddedDensity = d.EmbedTo<M,R,MP,RP>();
            return embeddedDensity.Stdev<MP,RP>();
        }

        public static MultiDensity<G,M> AsMultiDensity<G,M>(this Density<G,M> d, int n)
            where G :
                IAdditiveMonoid<M>
        {
            var dList = Enumerable.Repeat(d, n).ToList();
            var multiDensity = new MultiDensity<G,M>(dList);
            return multiDensity;
        }

        public static Density<G,M> WithAdvantage<G,M>(this Density<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
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
                return Density<G,M>.BinaryOp<G,M>(d.BaseStructure, d, d, (a,b) => g.Max(a,b), (s,t) => $"a{s}");
            }
            var mDensity = d.AsMultiDensity(n).MultiOp<G,M>(d.BaseStructure, e => g.Max(e), e => $"a{n}{e.First()}");
            return mDensity;
        }

        public static Density<G,M> WithDisadvantage<G,M>(this Density<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
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
                return Density<G,M>.BinaryOp<G,M>(d.BaseStructure, d, d, (a,b) => g.Min(a,b), (s,t) => $"d{s}");
            }
            var mDensity = d.AsMultiDensity(n).MultiOp<G,M>(d.BaseStructure, e => g.Min(e), e => $"d{n}{e.First()}");
            return mDensity;
        }
    }
}