using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public class MultiDensity<M> :
        MultiDensity<FieldType<M,PType>, M, RealFieldType<PType>>
    { 
        public MultiDensity(params IDensity<FieldType<M,PType>,M,RealFieldType<PType>>[] dArray) : base(dArray) { }
        public MultiDensity(IList<IDensity<FieldType<M,PType>,M,RealFieldType<PType>>> dList) : base(dList) { }
    }

    //TODO: For now we assume an additive monoid to be able to inherit from Density<G, M, RF> (viewed as a sum of densities)
    //      But that's just one way to get a density, so this should be reduced to IEqualityComparer<M>.
    public class MultiDensity<G, M, RF> :
        Density<G, M, RF>
        where G :
            IAdditiveMonoid<M>
    {
        public IList<IDensity<G, M, RF>> DensityList { get; private set; }

        public MultiDensity(params IDensity<G, M, RF>[] dArray) : this(dArray.ToList()) { }
        public MultiDensity(IList<IDensity<G, M, RF>> dList) : base(GetSummedMultiDensity(dList))
        {
            DensityList = dList;
            Name = GetMultiDensityName(dList);
        }

        public IDensity<G, M, RF> AsSummedDensity()
        {
            return GetSummedMultiDensity(DensityList);
        }

        public IDensity<S,N,RF> MultiOp<S,N>(
            S newBaseStructure,
            Func<IEnumerable<M>,N> multiOp, 
            Func<IEnumerable<string>,string> multiOpStrFunc = null)
            where S :
                IBaseStructure<N>
        {
            return Density<G,M,RF>.MultiOp<S,N>(newBaseStructure, DensityList, multiOp, multiOpStrFunc);
        }

        protected static IDensity<G, M, RF> GetSummedMultiDensity(IList<IDensity<G, M, RF>> dList)
        {
            if (!dList.Any())
            {
                throw new NotImplementedException();
            }
            var summedDensity = dList.First();
            var g = summedDensity.BaseStructure;
            foreach (var density in dList.Skip(1)) {
                summedDensity = summedDensity.Add(density);
            }
            summedDensity.Name = "(" + string.Join("+", dList.Select(d => d.Name)) + ")";
            return summedDensity;
        }

        protected static string GetMultiDensityName(IList<IDensity<G, M, RF>> dList)
        {
            var multiName = "[" + string.Join(", ", dList.Select(d => d.Name)) + "]";
            return multiName;
        }
    }
}