using System;
using System.Collections.Generic;
using System.Linq;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicStructure
{
    public static class AlgebraicExtensionMethods
    {
        public static M Subtract<M>(this IAdditiveGroup<M> g, M a, M b)
        {
            return g.Add(a, g.Negate(b));
        }
        public static M Divide<M>(this IMultiplicativeGroup<M> g, M a, M b)
        {
            return g.Multiply(a, g.Inverse(b));
        }
        public static R EmbedFrom<R>(this IRing<R> g, int a)
        {
            var res = g.Zero();
            var one = g.One();
            for (var i=0; i < Math.Abs(a); i++)
            {
                res = g.Add(res, one);
            }
            if (a < 0)
            {
                res = g.Negate(res);
            }
            return res;
        }

        public static M Sum<M>(this IAdditiveMonoid<M> g, params M[] source)
        {
            return g.Sum(source.ToList());
        }
        public static M Sum<M>(this IAdditiveMonoid<M> g, IEnumerable<M> source)
        {
            var sum = g.Zero();
            foreach (var value in source)
            {
                sum = g.Add(sum, value);
            }
            return sum;
        }
        public static M Max<M>(this IComparer<M> g, params M[] source)
        {
            return g.Max(source.ToList());
        }
        public static M Max<M>(this IComparer<M> g, IEnumerable<M> source)
        {
            if (!source.Any()) {
                throw new ArgumentException("No Maximum defined for an empty set!");
            }
            var max = source.First();
            foreach (var value in source.Skip(1))
            {
                if (g.Compare(value, max) > 0)
                {
                    max = value;
                }
            }

            return max;
        }
        public static M Min<M>(this IComparer<M> g, params M[] source)
        {
            return g.Min(source.ToList());
        }
        public static M Min<M>(this IComparer<M> g, IEnumerable<M> source)
        {
            if (!source.Any()) {
                throw new ArgumentException("No Minimum defined for an empty set!");
            }
            var min = source.First();
            foreach (var value in source.Skip(1))
            {
                if (g.Compare(value, min) < 0)
                {
                    min = value;
                }
            }

            return min;
        }
        
        //For this definition one should usually assume that IAdditiveMonoid<M> is abelian
        public static M ScalarMult<M>(this IAdditiveMonoid<M> g, int n, M a)
        {
            if (n < 0)
            {
                throw new ArgumentException($"n={n} < 0!");
            }
            var res = g.Zero();
            for (var i=0; i<n; i++)
            {
                res = g.Add(res, a);
            }
            return res;
        }

        public static M ScalarMult<M>(this IAdditiveAbelianGroup<M> g, int n, M a)
        {
            var res = (g as IAdditiveMonoid<M>).ScalarMult(Math.Abs(n), a);
            if (n < 0)
            {
                res = g.Negate(res);
            }
            return res;
        }

        public static M Average<M, R>(this IVectorspace<M, R> v, params M[] source)
        {
            return v.Average(source.ToList());
        }
        public static M Average<M, R>(this IVectorspace<M, R> v, IEnumerable<M> source)
        {
            var baseField = v.BaseField;
            var sum = v.Zero();
            var count = 0;
            foreach (var value in source)
            {
                sum = v.Add(sum, value);
                count++;
            }
            var embeddedCount = baseField.EmbedFrom(count);
            var scalar = baseField.Inverse(embeddedCount);
            return v.ScalarMult(scalar, sum);
        }

        public static M Product<M>(this IMultiplicativeMonoid<M> g, params M[] source)
        {
            return g.Product(source.ToList());
        }
        public static M Product<M>(this IMultiplicativeMonoid<M> g, IEnumerable<M> source)
        {
            var product = g.One();
            foreach (var value in source)
            {
                product = g.Multiply(product, value);
            }
            return product;
        }

        //For this definition one should usually assume that IMultiplicativeMonoid<M> is abelian
        public static M ScalarPow<M>(this IMultiplicativeMonoid<M> g, M a, int n)
        {
            if (n < 0)
            {
                throw new ArgumentException($"n={n} < 0!");
            }
            var res = g.One();
            for (var i=0; i<n; i++)
            {
                res = g.Multiply(res, a);
            }
            return res;
        }

        //For this definition one should usually assume that IMultiplicativeGroup<M> is abelian
        public static M ScalarPow<M>(this IMultiplicativeGroup<M> g, M a, int n)
        {
            var res = (g as IMultiplicativeMonoid<M>).ScalarPow(a, Math.Abs(n));
            if (n < 0)
            {
                res = g.Inverse(res);
            }
            return res;
        }

        public static IEnumerable<M> GetNLargest<M>(this IComparer<M> g, IEnumerable<M> source, int n)
        {
            if (n < 0)
            {
                return g.GetNSmallest(source, -n);
            }
            if (n == 0)
            {
                return Enumerable.Empty<M>();
            }
            var sourceList = source.ToList();
            n = Math.Min(sourceList.Count, n);
            var resSet = sourceList.Take(n).ToList();
            var currentMin = g.Min(resSet);

            foreach (var t in sourceList.Skip(n))
            {
                if (g.Compare(t,currentMin) <= 0)
                {
                    continue;
                }
                resSet.Remove(currentMin);
                resSet.Add(t);
                currentMin = g.Min(resSet);
            }
            return resSet.OrderByDescending(i => i, g);
        }

        public static IEnumerable<M> GetNSmallest<M>(this IComparer<M> g, IEnumerable<M> source, int n)
        {
            if (n < 0)
            {
                return g.GetNLargest(source, -n);
            }
            if (n == 0)
            {
                return Enumerable.Empty<M>();
            }
            var sourceList = source.ToList();
            n = Math.Min(sourceList.Count, n);
            var resSet = sourceList.Take(n).ToList();
            var currentMax = g.Max(resSet);

            foreach (var t in sourceList.Skip(n))
            {
                if (g.Compare(t,currentMax) >= 0)
                {
                    continue;
                }
                resSet.Remove(currentMax);
                resSet.Add(t);
                currentMax = g.Max(resSet);
            }
            return resSet.OrderBy(i => i, g);
        }

        //TODO: SumNLargest, SumNSmallest, Abs. But they require both IAdditiveMonoid<M> and also IComparer<M>...




        public static Func<R,R> FromRealOp<R>(this IRealField<R> F, Func<PType, PType> f)
        {
            return r => F.EmbedFromReal(f(F.EmbedToReal(r)));
        }
        public static Func<R,R,R> FromRealBinaryOp<R>(this IRealField<R> F, Func<PType, PType, PType> f)
        {
            return (r,s) => F.EmbedFromReal(f(F.EmbedToReal(r), F.EmbedToReal(s)));
        }
        public static Func<PType,PType> ToRealOp<R>(this IRealField<R> F, Func<R, R> f)
        {
            return r => F.EmbedToReal(f(F.EmbedFromReal(r)));
        }
        public static Func<PType,PType,PType> ToRealBinaryOp<R>(this IRealField<R> F, Func<R, R, R> f)
        {
            return (r,s) => F.EmbedToReal(f(F.EmbedFromReal(r), F.EmbedFromReal(s)));
        }

        public static R Sqrt<R>(this IRealField<R> F, R r)
        {
            var f = F.FromRealOp(Math.Sqrt);
            return f(r);
        }

        public static R Abs<R>(this IRealField<R> F, R r)
        {
            return F.Compare(r, F.Zero()) >= 0
                ? r
                : F.Negate(r);
        }

        public static R ScalarPow<R>(this IRealField<R> F, R r, R pow)
        {
            var f = F.FromRealBinaryOp(Math.Pow);
            return f(r, pow);
        }

        public static int Round<R>(this IRealField<R> F, R r)
        {
            return (int)Math.Round(F.EmbedToReal(r));
        }
    }
}