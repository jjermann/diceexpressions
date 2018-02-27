using System.Reactive.Linq;
using DiceExpressions.Model;
using DiceExpressions.ModelHelper;
using DiceExpressions.ViewModels;
using OxyPlot;
using ReactiveUI;
using PType = System.Double;

namespace DiceExpressions.ViewModel
{
    public abstract class DensityExpressionsViewModel<T> : ViewModelBase
    {
        public DensityExpressionsViewModel()
        {
            this.WhenAnyValue(x => x.DiceExpression)
                .Select(x => ParseDiceExpression(x))
                .ToProperty(this, x => x.ParsedExpression, out _parsedExpression);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Select(x => (x == null) ? "No Expression to parse!" : x.ErrorString)
                .ToProperty(this, x => x.ParseError, out _parseError);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Where(x => x != null && x.ErrorString == null)
                .Select(x => x?.Density)
                .ToProperty(this, x => x.Density, out _density);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Where(x => x != null && x.ErrorString == null)
                .Select(x => x?.Probability)
                .ToProperty(this, x => x.Probability, out _probability);
            this.WhenAnyValue(x => x.Density)
                .Select(x => x?.OxyPlot())
                .ToProperty(this, x => x.Plot, out _plot);
            this.WhenAnyValue(x => x.Density)
                .Select(x => x?.TrimmedName)
                .ToProperty(this, x => x.DensityName, out _densityName);
        }

        abstract protected DensityExpressionResult<T> ParseDiceExpression(string expression);

        private string _diceExpression;
        public string DiceExpression
        {
            get { return _diceExpression; }
            set { this.RaiseAndSetIfChanged(ref _diceExpression, value); }
        }

        private ObservableAsPropertyHelper<string> _parseError;
        public string ParseError
        {
            get { return _parseError.Value; }
        }

        private ObservableAsPropertyHelper<DensityExpressionResult<T>> _parsedExpression;
        public DensityExpressionResult<T> ParsedExpression
        {
            get { return _parsedExpression.Value; }
        }

        private ObservableAsPropertyHelper<Density<T>> _density;
        public Density<T> Density
        {
            get { return _density.Value; }
        }

        private ObservableAsPropertyHelper<PType?> _probability;
        public PType? Probability
        {
            get { return _probability.Value; }
        }

        private ObservableAsPropertyHelper<string> _densityName;
        public string DensityName
        {
            get { return _densityName.Value; }
        }

        private ObservableAsPropertyHelper<PlotModel> _plot;
        public PlotModel Plot
        {
            get { return _plot.Value; }
        }
    }
}