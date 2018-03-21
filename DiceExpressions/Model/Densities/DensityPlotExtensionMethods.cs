using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.Helpers;
using OxyPlot;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public static class DensityPlotExtensionMethods
    {
        public static string Plot<G,M>(this IDensity<G,M> d, int plotWidth = 70)
            where G :
                IBaseStructure<M>
        {
            var pf = Density<G,M>.PField;
            var maxPercentage = pf.Max(d.GetValues());
            var asciiPlotter = new AsciiPlotter<M, RealFieldType<PType>, PType>(new RealFieldType<PType>());
            var plotStr = asciiPlotter.GetPlot(
                k => d[k],
                d.SortedKeys(),
                plotWidth:plotWidth,
                minP:pf.Zero(),
                maxP:maxPercentage,
                asPercentage:true);
            return plotStr;
        }

        public static PlotModel OxyPlot<G,M>(this IDensity<G,M> d, string title = null)
            where G :
                IBaseStructure<M>,
                IRealEmbedding<M>
            where M :
                struct
        {
            var oxyplotter = new OxyPlotter<G,M,RealFieldType<PType>,PType>(d.BaseStructure, new RealFieldType<PType>());
            var pf = Density<G,M>.PField;
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
                yMin: 0.0,
                valueLabelFormatter: p => p.ToString("P", CultureInfo.InvariantCulture));
            return plotModel;
        }

        public static string GetOxyPlotSvg<G,M>(this IDensity<G,M> d, string title)
            where G :
                IBaseStructure<M>,
                IRealEmbedding<M>
            where M :
                struct
        {
            var oxyplot = d.OxyPlot(title);
            var svgExporter = new SvgExporter { Width = 600, Height = 400 };
            var res = svgExporter.ExportToString(oxyplot);
            return res;
        }

        public static void SaveOxyPlotPdf<G,M>(this IDensity<G,M> d, string filename = null)
            where G :
                IBaseStructure<M>,
                IRealEmbedding<M>
            where M :
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