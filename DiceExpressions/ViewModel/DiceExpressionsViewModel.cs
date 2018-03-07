using System.Reactive.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.Densities;
using DiceExpressions.Model.Helpers;
using DiceExpressions.ViewModels;
using OxyPlot;
using ReactiveUI;
using PType = System.Double;

namespace DiceExpressions.ViewModel
{
    using DType = Density<FieldType<int>, int>;
    using EType = DensityExpressionResult<FieldType<int>, int>;
    public class DiceExpressionsViewModel :
        DensityExpressionsViewModel<FieldType<int>, int>
    {
        public DiceExpressionsViewModel()
        {
            DiceExpression = "(d20 + ad20) * d2 + 3";
        }

        protected override EType ParseDiceExpression(string expression)
        {
            var res = DieParser.Parse(expression);
            return res;
        }
    }
}