using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DiceExpressions.Model.Helpers
{
    //TODO: For now we assume that M, R are structs to be able to use default nullables
    public static class OxyPlotter<G, M, F, R>
        where G :
            IEqualityComparer<M>,
            IComparer<M>,
            IEmbedTo<M, R>,
            new()
        where F :
            IProbabilityField<R>,
            new()
        where M :
            struct
        where R :
            struct
    {
        private static double ScatterPointSize => 5;
        private static readonly G AlgebraicStructure = new G();
        private static readonly F PField = new F();
        private static double EmbedToDouble(M m) => PField.EmbedTo(AlgebraicStructure.EmbedTo(m));
        // TODO: Ideally we would like to have keyLabelFormatter as a function from R to string,
        //       but Oxyplot doesn't work that way and we don't want to enforce an embedding of double into R...
        // private static M EmbedFromDouble(double r) => AlgebraicStructure.EmbedFrom(PField.EmbedFrom(r));

        public static PlotModel GetPlot(
            Func<M, R> f,
            IEnumerable<M> inputs,
            string title = "Plot",
            string subtitle = null,
            M? xMin = null,
            M? xMax = null,
            R? yMin = null,
            R? yMax = null,
            Func<double, string> keyLabelFormatter = null,
            Func<R, string> valueLabelFormatter = null,
            bool withMarkers = true)
        {
            var inputList = inputs.ToList();
            var model = new PlotModel
            {
                Background = OxyColors.White,
                // PlotAreaBorderColor = OxyColors.Black,
                Culture = CultureInfo.InvariantCulture,
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
            var xMinFinal = xMin ?? AlgebraicStructure.Min(inputList.ToArray());
            var xMaxFinal = xMax ?? AlgebraicStructure.Max(inputList.ToArray());
            var yMinFinal = yMin ?? PField.Min(inputList.Select(k => f(k)).ToArray());
            var yMaxFinal = yMax ?? PField.Max(inputList.Select(k => f(k)).ToArray());
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = EmbedToDouble(xMinFinal),
                Maximum = EmbedToDouble(xMaxFinal)
                // AxislineColor = OxyColors.Black
            };
            if (keyLabelFormatter != null)
            {
                xAxis.LabelFormatter = keyLabelFormatter;
            }
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = PField.EmbedTo(yMinFinal),
                Maximum = PField.EmbedTo(yMaxFinal)
                // AxislineColor = OxyColors.Black
            };
            if (valueLabelFormatter != null)
            {
                yAxis.LabelFormatter = r => valueLabelFormatter(PField.EmbedFrom(r));
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
            } else
            {
                lineseries.Color = OxyColors.Black;
            }
            foreach (var key in inputList)
            {
                lineseries.Points.Add(
                    new DataPoint(
                        EmbedToDouble(key),
                        PField.EmbedTo(f(key))
                    ));
            }
            model.Series.Add(lineseries);

            return model;
        }
    }
}