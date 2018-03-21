using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.Densities
{
    public partial class Density<G,M,RF>
    {
        private static string DefaultName => "Density";

        protected static IDictionary<N, RF> GetOpDictionary<N>(
            IDensity<G, M, RF> d,
            Func<M, N> op)
        {
            var resDensity = new Dictionary<N, RF>();
            var rf = d.RealField;
            foreach (var key in d.GetKeys())
            {
                var resKey = op(key);
                if (!resDensity.ContainsKey(resKey))
                {
                    resDensity[resKey] = default(RF);
                }
                resDensity[resKey] = rf.Add(resDensity[resKey], d[key]);
            }
            return resDensity;
        }
        public static IDensity<S, N, RF> Op<S, N>(
            IDensity<G,M,RF> d,
            S newBaseStructure,
            Func<M, N> op,
            Func<string, string> opStrFunc = null)
        {
            var resDensity = GetOpDictionary(d, op);
            var name = opStrFunc == null
                ? DefaultName
                : opStrFunc(d.Name);
            return new Density<S, N, RF>(resDensity, newBaseStructure, d.RealField, name);
        }
        protected static IDictionary<N, RF> GetBinaryOpDictionary<N>(
            IDensity<G, M, RF> d1,
            IDensity<G, M, RF> d2,
            Func<M, M, N> op)
        {
            var resDensity = new Dictionary<N, RF>();
            var rf = d1.RealField;
            foreach (var key1 in d1.GetKeys())
            {
                foreach (var key2 in d2.GetKeys())
                {
                    var resKey = op(key1, key2);
                    if (!resDensity.ContainsKey(resKey))
                    {
                        resDensity[resKey] = default(RF);
                    }
                    resDensity[resKey] = rf.Add(resDensity[resKey], rf.Multiply(d1[key1],d2[key2]));
                }
            }
            return resDensity;
        }
        public static IDensity<S, N, RF> BinaryOp<S, N>(
            S newBaseStructure,
            IDensity<G, M, RF> d1,
            IDensity<G, M, RF> d2,
            Func<M, M, N> op, 
            Func<string,string,string> binOpStrFunc = null)
            where S :
                IBaseStructure<N>
        {
            var resDensity = GetBinaryOpDictionary(d1, d2, op);
            var name = binOpStrFunc == null
                ? DefaultName
                : binOpStrFunc(d1.Name, d2.Name);
            return new Density<S, N, RF>(resDensity, newBaseStructure, d1.RealField, name);
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
        protected static IDictionary<N, RF> GetMultiOpDictionary<N>(
            IEnumerable<IDensity<G, M, RF>> densityList,
            Func<IEnumerable<M>,N> multiOp)
        {
            var realField = densityList.First().RealField;
            var dListList = densityList.Select(d => d.Dictionary.ToList());
            var resDensity = new Dictionary<N, RF>();
            var productList = CartesianProduct(dListList);

            foreach (var product in productList)
            {
                var multiOpArgument = product.Select(p => p.Key).ToList();
                var resultKey = multiOp(multiOpArgument);
                var resultValue = realField.Product(product.Select(p => p.Value));
                if (!resDensity.ContainsKey(resultKey))
                {
                    resDensity[resultKey] = default(RF);
                }
                resDensity[resultKey] = resultValue;
            }
            return resDensity;
        }
        public static IDensity<S, N, RF> MultiOp<S, N>(
            S newBaseStructure,
            IEnumerable<IDensity<G, M, RF>> densityList,
            Func<IEnumerable<M>,N> multiOp,
            Func<IEnumerable<string>,string> multiOpStrFunc = null)
            where S :
                IBaseStructure<N>
        {
            if (!densityList.Any())
            {
                throw new NotImplementedException();
            }
            var realField = densityList.First().RealField;
            var resDensity = GetMultiOpDictionary(densityList, multiOp);
            var name = multiOpStrFunc != null
                ? multiOpStrFunc(densityList.Select(d => d.Name))
                : DefaultName;
            return new Density<S, N, RF>(resDensity, newBaseStructure, realField, name);
        }
        protected static IDictionary<M, RF> GetConditionalDensityDictionary(
            IDensity<G, M, RF> d,
            Func<M, bool> cond)
        {
            var resDict = new Dictionary<M, RF>();
            var rf = d.RealField;
            foreach (var key in d.GetKeys().Where(cond))
            {
                resDict[key] = d[key];
            }
            var conditionalProbability = rf.Sum(resDict.Values);
            foreach (var key in resDict.Keys)
            {
                resDict[key] = rf.Divide(resDict[key], conditionalProbability);
            }
            return resDict;
        }
        public static IDensity<G, M, RF> ConditionalDensity(
            IDensity<G,M,RF> d,
            Func<M, bool> cond,
            Func<string,string> condStrFunc = null)
        {
            var resDict = GetConditionalDensityDictionary(d, cond);
            var name = condStrFunc != null
                ? condStrFunc(d.Name)
                : DefaultName;
            var newDensity = new Density<G, M, RF>(resDict, d.BaseStructure, d.RealField, name);
            return newDensity;
        }
        public static RF Prob(IDensity<G,M,RF> d, Func<M, bool> cond)
        {
            var rf = d.RealField;
            var resSum = rf.Sum(d.GetKeys().Where(cond).Select(k => d[k]));
            return resSum;
        }
        public static RF BinaryProb(IDensity<G, M, RF> d1, IDensity<G, M, RF> d2, Func<M, M, bool> cond)
        {
            var resSum = default(RF);
            var rf = d1.RealField;
            foreach (var key1 in d1.GetKeys())
            {
                foreach (var key2 in d2.GetKeys())
                {
                    if (cond(key1, key2))
                    {
                        resSum = rf.Add(resSum, rf.Multiply(d1[key1],d2[key2]));
                    }
                }
            }
            return resSum;
        }
    }
}