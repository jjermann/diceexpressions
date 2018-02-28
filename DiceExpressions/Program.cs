using System;
using Antlr4.Runtime;
using Avalonia;
using DiceExpressions.Model;
using DiceExpressions.View;
using DiceExpressions.ViewModel;
using OxyPlot.Avalonia;

namespace DiceExpressions
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var example = new MultiDensity<int>(new Die(20), new Die(10), new Die(10)).DropLowest(2);
            // Console.WriteLine(example.Plot(40));
            OxyPlotModule.EnsureLoaded();
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .BeforeStarting(_ => OxyPlotModule.Initialize())
                .Start<DiceExpressionsView>(() => new DiceExpressionsViewModel());
        }
    }
}
