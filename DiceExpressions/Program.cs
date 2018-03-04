using System;
using Antlr4.Runtime;
using Avalonia;
using DiceExpressions.Model;
using DiceExpressions.View;
using DiceExpressions.ViewModel;
using DiceExpressions.Model.Densities;
using DiceExpressions.Model.AlgebraicStructure;
using OxyPlot.Avalonia;
using DiceExpressions.Model.AlgebraicStructureHelper;
using DiceExpressions.Model.AlgebraicDefaultImplementations;

namespace DiceExpressions
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var example = new MultiDensity<FieldType<int>,int>(new Die(20), new Die(10), new Die(10)).DropLowest(2);
            // Console.WriteLine(example);
            OxyPlotModule.EnsureLoaded();
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .BeforeStarting(_ => OxyPlotModule.Initialize())
                .Start<DiceExpressionsView>(() => new DiceExpressionsViewModel());
        }
    }
}
