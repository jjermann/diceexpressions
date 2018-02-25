using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Math;
using PType = System.Double;

namespace DiceExpressions.Model
{
    public static class AsciiPlotter<T>
    {
        private static PType Delta => GenericMath.Convert<double,PType>(1e-9);
        private static string FillChar => "█";
        private static string SepChar => "│";
        private static string VoidChar => " ";

        private static string GetPlotLine(PType p, PType minP, PType maxP, int plotWidth)
        {
            var aboveMax = GenericMath.GreaterThan(GenericMath.Subtract(p,maxP), Delta);
            var belowMin = GenericMath.GreaterThan(GenericMath.Subtract(minP,p), Delta);
            var correctedZero = GenericMath<PType>.Zero;
            correctedZero = GenericMathExtension.Max(correctedZero, minP);
            correctedZero = GenericMathExtension.Min(correctedZero, maxP);

            p = GenericMathExtension.Max(p, minP);
            p = GenericMathExtension.Min(p, maxP);
            
            var unroundedZeroPosition = GenericMath.Divide(GenericMath.MultiplyAlternative((correctedZero-minP),plotWidth),(maxP-minP));
            var correctedZeroPosition = GenericMathExtension.Round(unroundedZeroPosition);
            var unroundedPosition = GenericMath.Divide(GenericMath.MultiplyAlternative((p-minP),plotWidth),(maxP-minP));
            var position = GenericMathExtension.Round(unroundedPosition);

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

        private static string GetCenteredPlotLine(PType p, PType minP, PType maxP, int plotWidth)
        {
            var aboveMax = GenericMath.GreaterThan(GenericMath.Subtract(p,maxP), Delta);
            var belowMin = GenericMath.GreaterThan(GenericMath.Subtract(minP,p), Delta);
            var correctedZero = GenericMath<PType>.Zero;
            correctedZero = GenericMathExtension.Max(correctedZero, minP);
            correctedZero = GenericMathExtension.Min(correctedZero, maxP);

            p = GenericMathExtension.Max(p, minP);
            p = GenericMathExtension.Min(p, maxP);

            var unroundedZeroPosition = GenericMath.Divide(GenericMath.MultiplyAlternative((correctedZero-minP),plotWidth),(maxP-minP));
            var correctedZeroPosition = GenericMathExtension.Round(unroundedZeroPosition);
            var unroundedPosition = GenericMath.Divide(GenericMath.MultiplyAlternative((p-minP),plotWidth),(maxP-minP));
            var position = GenericMathExtension.Round(unroundedPosition);

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
            var result = belowMin || correctedZero > 0
                ? FillChar
                : SepChar;
            result += mainContent;
            result += aboveMax || correctedZero < 0
                ? FillChar
                : SepChar;
            return result;
        }

        public static string GetPlot(
            Func<T, PType> f,
            IEnumerable<T> inputs,
            int plotWidth = 50,
            PType? minP = null,
            PType? maxP = null,
            bool asPercentage = false,
            bool centered = true)
        {
            var setMinP = minP ?? GenericMathExtension.Min(inputs.Select(k => f(k)));
            var setMaxP = maxP ?? GenericMathExtension.Max(inputs.Select(k => f(k)));
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
            Func<T, PType> f,
            IEnumerable<T> inputs,
            int plotWidth = 50,
            PType? minP = null,
            PType? maxP = null,
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