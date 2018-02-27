using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model;
using PType = System.Double;

namespace DiceExpressions.ModelHelpers
{
    //TODO: Do we need DensityExpressionGrammarBaseListener?
    public abstract class DensityVisitor<T> : DensityExpressionGrammarBaseListener
    {
        virtual public DensityExpressionResult<T> VisitCompileUnit(DensityExpressionGrammarParser.CompileUnitContext ctx)
        {
            var isDensity = ctx.density() != null;
            var isProbability = ctx.probability() != null;
            var density = isDensity
                ? VisitDensity(ctx.density())
                : null;
            var probability = isProbability
                ? VisitProbability(ctx.probability())
                : (PType?)null;
            var errorString = (isDensity || isProbability)
                ? null
                : "No valid Density or Probability!";

            var res = new DensityExpressionResult<T>
            {
                Density = density,
                Probability = probability,
                ErrorString = errorString
            };

            return res;
        }

        virtual public PType VisitProbability(DensityExpressionGrammarParser.ProbabilityContext ctx)
        {
            var leftDensity = VisitDensity(ctx.density(0));
            var rightDensity = VisitDensity(ctx.density(1));
            var opCtx = ctx.binaryBooleanOp();
            if (opCtx.EQ() != null)
            {
                return leftDensity == rightDensity;
            } else if (opCtx.NEQ() != null)
            {
                return leftDensity != rightDensity;
            } else if (opCtx.LT() != null)
            {
                return leftDensity < rightDensity;
            } else if (opCtx.LE() != null)
            {
                return leftDensity <= rightDensity;
            } else if (opCtx.GT() != null)
            {
                return leftDensity > rightDensity;
            } else if (opCtx.GE() != null)
            {
                return leftDensity >= rightDensity;
            } else
            {
                throw new NotImplementedException();
            }
        }

        virtual public Density<T> VisitDensity(DensityExpressionGrammarParser.DensityContext ctx)
        {
            var isFunction = false;
            // var isFunction = ctx.functionName() != null;
            if (isFunction)
            {
                throw new NotImplementedException();
                // var functionName = ctx.functionName().GetText();
                // var functionArguments = VisitFunctionArguments(ctx.functionArguments());
                // return ParseDensityFunction(functionName, functionArguments.ToArray());
            } else
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
        }

        virtual public MultiDensity<T> VisitMultiDensity(DensityExpressionGrammarParser.MultiDensityListContext ctx)
        {
            var dList = ctx.density().Select(c => VisitDensity(c)).ToList();
            var multiDensity = new MultiDensity<T>(dList);
            return multiDensity;
        }

        // public List<string> VisitFunctionArguments(DensityExpressionGrammarParser.FunctionArgumentsContext ctx)
        // {
        //     var argumentList = new List<string>();
        //     if (ctx != null)
        //     {
        //         for (var i = 0; i < ctx.strVariable().Length; i++) {
        //             var tmpArgument = ctx.strVariable(i).GetText();
        //             argumentList.Add(tmpArgument);
        //         }
        //     }
        //     return argumentList;
        // }

        virtual public Density<T> VisitTerm(DensityExpressionGrammarParser.TermContext ctx)
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

        virtual public Density<T> VisitFactor(DensityExpressionGrammarParser.FactorContext ctx)
        {
            var isAtom = ctx.atom() != null;
            var isMinus = ctx.MINUS() != null;
            var density = isAtom
                ? VisitAtom(ctx.atom())
                : -VisitFactor(ctx.factor());
            return density;
        }

        virtual public Density<T> VisitAtom(DensityExpressionGrammarParser.AtomContext ctx)
        {
            var isNumber = ctx.number() != null;
            var isVariable = ctx.variable() != null;
            var isDensity = ctx.LPAREN() != null;
            var isMultiDensity = ctx.multiDensityList() != null;

            Density<T> density;
            if (isNumber)
            {
                var num = VisitNumber(ctx.number());
                density = new Density<T>(num);
            } else if (isVariable)
            {
                var vDensity = VisitVariable(ctx.variable());
                density = vDensity;
            } else if (isDensity)
            {
                density = VisitDensity(ctx.density());
            } else if (isMultiDensity)
            {
                density = VisitMultiDensity(ctx.multiDensityList());
            } else
            {
                throw new NotImplementedException();
            }
            return density;
        }

        abstract public T VisitNumber(DensityExpressionGrammarParser.NumberContext ctx);

        abstract public Density<T> VisitVariable(DensityExpressionGrammarParser.VariableContext ctx);

        // abstract public Density<T> ParseDensityFunction(string functionName, params string[] functionArguments);
    }
}