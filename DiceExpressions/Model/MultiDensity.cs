using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Math;
using PType = System.Double;

namespace DiceExpressions.Model
{
    public class MultiDensity<T> : Density<T>
    {
        protected IList<Density<T>> DensityList { get; private set; }

        public MultiDensity(params Density<T>[] dArray) : this(dArray.ToList()) { }
        public MultiDensity(IList<Density<T>> dList) : base(GetSummedMultiDensity(dList))
        {
            DensityList = dList;
            Name = GetMultiDensityName(dList);
        }

        public Density<T> AsDensity()
        {
            return GetSummedMultiDensity(DensityList);
        }

        protected static Density<T> GetSummedMultiDensity(IList<Density<T>> dList)
        {
            var summedDensity = dList.Count > 0
                ? dList[0]
                : new Zero<T>();
            foreach (var density in dList.Skip(1)) {
                summedDensity += density;
            }
            summedDensity.Name = "(" + string.Join("+", dList.Select(d => d.Name)) + ")";
            return summedDensity;
        }

        protected static string GetMultiDensityName(IList<Density<T>> dList)
        {
            var multiName = "[" + string.Join(", ", dList.Select(d => d.Name)) + "]";
            return multiName;
        }

        public Density<U> MultiOp<U>(Func<IEnumerable<T>,U> multiOp, Func<IEnumerable<string>,string> multiOpStrFunc = null)
        {
            return Density<T>.MultiOp(DensityList, multiOp, multiOpStrFunc);
        }

        public Density<T> DropHighest(int n=1)
        {
            Density<T> resDensity;
            if (n<0)
            {
                throw new NotImplementedException();
            } else if (n == 0)
            {
                resDensity = AsDensity();
            } else if (n == 1)
            {
                resDensity = MultiOp<T>(en => GenericMath.Subtract(GenericMathExtension.Sum(en), GenericMathExtension.Max(en)));
            } else
            {
                resDensity = MultiOp<T>(en => GenericMath.Subtract(GenericMathExtension.Sum(en), GenericMathExtension.SumNLargest(en, n)));
            }
            resDensity.Name = Name + $".DropHighest({n})";
            return resDensity;
        }

        public Density<T> DropLowest(int n=1)
        {
            Density<T> resDensity;
            if (n<0)
            {
                throw new NotImplementedException();
            } else if (n == 0)
            {
                resDensity = AsDensity();
            } else if (n == 1)
            {
                resDensity = MultiOp<T>(en => GenericMath.Subtract(GenericMathExtension.Sum(en), GenericMathExtension.Min(en)));
            } else
            {
                resDensity = MultiOp<T>(en => GenericMath.Subtract(GenericMathExtension.Sum(en), GenericMathExtension.SumNSmallest(en, n)));
            }
            resDensity.Name = Name + $".DropLowest({n})";
            return resDensity;
        }

        public Density<T> KeepHighest(int n=1)
        {
            Density<T> resDensity;
            if (n<0)
            {
                throw new NotImplementedException();
            } else if (n == 0)
            {
                resDensity = AsDensity();
            } else if (n == 1)
            {
                resDensity = MultiOp<T>(en => GenericMathExtension.Max(en));
            } else
            {
                resDensity = MultiOp<T>(en => GenericMathExtension.SumNLargest(en, n));
            }
            resDensity.Name = Name + $".KeepHighest({n})";
            return resDensity;
        }

        public Density<T> KeepLowest(int n=1)
        {
            Density<T> resDensity;
            if (n<0)
            {
                throw new NotImplementedException();
            } else if (n == 0)
            {
                resDensity = AsDensity();
            } else if (n == 1)
            {
                resDensity = MultiOp<T>(en => GenericMathExtension.Min(en));
            } else
            {
                resDensity = MultiOp<T>(en => GenericMathExtension.SumNSmallest(en, n));
            }
            resDensity.Name = Name + $".KeepLowest({n})";
            return resDensity;
        }
    }
}