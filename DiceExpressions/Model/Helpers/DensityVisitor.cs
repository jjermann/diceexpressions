using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.Densities;
using PType = System.Double;

namespace DiceExpressions.Model.Helpers
{
    //Remark: Base class DensityExpressionGrammarBaseListener was removed
    public abstract class DensityVisitor<G,M>
        where G :
            IAdditiveGroup<M>,
            IMultiplicativeGroup<M>,
            IComparer<M>,
            new()
    {
        virtual public DensityExpressionResult<G,M> VisitCompileUnit(DensityExpressionGrammarParser.CompileUnitContext ctx)
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

            var res = new DensityExpressionResult<G,M>
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
                return leftDensity.EqProb(rightDensity);
            } else if (opCtx.NEQ() != null)
            {
                return leftDensity.NeqProb(rightDensity);
            } else if (opCtx.LT() != null)
            {
                return leftDensity.LtProb(rightDensity);
            } else if (opCtx.LE() != null)
            {
                return leftDensity.LeqProb(rightDensity);
            } else if (opCtx.GT() != null)
            {
                return leftDensity.GtProb(rightDensity);
            } else if (opCtx.GE() != null)
            {
                return leftDensity.GeqProb(rightDensity);
            } else
            {
                throw new NotImplementedException();
            }
        }

        virtual public Density<G,M> VisitDensity(DensityExpressionGrammarParser.DensityContext ctx)
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
                        density = density.Add(tmpDensity);
                    } else if (isMinus)
                    {
                        density = density.Subtract(tmpDensity);
                    } else
                    {
                        throw new NotImplementedException();
                    }
                }
                return density;
            }
        }

        virtual public MultiDensity<G,M> VisitMultiDensity(DensityExpressionGrammarParser.MultiDensityListContext ctx)
        {
            var dList = ctx.density().Select(c => VisitDensity(c)).ToList();
            var multiDensity = new MultiDensity<G,M>(dList);
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

        virtual public Density<G,M> VisitTerm(DensityExpressionGrammarParser.TermContext ctx)
        {
            var density = VisitFactor(ctx.factor(0));
            for (var i = 1; i < ctx.factor().Length; i++)
            {
                var isMult = ctx.TIMES(i-1) != null;
                var isDiv = ctx.DIV(i-1) != null;
                var tmpDensity = VisitFactor(ctx.factor(i));
                if (isMult)
                {
                    density = density.Multiply(tmpDensity);
                } else if (isDiv)
                {
                    density = density.Divide(tmpDensity);
                } else
                {
                    throw new NotImplementedException();
                }
            }
            return density;
        }

        virtual public Density<G,M> VisitFactor(DensityExpressionGrammarParser.FactorContext ctx)
        {
            var isAtom = ctx.atom() != null;
            var isMinus = ctx.MINUS() != null;
            var density = isAtom
                ? VisitAtom(ctx.atom())
                : VisitFactor(ctx.factor()).Negate();
            return density;
        }

        virtual public Density<G,M> VisitAtom(DensityExpressionGrammarParser.AtomContext ctx)
        {
            var isNumber = ctx.number() != null;
            var isVariable = ctx.variable() != null;
            var isDensity = ctx.LPAREN() != null;
            var isMultiDensity = ctx.multiDensityList() != null;

            Density<G,M> density;
            if (isNumber)
            {
                var num = VisitNumber(ctx.number());
                density = new Constant<G,M>(num);
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

        abstract public M VisitNumber(DensityExpressionGrammarParser.NumberContext ctx);

        abstract public Density<G,M> VisitVariable(DensityExpressionGrammarParser.VariableContext ctx);

        // abstract public Density<G,M> ParseDensityFunction(string functionName, params string[] functionArguments);
    }
}