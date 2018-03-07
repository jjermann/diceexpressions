using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DiceExpressions.Model.AlgebraicStructure;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DiceExpressions.Model.Helpers
{
    //TODO: For now we assume that M, R are structs to be able to use default nullables
    public class OxyPlotter<G, M, F, R>
        where G :
            IBaseStructure<M>,
            IComparer<M>,
            IRealEmbedding<M, double>
        where F :
            IRealField<R>
        where M :
            struct
        where R :
            struct
    {
        public F BaseRealField { get; }
        public G BaseStructure { get; }
        public OxyPlotter(G baseStructure, F field)
        {
            BaseRealField = field;
            BaseStructure = baseStructure;
        }

        private double ScatterPointSize => 5;

        // TODO: Ideally we would like to have keyLabelFormatter as a function from R to string,
        //       but Oxyplot doesn't work that way and we don't want to enforce an embedding of double into R...
        // private static M EmbedFromDouble(double r) => AlgebraicStructure.EmbedFrom(PField.EmbedFrom(r));

        public PlotModel GetPlot(
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
            var xMinFinal = xMin ?? BaseStructure.Min(inputList.ToArray());
            var xMaxFinal = xMax ?? BaseStructure.Max(inputList.ToArray());
            var yMinFinal = yMin ?? BaseRealField.Min(inputList.Select(k => f(k)).ToArray());
            var yMaxFinal = yMax ?? BaseRealField.Max(inputList.Select(k => f(k)).ToArray());
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = BaseStructure.EmbedToReal(xMinFinal),
                Maximum = BaseStructure.EmbedToReal(xMaxFinal)
                // AxislineColor = OxyColors.Black
            };
            if (keyLabelFormatter != null)
            {
                xAxis.LabelFormatter = keyLabelFormatter;
            }
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = BaseRealField.EmbedToReal(yMinFinal),
                Maximum = BaseRealField.EmbedToReal(yMaxFinal)
                // AxislineColor = OxyColors.Black
            };
            if (valueLabelFormatter != null)
            {
                yAxis.LabelFormatter = r => valueLabelFormatter(BaseRealField.EmbedFromReal(r));
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
                        BaseStructure.EmbedToReal(key),
                        BaseRealField.EmbedToReal(f(key))
                    ));
            }
            model.Series.Add(lineseries);

            return model;
        }
    }
}