using System;
using Antlr4.Runtime;
using Avalonia;
using DiceExpressions.Model;
using DiceExpressions.View;
using DiceExpressions.ViewModel;

namespace DiceExpressions
{
    public class Program
    {
        static void Main(string[] args)
        {
            var example = new MultiDensity<int>(new Die(20), new Die(10), new Die(10)).DropLowest(2);
            Console.WriteLine(example.Plot(40));
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .Start<DiceExpressionsView>(() => new DiceExpressionsViewModel());
        }
    }
}
