using System;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.AlgebraicStructureHelper
{
    public static class StructureHelperExtensionMethods
    {
        public static Func<R,R> FromPTypeOp<R>(this IProbabilityField<R> F, Func<PType, PType> f)
        {
            return r => F.EmbedFrom(f(F.EmbedTo(r)));
        }

        public static Func<R,R,R> FromPTypeBinaryOp<R>(this IProbabilityField<R> F, Func<PType, PType, PType> f)
        {
            return (r,s) => F.EmbedFrom(f(F.EmbedTo(r), F.EmbedTo(s)));
        }

        public static Func<PType,PType> ToPTypeOp<R>(this IProbabilityField<R> F, Func<R, R> f)
        {
            return r => F.EmbedTo(f(F.EmbedFrom(r)));
        }

        public static Func<PType,PType,PType> ToPTypeBinaryOp<R>(this IProbabilityField<R> F, Func<R, R, R> f)
        {
            return (r,s) => F.EmbedTo(f(F.EmbedFrom(r), F.EmbedFrom(s)));
        }

        public static R Sqrt<R>(this IProbabilityField<R> F, R r)
        {
            var f = F.FromPTypeOp(Math.Sqrt);
            return f(r);
        }

        public static R Abs<R>(this IProbabilityField<R> F, R r)
        {
            return F.Compare(r, F.Zero()) >= 0
                ? r
                : F.Negate(r);
        }

        public static R ScalarPow<R>(this IProbabilityField<R> F, R r, R pow)
        {
            var f = F.FromPTypeBinaryOp(Math.Pow);
            return f(r, pow);
        }

        public static int Round<R>(this IProbabilityField<R> F, R r)
        {
            return (int)Math.Round(F.EmbedTo(r));
        }
    }
}