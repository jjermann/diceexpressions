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
        public static IDensity<G,M> KeepHighest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>
        {
            var g = d.BaseStructure;
            if (n<0)
            {
                return d.KeepLowest(-n);
            }
            IDensity<G,M> resDensity;
            if (n == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 1)
            {
                resDensity = d.MultiOp<G,M>(g, en => g.Max(en));
                resDensity.Name = d.Name + $".KeepHighest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g, en => g.SumNLargest(en, n));
                resDensity.Name = d.Name + $".KeepHighest({n})";
            }
            return resDensity;
        }

        public static IDensity<G,M> KeepLowest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>
        {
            var g = d.BaseStructure;
            if (n<0)
            {
                return d.KeepHighest(-n);
            }
            IDensity<G,M> resDensity;
            if (n == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 1)
            {
                resDensity = d.MultiOp<G,M>(g, en => g.Min(en));
                resDensity.Name = d.Name + $".KeepLowest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g, en => g.SumNSmallest(en, n));
                resDensity.Name = d.Name + $".KeepLowest({n})";
            }
            return resDensity;
        }

        public static IDensity<G,M> DropHighest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>
        {
            var g = d.BaseStructure;
            var remaining = Math.Min(0, d.DensityList.Count() - n);
            if (n<0)
            {
                return d.DropLowest(-n);
            }
            IDensity<G,M> resDensity;
            if (remaining == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 0)
            {
                resDensity = d.AsSummedDensity();
                resDensity.Name = d.Name + $".DropHighest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g, en => g.SumNSmallest(en, remaining));
                resDensity.Name = d.Name + $".DropHighest({n})";
            }
            return resDensity;
        }

        public static IDensity<G,M> DropLowest<G,M>(this MultiDensity<G,M> d, int n = 1)
            where G :
                IAdditiveMonoid<M>
        {
            var g = d.BaseStructure;
            var remaining = Math.Min(0, d.DensityList.Count() - n);
            if (n<0)
            {
                return d.DropHighest(-n);
            }
            IDensity<G,M> resDensity;
            if (remaining == 0)
            {
                resDensity = new Zero<G,M>(g);
            } else if (n == 0)
            {
                resDensity = d.AsSummedDensity();
                resDensity.Name = d.Name + $".DropLowest({n})";
            } else
            {
                resDensity = d.MultiOp<G,M>(g,en => g.SumNLargest(en, remaining));
                resDensity.Name = d.Name + $".DropLowest({n})";
            }
            return resDensity;
        }
    }
}