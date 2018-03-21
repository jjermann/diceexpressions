using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.Densities
{
    public static class DensityExtensionMethods
    {
        public static string GetTrimmedName<G,M,RF>(this IDensity<G,M,RF> d)
        {
            var trimmedName = d.Name;
            while(trimmedName.StartsWith("(") && trimmedName.EndsWith(")"))
            {
                trimmedName = trimmedName.Substring(1, Math.Max(1, trimmedName.Length-2));
            }
            return trimmedName;
        }
        public static IList<M> GetKeys<G,M,RF>(this IDensity<G,M,RF> d)
        {
            return d.Dictionary.Keys.ToList();
        }
        public static IList<RF> GetValues<G,M,RF>(this IDensity<G,M,RF> d)
        {
            return d.GetKeys().Select(k => d.Dictionary[k]).ToList();
        }

        public static IDensity<S, N, RF> Op<G, M, S, N, RF>(this IDensity<G,M,RF> d,
            S newBaseStructure,
            Func<M, N> op,
            Func<string, string> opStrFunc = null)
        {
            return Op(d, newBaseStructure, op, opStrFunc);
        }
        public static RF Prob<G,M,RF>(this IDensity<G,M,RF> d, Func<M, bool> cond)
        {
            return Prob(d, cond);
        }
        public static IDensity<G, M, RF> ConditionalDensity<G,M,RF>(this IDensity<G,M,RF> d,
            Func<M, bool> cond,
            Func<string,string> condStrFunc = null)
        {
            return ConditionalDensity(d, cond, condStrFunc);
        }

        public static IDensity<G,M,RF> Negate<G,M,RF>(this IDensity<G,M,RF> d)
            where G :
                IAdditiveGroup<M>
        {
            var g = d.BaseStructure;
            return d.Op(g, a => g.Negate(a), s => $"(-{s})");
        }

        public static IDensity<G,M,RF> Add<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
               IAdditiveMonoid<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Add(a, b), (s,t) => $"({s}+{t})");
        }
        public static IDensity<G,M,RF> Subtract<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IAdditiveGroup<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Subtract(a, b), (s,t) => $"({s}-{t})");
        }
        public static IDensity<G,M,RF> Multiply<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IMultiplicativeMonoid<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Multiply(a, b), (s,t) => $"({s}*{t})");
        }
        public static IDensity<G,M,RF> Divide<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IMultiplicativeGroup<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryOp<G,M>(g, d, d2, (a,b) => g.Divide(a, b), (s,t) => $"({s}/{t})");
        }

        public static RF EqProb<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IBaseStructure<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryProb(d, d2, (a,b) => g.Equals(a, b));
        }
        public static RF NeqProb<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IBaseStructure<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryProb(d, d2, (a,b) => !g.Equals(a, b));
        }
        public static RF LtProb<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) < 0);
        }
        public static RF GtProb<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) > 0);
        }
        public static RF LeqProb<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) <= 0);
        }
        public static RF GeqProb<G,M,RF>(this IDensity<G,M,RF> d, IDensity<G,M,RF> d2)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            return Density<G,M,RF>.BinaryProb(d, d2, (a,b) => g.Compare(a, b) >= 0);
        }

        public static IList<M> SortedKeys<G,M,RF>(this IDensity<G,M,RF> d)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            return d.GetKeys().OrderBy(k => k, d.BaseStructure).ToList();
        }

        public static IList<RF> SortedValues<G,M,RF>(this IDensity<G,M,RF> d)
            where G :
                IBaseStructure<M>,
                IComparer<M>
        {
            return d.SortedKeys().Select(k => d[k]).ToList();
        }

        public static IDensity<G,M,RF> ArithMult<G,M,RF>(this IDensity<G,M,RF> d, int n)
            where G :
                IAdditiveMonoid<M>
        {
            if (n<0)
            {
                throw new ArgumentException();
            }
            if (n == 0)
            {
                return new Zero<G,M,RF>(d.BaseStructure, d.RealField);
            }
            var newDensity = d;
            foreach(var step in Enumerable.Range(1, n-1))
            {
                newDensity = newDensity.Add(d);
            }
            newDensity.Name = $"({d.GetTrimmedName()}).ArithMult({n})";
            return newDensity;
        }

        public static IDensity<G,M,RF> ArithMult<G,M,RF>(this IDensity<G,M,RF> d, Density<int> dInt)
            where G :
                IAdditiveMonoid<M>
        {
            var resDict = new Dictionary<M, RF>();
            var rf = d.RealField;
            foreach (var key in dInt.GetKeys())
            {
                var mulDensity = d.ArithMult(key);
                foreach (var mulKey in mulDensity.GetKeys())
                {
                    if (!resDict.ContainsKey(mulKey))
                    {
                        resDict[mulKey] = default(RF);
                    }
                    resDict[mulKey] = rf.Add(resDict[mulKey], rf.Multiply(mulDensity[mulKey],rf.EmbedFromReal(dInt[key])));
                }
            }
            var newDensity = new Density<G,M,RF>(resDict, d.BaseStructure, d.RealField, $"{d.GetTrimmedName()}.ArithMult({dInt.GetTrimmedName()})");
            return newDensity;
        }

        public static MP Expected<MP,RF>(this IDensity<IRealVectorspace<MP,RF>,MP,RF> d)
        {
            var gp = d.BaseStructure;
            var grp = gp.BaseRealField;
            var expected = gp.Sum(d.Dictionary.Select(p => gp.ScalarMult(p.Value, p.Key)));
            return expected;
        }

        public static RF Variance<MP,RF>(this IDensity<IRealVectorspace<MP,RF>,MP,RF> d)
        {
            var gp = d.BaseStructure;
            var grp = gp.BaseRealField;
            var expected = d.Expected<MP,RF>();
            Func<MP, RF> normFunc = mp => grp.ScalarPow(gp.Norm(gp.Subtract(expected, mp)), 2);
            var squaredDistances = d.Dictionary
                .Select(p => grp.Multiply(p.Value, normFunc(p.Key)))
                .ToList();
            var variance = grp.Sum(squaredDistances);
            return variance;
        }
        public static RF Stdev<MP,RF>(this IDensity<IRealVectorspace<MP,RF>,MP,RF> d)
        {
            var grp = d.BaseStructure.BaseRealField;
            return grp.Sqrt(d.Variance<MP,RF>());
        }
        public static RF Cdf<G,M,RF>(this IDensity<G,M,RF> d, RF x)
            where G :
                IBaseStructure<M>,
                IComparer<M>,
                IRealEmbedding<M,RF>
        {
            var g = d.BaseStructure;
            var rf = d.RealField;
            return d.Prob(a => rf.Compare(g.EmbedToReal(a), x) <= 0);
        }

        public static MP Expected<M,R,MP,RF>(this IDensity<IModuleWithExtension<M,R,MP,RF>,M,RF> d)
        {
            var g = d.BaseStructure;
            var embeddedDensity = d.EmbedTo<M,R,MP,RF>();
            return embeddedDensity.Expected();
        }
        public static RF VarianceEmbedded<M,R,MP,RF>(this IDensity<IModuleWithExtension<M,R,MP,RF>,M,RF> d)
        {
            var embeddedDensity = d.EmbedTo<M,R,MP,RF>();
            return embeddedDensity.Variance<MP,RF>();
        }
        public static RF StdevEmbedded<M,R,MP,RF>(this IDensity<IModuleWithExtension<M,R,MP,RF>,M,RF> d)
        {
            var embeddedDensity = d.EmbedTo<M,R,MP,RF>();
            return embeddedDensity.Stdev<MP,RF>();
        }

        public static MultiDensity<G,M,RF> AsMultiDensity<G,M,RF>(this IDensity<G,M,RF> d, int n)
            where G :
                IAdditiveMonoid<M>
        {
            var dList = Enumerable.Repeat(d, n).ToList();
            var multiDensity = new MultiDensity<G,M,RF>(dList);
            return multiDensity;
        }

        public static IDensity<G,M,RF> WithAdvantage<G,M,RF>(this IDensity<G,M,RF> d, int n = 1)
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
                return Density<G,M,RF>.BinaryOp<G,M>(d.BaseStructure, d, d, (a,b) => g.Max(a,b), (s,t) => $"a{s}");
            }
            var mDensity = d.AsMultiDensity(n).MultiOp<G,M>(d.BaseStructure, e => g.Max(e), e => $"a{n}{e.First()}");
            return mDensity;
        }

        public static IDensity<G,M,RF> WithDisadvantage<G,M,RF>(this IDensity<G,M,RF> d, int n = 1)
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
                return Density<G,M,RF>.BinaryOp<G,M>(d.BaseStructure, d, d, (a,b) => g.Min(a,b), (s,t) => $"d{s}");
            }
            var mDensity = d.AsMultiDensity(n).MultiOp<G,M>(d.BaseStructure, e => g.Min(e), e => $"d{n}{e.First()}");
            return mDensity;
        }

        public static IDensity<IRealVectorspace<MP,RF>,MP,RF> EmbedTo<M,R,MP,RF>(this IDensity<IModuleWithExtension<M,R,MP,RF>,M,RF> d)
        {
            var g = d.BaseStructure;
            var resDict = d.Dictionary.ToDictionary(
                x => g.ModuleEmbedding(x.Key),
                x => x.Value
            );
            var resDensity = new Density<IRealVectorspace<MP,RF>,MP,RF>(resDict, g.ExtensionVectorspace, d.RealField, d.Name);
            return resDensity;
        }
    }
}