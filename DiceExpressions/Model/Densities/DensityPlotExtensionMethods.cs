using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.Helpers;
using OxyPlot;

namespace DiceExpressions.Model.Densities
{
    public static class DensityPlotExtensionMethods
    {
        public static string Plot<G,M,RF>(this IDensity<G,M,RF> d, int plotWidth = 70)
            where G :
                IBaseStructure<M>
            where RF :
                struct
        {
            var rf = d.RealField;
            var maxPercentage = rf.Max(d.GetValues());
            var asciiPlotter = new AsciiPlotter<M, RealFieldType<RF>, RF>(new RealFieldType<RF>());
            var plotStr = asciiPlotter.GetPlot(
                k => d[k],
                d.SortedKeys(),
                plotWidth:plotWidth,
                minP:rf.Zero(),
                maxP:maxPercentage,
                asPercentage:true);
            return plotStr;
        }

        public static PlotModel OxyPlot<G,M,RF>(this IDensity<G,M,RF> d, string title = null)
            where G :
                IBaseStructure<M>,
                IRealEmbedding<M,RF>
            where M :
                struct
            where RF :
                struct
        {
            var oxyplotter = new OxyPlotter<G,M,RealFieldType<RF>,RF>(d.BaseStructure, new RealFieldType<RF>());
            var rf = d.RealField;
            title = title ?? d.GetTrimmedName();
            // TODO: For Expected() and Stdev() much more assumptions are necessary!
            // var subtitle = string.Format(
            //     CultureInfo.InvariantCulture,
            //     "Expected = {0:0.0000}, Stdev = {1:0.00}",
            //     d.Expected(),
            //     d.Stdev()
            // );
            var subtitle = "";
            var plotModel = oxyplotter.GetPlot(
                k => d[k],
                d.SortedKeys(),
                title: title,
                subtitle: subtitle,
                yMin: rf.EmbedFromReal(0.0),
                valueLabelFormatter: p => rf.EmbedToReal(p).ToString("P", CultureInfo.InvariantCulture));
            return plotModel;
        }

        public static string GetOxyPlotSvg<G,M,RF>(this IDensity<G,M,RF> d, string title)
            where G :
                IBaseStructure<M>,
                IRealEmbedding<M,RF>
            where M :
                struct
            where RF :
                struct
        {
            var oxyplot = d.OxyPlot(title);
            var svgExporter = new SvgExporter { Width = 600, Height = 400 };
            var res = svgExporter.ExportToString(oxyplot);
            return res;
        }

        public static void SaveOxyPlotPdf<G,M,RF>(this IDensity<G,M,RF> d, string filename = null)
            where G :
                IBaseStructure<M>,
                IRealEmbedding<M,RF>
            where M :
                struct
            where RF :
                struct
        {
            filename = filename ?? d.GetTrimmedName() + ".pdf";
            var oxyplot = d.OxyPlot(Path.GetFileName(filename));
            using (var stream = File.Create(filename))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(oxyplot, stream);
            }
        }        
    }
}