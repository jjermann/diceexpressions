using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public partial class Density<G,M>
    {
        public static readonly RealFieldType<PType> PField = new RealFieldType<PType>(); 
        private static string DefaultName => "Density";

        protected static IDictionary<N, PType> GetOpDictionary<N>(
            IDensity<G, M> d,
            Func<M, N> op)
        {
            var resDensity = new Dictionary<N, PType>();
            foreach (var key in d.GetKeys())
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
        public static IDensity<S, N> Op<S, N>(
            IDensity<G,M> d,
            S newBaseStructure,
            Func<M, N> op,
            Func<string, string> opStrFunc = null)
        {
            var resDensity = GetOpDictionary(d, op);
            var name = opStrFunc == null
                ? DefaultName
                : opStrFunc(d.Name);
            return new Density<S, N>(resDensity, newBaseStructure, name);
        }
        protected static IDictionary<N, PType> GetBinaryOpDictionary<N>(
            IDensity<G, M> d1,
            IDensity<G, M> d2,
            Func<M, M, N> op)
        {
            var resDensity = new Dictionary<N, PType>();
            foreach (var key1 in d1.GetKeys())
            {
                foreach (var key2 in d2.GetKeys())
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
        public static IDensity<S, N> BinaryOp<S, N>(
            S newBaseStructure,
            IDensity<G, M> d1,
            IDensity<G, M> d2,
            Func<M, M, N> op, 
            Func<string,string,string> binOpStrFunc = null)
            where S :
                IBaseStructure<N>
        {
            var resDensity = GetBinaryOpDictionary(d1, d2, op);
            var name = binOpStrFunc == null
                ? DefaultName
                : binOpStrFunc(d1.Name, d2.Name);
            return new Density<S, N>(resDensity, newBaseStructure, name);
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
            IEnumerable<IDensity<G, M>> densityList,
            Func<IEnumerable<M>,N> multiOp)
        {
            var dListList = densityList.Select(d => d.Dictionary.ToList());
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
        public static IDensity<S, N> MultiOp<S, N>(
            S newBaseStructure,
            IEnumerable<IDensity<G, M>> densityList,
            Func<IEnumerable<M>,N> multiOp,
            Func<IEnumerable<string>,string> multiOpStrFunc = null)
            where S :
                IBaseStructure<N>
        {
            var resDensity = GetMultiOpDictionary(densityList, multiOp);
            var name = multiOpStrFunc != null
                ? multiOpStrFunc(densityList.Select(d => d.Name))
                : DefaultName;
            return new Density<S, N>(resDensity, newBaseStructure, name);
        }
        protected static IDictionary<M, PType> GetConditionalDensityDictionary(
            IDensity<G, M> d,
            Func<M, bool> cond)
        {
            var resDict = new Dictionary<M, PType>();
            foreach (var key in d.GetKeys().Where(cond))
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
        public static IDensity<G, M> ConditionalDensity(
            IDensity<G,M> d,
            Func<M, bool> cond,
            Func<string,string> condStrFunc = null)
        {
            var resDict = GetConditionalDensityDictionary(d, cond);
            var name = condStrFunc != null
                ? condStrFunc(d.Name)
                : DefaultName;
            var newDensity = new Density<G, M>(resDict, d.BaseStructure, name);
            return newDensity;
        }
        public static PType Prob(IDensity<G,M> d, Func<M, bool> cond)
        {
            var resSum = d.GetKeys().Where(cond).Select(k => d[k]).Sum();
            return resSum;
        }
        public static PType BinaryProb(IDensity<G, M> d1, IDensity<G, M> d2, Func<M, M, bool> cond)
        {
            var resSum = default(PType);
            foreach (var key1 in d1.GetKeys())
            {
                foreach (var key2 in d2.GetKeys())
                {
                    if (cond(key1, key2))
                    {
                        resSum += d1[key1]*d2[key2];
                    }
                }
            }
            return resSum;
        }
    }
}