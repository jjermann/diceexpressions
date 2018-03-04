using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;

namespace DiceExpressions.Model.Helpers
{
    //TODO: For now we assume that R is a struct to be able to use R?
    public static class AsciiPlotter<M, F, R>
        where F :
            IProbabilityField<R>,
            new()
        where R :
            struct
    {
        private static readonly F PF = new F();
        private static R Delta => PF.EmbedFrom(1e-9);
        private static string FillChar => "█";
        private static string SepChar => "│";
        private static string VoidChar => " ";

        private static string GetPlotLine(R p, R minP, R maxP, int plotWidth)
        {
            var aboveMax = PF.Compare(PF.Subtract(p,maxP), Delta) > 0;
            var belowMin = PF.Compare(PF.Subtract(minP,p), Delta) > 0;
            var correctedZero = PF.Zero();
            correctedZero = PF.Max(correctedZero, minP);
            correctedZero = PF.Min(correctedZero, maxP);

            p = PF.Max(p, minP);
            p = PF.Min(p, maxP);

            var unroundedZeroPosition = PF.Divide(PF.ScalarMult(plotWidth, PF.Subtract(correctedZero,minP)), PF.Subtract(maxP, minP));
            var correctedZeroPosition = PF.Round(unroundedZeroPosition);
            var unroundedPosition = PF.Divide(PF.ScalarMult(plotWidth, PF.Subtract(p, minP)), PF.Subtract(maxP,minP));
            var position = PF.Round(unroundedPosition);

            var mainContent = string.Concat(Enumerable.Repeat(FillChar, position))
                + string.Concat(Enumerable.Repeat(VoidChar, plotWidth-position));
            if (correctedZeroPosition > 0 && correctedZeroPosition < plotWidth)
            {
                mainContent = mainContent.Substring(0, correctedZeroPosition)
                    + SepChar
                    + mainContent.Substring(correctedZeroPosition + 1);
            }
            var result = belowMin
                ? SepChar
                : FillChar;
            result += mainContent;
            result += aboveMax
                ? FillChar
                : SepChar;
            return result;
        }

        private static string GetCenteredPlotLine(R p, R minP, R maxP, int plotWidth)
        {
            var aboveMax = PF.Compare(PF.Subtract(p,maxP), Delta) > 0;
            var belowMin = PF.Compare(PF.Subtract(minP,p), Delta) > 0;
            var correctedZero = PF.Zero();
            correctedZero = PF.Max(correctedZero, minP);
            correctedZero = PF.Min(correctedZero, maxP);

            p = PF.Max(p, minP);
            p = PF.Min(p, maxP);

            var unroundedZeroPosition = PF.Divide(PF.ScalarMult(plotWidth, PF.Subtract(correctedZero,minP)), PF.Subtract(maxP,minP));
            var correctedZeroPosition = PF.Round(unroundedZeroPosition);
            var unroundedPosition = PF.Divide(PF.ScalarMult(plotWidth, PF.Subtract(p,minP)),PF.Subtract(maxP,minP));
            var position = PF.Round(unroundedPosition);

            var relPosition = position-correctedZeroPosition;
            var mainContent = string.Concat(Enumerable.Repeat(FillChar, plotWidth));
            if (relPosition >= 0)
            {
                mainContent = mainContent.Substring(0, correctedZeroPosition)
                    + string.Concat(Enumerable.Repeat(FillChar, relPosition))
                    + mainContent.Substring(correctedZeroPosition + relPosition);
            } else
            {
                mainContent = mainContent.Substring(0, correctedZeroPosition + relPosition)
                    + string.Concat(Enumerable.Repeat(FillChar, -relPosition))
                    + mainContent.Substring(correctedZeroPosition);
            }

            if (correctedZeroPosition > 0 && correctedZeroPosition < plotWidth)
            {
                mainContent = mainContent.Substring(0, correctedZeroPosition)
                    + SepChar
                    + mainContent.Substring(correctedZeroPosition + 1);
            }
            var result = belowMin || PF.Compare(correctedZero, PF.Zero()) > 0
                ? FillChar
                : SepChar;
            result += mainContent;
            result += aboveMax || PF.Compare(correctedZero, PF.Zero()) < 0
                ? FillChar
                : SepChar;
            return result;
        }

        public static string GetPlot(
            Func<M, R> f,
            IEnumerable<M> inputs,
            int plotWidth = 50,
            R? minP = null,
            R? maxP = null,
            bool asPercentage = false,
            bool centered = true)
        {
            var setMinP = minP ?? PF.Min(inputs.Select(k => f(k)));
            var setMaxP = maxP ?? PF.Max(inputs.Select(k => f(k)));
            var formatString = asPercentage
                ? "{0:>12}\t{1:>12.2%}\t{2}"
                : "{0:>12}\t{1:>12}\t{2}";

            if (centered)
            {
                var result = string.Join(Environment.NewLine, inputs.Select(k => GetPlotLine(f(k), setMinP, setMaxP, plotWidth)));
                return result;
            } else
            {
                var result = string.Join(Environment.NewLine, inputs.Select(k => GetCenteredPlotLine(f(k), setMinP, setMaxP, plotWidth)));
                return result;
            }

            // return str.join("\n",list(map(lambda k:\
            //     formatString.format(\
            //     round(k,4),\
            //     round(p(k), 4),\
            //     plotFunction(p(k),setMinP,setMaxP,plotWidth)\
            //     ), inputs)))
        }

        public static string GetSimplePlot(
            Func<M, R> f,
            IEnumerable<M> inputs,
            int plotWidth = 50,
            R? minP = null,
            R? maxP = null,
            bool asPercentage = false,
            bool centered = true)
        {
            var formatString = asPercentage
                ? "{0:.2%}"
                : "{0}";
            var result = string.Join(Environment.NewLine, inputs.Select(k => f(k).ToString()));
            return result;

            // return str.join("\n",list(map(lambda k:\
            //     formatString.format(round(p(k), 4)),\
            //     inputs)))
        }
    }
}