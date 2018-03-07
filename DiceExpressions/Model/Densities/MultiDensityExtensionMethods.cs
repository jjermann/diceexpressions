using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public static class MultiDensityExtensionMethods
    {
        public static Density<G,M> KeepHighest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            if (n<0)
            {
                return d.KeepLowest(-n);
            }
            Density<G,M> resDensity;
            if (n == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 1)
            {
                resDensity = d.MultiOp<G,M>(g, en => g.Max(en));
                resDensity.Name = d.Name + $".KeepHighest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g, en => SumNLargest<G,M>(g, en, n));
                resDensity.Name = d.Name + $".KeepHighest({n})";
            }
            return resDensity;
        }

        public static Density<G,M> KeepLowest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            if (n<0)
            {
                return d.KeepHighest(-n);
            }
            Density<G,M> resDensity;
            if (n == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 1)
            {
                resDensity = d.MultiOp<G,M>(g, en => g.Min(en));
                resDensity.Name = d.Name + $".KeepLowest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g, en => SumNSmallest<G,M>(g, en, n));
                resDensity.Name = d.Name + $".KeepLowest({n})";
            }
            return resDensity;
        }

        public static Density<G,M> DropHighest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            var remaining = Math.Min(0, d.DensityList.Count() - n);
            if (n<0)
            {
                return d.DropLowest(-n);
            }
            Density<G,M> resDensity;
            if (remaining == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 0)
            {
                resDensity = d.AsSummedDensity();
                resDensity.Name = d.Name + $".DropHighest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g, en => SumNSmallest<G,M>(g, en, remaining));
                resDensity.Name = d.Name + $".DropHighest({n})";
            }
            return resDensity;
        }

        public static Density<G,M> DropLowest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var g = d.BaseStructure;
            var remaining = Math.Min(0, d.DensityList.Count() - n);
            if (n<0)
            {
                return d.DropHighest(-n);
            }
            Density<G,M> resDensity;
            if (remaining == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 0)
            {
                resDensity = d.AsSummedDensity();
                resDensity.Name = d.Name + $".DropLowest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g,en => SumNLargest<G,M>(g, en, remaining));
                resDensity.Name = d.Name + $".DropLowest({n})";
            }
            return resDensity;
        }

        //TODO: These should be part of AlgebraicExtensionMethods!
        //TODO: It's not proper to use Density<G,M>.AlgebraicStructure here but we want to avoid multiple calls of new()
        private static M SumNLargest<G,M>(G baseStructure, IEnumerable<M> source, int n)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var nlargest = baseStructure.GetNLargest(source, n);
            return baseStructure.Sum(nlargest);
        }

       private static M SumNSmallest<G,M>(G baseStructure, IEnumerable<M> source, int n)
            where G :
                IAdditiveMonoid<M>,
                IComparer<M>
        {
            var nsmallest = baseStructure.GetNSmallest(source, n);
            return baseStructure.Sum(nsmallest);
        }
    }
}