using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;

namespace DiceExpressions.Model.Helpers
{
    //TODO: For now we assume that R is a struct to be able to use R?
    public class AsciiPlotter<M, F, R>
        where F :
            IRealField<R>
        where R :
            struct
    {
        public F BaseRealField { get; }
        public AsciiPlotter(F field)
        {
            BaseRealField = field;
        }
        private R Delta => BaseRealField.EmbedFromReal(1e-9);
        private string FillChar => "█";
        private string SepChar => "│";
        private string VoidChar => " ";

        private string GetPlotLine(R p, R minP, R maxP, int plotWidth)
        {
            var aboveMax = BaseRealField.Compare(BaseRealField.Subtract(p,maxP), Delta) > 0;
            var belowMin = BaseRealField.Compare(BaseRealField.Subtract(minP,p), Delta) > 0;
            var correctedZero = BaseRealField.Zero();
            correctedZero = BaseRealField.Max(correctedZero, minP);
            correctedZero = BaseRealField.Min(correctedZero, maxP);

            p = BaseRealField.Max(p, minP);
            p = BaseRealField.Min(p, maxP);

            var unroundedZeroPosition = BaseRealField.Divide(BaseRealField.ScalarMult(plotWidth, BaseRealField.Subtract(correctedZero,minP)), BaseRealField.Subtract(maxP, minP));
            var correctedZeroPosition = BaseRealField.Round(unroundedZeroPosition);
            var unroundedPosition = BaseRealField.Divide(BaseRealField.ScalarMult(plotWidth, BaseRealField.Subtract(p, minP)), BaseRealField.Subtract(maxP,minP));
            var position = BaseRealField.Round(unroundedPosition);

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

        private string GetCenteredPlotLine(R p, R minP, R maxP, int plotWidth)
        {
            var aboveMax = BaseRealField.Compare(BaseRealField.Subtract(p,maxP), Delta) > 0;
            var belowMin = BaseRealField.Compare(BaseRealField.Subtract(minP,p), Delta) > 0;
            var correctedZero = BaseRealField.Zero();
            correctedZero = BaseRealField.Max(correctedZero, minP);
            correctedZero = BaseRealField.Min(correctedZero, maxP);

            p = BaseRealField.Max(p, minP);
            p = BaseRealField.Min(p, maxP);

            var unroundedZeroPosition = BaseRealField.Divide(BaseRealField.ScalarMult(plotWidth, BaseRealField.Subtract(correctedZero,minP)), BaseRealField.Subtract(maxP,minP));
            var correctedZeroPosition = BaseRealField.Round(unroundedZeroPosition);
            var unroundedPosition = BaseRealField.Divide(BaseRealField.ScalarMult(plotWidth, BaseRealField.Subtract(p,minP)),BaseRealField.Subtract(maxP,minP));
            var position = BaseRealField.Round(unroundedPosition);

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
            var result = belowMin || BaseRealField.Compare(correctedZero, BaseRealField.Zero()) > 0
                ? FillChar
                : SepChar;
            result += mainContent;
            result += aboveMax || BaseRealField.Compare(correctedZero, BaseRealField.Zero()) < 0
                ? FillChar
                : SepChar;
            return result;
        }

        public string GetPlot(
            Func<M, R> f,
            IEnumerable<M> inputs,
            int plotWidth = 50,
            R? minP = null,
            R? maxP = null,
            bool asPercentage = false,
            bool centered = true)
        {
            var setMinP = minP ?? BaseRealField.Min(inputs.Select(k => f(k)));
            var setMaxP = maxP ?? BaseRealField.Max(inputs.Select(k => f(k)));
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

        public string GetSimplePlot(
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