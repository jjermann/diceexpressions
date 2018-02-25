using Avalonia;
using DiceExpressions.View;
using DiceExpressions.ViewModel;

namespace DiceExpressions
{
    public class Program
    {
        static void Main(string[] args)
        {
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .Start<DiceExpressionsView>(() => new DiceExpressionsViewModel());
        }
    }
}
