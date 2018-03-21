using System.Globalization;
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
    using DType = Density<FieldType<int,PType>, int, PType>;
    using EType = DensityExpressionResult<FieldType<int,PType>, int, PType>;
    public class DiceExpressionsViewModel :
        DensityExpressionsViewModel<FieldType<int,PType>, int, PType>
    {
        public DiceExpressionsViewModel()
        {
            this.WhenAnyValue(x => x.Probability)
                .Select(x => x.HasValue ? x.Value.ToString("P3", CultureInfo.InvariantCulture) : (string)null)
                .ToProperty(this, x => x.ProbabilityFormatted, out _probabilityFormatted, null);
            DiceExpression = "(d20 + ad20) * d2 + 3";

        }

        protected override EType ParseDiceExpression(string expression)
        {
            var res = DieParser.Parse(expression);
            return res;
        }

        private ObservableAsPropertyHelper<string> _probabilityFormatted;
        public string ProbabilityFormatted
        {
            get { return _probabilityFormatted.Value; }
        }
    }
}