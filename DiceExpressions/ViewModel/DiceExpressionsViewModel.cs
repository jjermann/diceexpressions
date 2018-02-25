using DiceExpressions.Model;
using DiceExpressions.ModelHelper;
using DiceExpressions.ViewModels;
using OxyPlot;
using ReactiveUI;
using PType = System.Double;

namespace DiceExpressions.ViewModel
{
    public class DiceExpressionsViewModel : DensityExpressionsViewModel<int>
    {
        public DiceExpressionsViewModel()
        {
            DiceExpression = "(d20 + ad20) * d2 + 3";
        }

        protected override Density<int> ParseDiceExpression(string expression)
        {
            var density = DieParser.Parse(expression);
            return density;
        }
    }
}