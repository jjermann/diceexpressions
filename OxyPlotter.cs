using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Generic.Math;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PType = System.Double;

namespace diceexpressions
{
    public static class OxyPlotter<T>
    {
        private static double ScatterPointSize => 5;

        public static PlotModel GetPlot(
            Func<T, PType> f,
            IEnumerable<T> inputs,
            string title = "Plot",
            string subtitle = null,
            double? xMin = null,
            double? xMax = null,
            double? yMin = null,
            double? yMax = null,
            Func<double, string> keyLabelFormatter = null,
            Func<double, string> valueLabelFormatter = null,
            bool withMarkers = true)
        {
            var inputList = inputs.ToList();
            var model = new PlotModel
            {
                Background = OxyColors.White,
                Culture = CultureInfo.InvariantCulture
            };
            if (title != null)
            {
                model.Title = title;
            }
            if (subtitle != null)
            {
                model.Subtitle = subtitle;
            }

            //Add Axes
            var xMinFinal = xMin ?? GenericMath.Convert<T, double>     (GenericMathExtension.Min(inputList.ToArray()));
            var xMaxFinal = xMax ?? GenericMath.Convert<T, double>     (GenericMathExtension.Max(inputList.ToArray()));
            var yMinFinal = yMin ?? GenericMath.Convert<PType, double> (GenericMathExtension.Min(inputList.Select(k => f(k)).ToArray()));
            var yMaxFinal = yMax ?? GenericMath.Convert<PType, double> (GenericMathExtension.Max(inputList.Select(k => f(k)).ToArray()));
            var xAxis = new LinearAxis 
            { 
                Position = AxisPosition.Bottom, 
                Minimum = xMinFinal, 
                Maximum = xMaxFinal
            };
            if (keyLabelFormatter != null)
            {
                xAxis.LabelFormatter = keyLabelFormatter;
            }
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = yMinFinal,
                Maximum = yMaxFinal,
            };
            if (valueLabelFormatter != null)
            {
                yAxis.LabelFormatter = valueLabelFormatter;
            }
            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            //Add line plot
            var lineseries = new LineSeries
            {
                StrokeThickness = 1.0,
                LineStyle = LineStyle.Solid,
            };
            if (withMarkers)
            {
                lineseries.Color = OxyColors.DarkGray;
                lineseries.MarkerSize = 2.0;
                lineseries.MarkerFill = OxyColors.Transparent;
                lineseries.MarkerType = MarkerType.Cross;
                lineseries.MarkerStroke = OxyColors.Red;
            } else {
                lineseries.Color = OxyColors.Black;
            }
            foreach (var key in inputList)
            {
                lineseries.Points.Add(
                    new DataPoint(GenericMath.Convert<T,double>(key),
                    GenericMath.Convert<PType,double>(f(key))));
            }
            model.Series.Add(lineseries);

            return model;
        }
    }
}