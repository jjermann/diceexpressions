using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public class MultiDensity<M> :
        MultiDensity<FieldType<M>, M>
    { 
        public MultiDensity(params Density<FieldType<M>,M>[] dArray) : base(dArray) { }
        public MultiDensity(IList<Density<FieldType<M>,M>> dList) : base(dList) { }
    }

    //TODO: For now we assume an additive monoid to be able to inherit from Density<G, M> (viewed as a sum of densities)
    //      But that's just one way to get a density, so this should be reduced to IEqualityComparer<M>.
    public class MultiDensity<G, M> :
        Density<G, M>
        where G :
            IAdditiveMonoid<M>
    {
        public IList<Density<G, M>> DensityList { get; private set; }

        public MultiDensity(params Density<G, M>[] dArray) : this(dArray.ToList()) { }
        public MultiDensity(IList<Density<G, M>> dList) : base(GetSummedMultiDensity(dList))
        {
            DensityList = dList;
            Name = GetMultiDensityName(dList);
        }

        public Density<G, M> AsSummedDensity()
        {
            return GetSummedMultiDensity(DensityList);
        }

        public Density<S,N> MultiOp<S,N>(
            S newBaseStructure,
            Func<IEnumerable<M>,N> multiOp, 
            Func<IEnumerable<string>,string> multiOpStrFunc = null)
            where S :
                IBaseStructure<N>
        {
            return Density<G,M>.MultiOp<S,N>(newBaseStructure, DensityList, multiOp, multiOpStrFunc);
        }

        protected static Density<G, M> GetSummedMultiDensity(IList<Density<G, M>> dList)
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

        protected static string GetMultiDensityName(IList<Density<G, M>> dList)
        {
            var multiName = "[" + string.Join(", ", dList.Select(d => d.Name)) + "]";
            return multiName;
        }
    }
}