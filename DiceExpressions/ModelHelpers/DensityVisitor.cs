using System;
using DiceExpressions.Model;

namespace DiceExpressions.ModelHelpers
{
    public abstract class DensityVisitor<T> : DensityExpressionGrammarBaseListener
    {
        public Density<T> VisitCompileUnit(DensityExpressionGrammarParser.CompileUnitContext ctx)
        {
            var density = VisitExpression(ctx.expression(0));
            return density;
        }

        public Density<T> VisitExpression(DensityExpressionGrammarParser.ExpressionContext ctx)
        {
            var density = VisitTerm(ctx.term(0));
            for (var i = 1; i < ctx.term().Length; i++)
            {
                var isPlus = ctx.PLUS(i-1) != null;
                var isMinus = ctx.MINUS(i-1) != null;
                var tmpDensity = VisitTerm(ctx.term(i));
                if (isPlus)
                {
                    density += tmpDensity;
                } else if (isMinus)
                {
                    density -= tmpDensity;
                } else
                {
                    throw new NotImplementedException();
                }
            }
            return density;
        }

        public Density<T> VisitTerm(DensityExpressionGrammarParser.TermContext ctx)
        {
            var density = VisitFactor(ctx.factor(0));
            for (var i = 1; i < ctx.factor().Length; i++)
            {
                var isMult = ctx.TIMES(i-1) != null;
                var isDiv = ctx.DIV(i-1) != null;
                var tmpDensity = VisitFactor(ctx.factor(i));
                if (isMult)
                {
                    density *= tmpDensity;
                } else if (isDiv)
                {
                    density /= tmpDensity;
                } else
                {
                    throw new NotImplementedException();
                }
            }
            return density;
        }

        public Density<T> VisitFactor(DensityExpressionGrammarParser.FactorContext ctx)
        {
            var isAtom = ctx.atom() != null;
            var isPlus = ctx.PLUS() != null;
            var isMinus = ctx.MINUS() != null;
            var density = isAtom
                ? VisitAtom(ctx.atom())
                : (isPlus ? VisitFactor(ctx.factor()) : -VisitFactor(ctx.factor()));
            return density;
        }

        public Density<T> VisitAtom(DensityExpressionGrammarParser.AtomContext ctx)
        {
            var isNumber = ctx.number() != null;
            var isVariable = ctx.variable() != null;
            var isExpression = ctx.expression() != null;
            var density = isNumber
                ? VisitNumber(ctx.number())
                : (isVariable ? VisitVariable(ctx.variable()) : VisitExpression(ctx.expression()));
            return density;
        }

        abstract public Density<T> VisitNumber(DensityExpressionGrammarParser.NumberContext ctx);

        abstract public Density<T> VisitVariable(DensityExpressionGrammarParser.VariableContext ctx);
    }
}