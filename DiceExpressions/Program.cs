using System;
using Antlr4.Runtime;
using Avalonia;
using DiceExpressions.Model;
using DiceExpressions.View;
using DiceExpressions.ViewModel;
using DiceExpressions.Model.Densities;
using DiceExpressions.Model.AlgebraicStructure;
using OxyPlot.Avalonia;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using System.Collections.Generic;
using PType = System.Double;

namespace DiceExpressions
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var example1 = new MultiDensity<FieldType<int>,int>(new Die(20), new Die(10), new Die(10)).DropLowest(2);
            var intDict = new Dictionary<int,PType> {
                {0, 0.1},
                {1, 0.25},
                {2, 0.50},
                {13, 0.15}
            };
            var example2 = new Density<FieldType<int,PType>, int, PType>(intDict, new FieldType<int,PType>(), new RealFieldType<PType>());
            var exp = example2.Expected();

            OxyPlotModule.EnsureLoaded();
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .BeforeStarting(_ => OxyPlotModule.Initialize())
                .Start<DiceExpressionsView>(() => new DiceExpressionsViewModel());
        }
    }
}
