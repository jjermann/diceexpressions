using System.Reactive.Linq;
using DiceExpressions.Model;
using DiceExpressions.ModelHelper;
using DiceExpressions.ViewModels;
using OxyPlot;
using ReactiveUI;

namespace DiceExpressions.ViewModel
{
    public abstract class DensityExpressionsViewModel<T> : ViewModelBase
    {
        public DensityExpressionsViewModel()
        {
            this.WhenAnyValue(x => x.DiceExpression)
                .Select(x => ParseDiceExpression(x))
                .ToProperty(this, x => x.Density, out _density);
            this.WhenAnyValue(x => x.Density)
                .Select(x => x?.OxyPlot())
                .ToProperty(this, x => x.Plot, out _plot);
            this.WhenAnyValue(x => x.Density)
                .Select(x => x?.TrimmedName)
                .ToProperty(this, x => x.Result, out _result);
        }

        abstract protected Density<T> ParseDiceExpression(string expression);

        private string _diceExpression;
        public string DiceExpression
        {
            get { return _diceExpression; }
            set { this.RaiseAndSetIfChanged(ref _diceExpression, value); }
        }

        private ObservableAsPropertyHelper<Density<T>> _density;
        public Density<T> Density
        {
            get { return _density.Value; }
        }

        private ObservableAsPropertyHelper<string> _result;
        public string Result
        {
            get { return _result.Value; }
        }

        private ObservableAsPropertyHelper<PlotModel> _plot;
        public PlotModel Plot
        {
            get { return _plot.Value; }
        }
    }
}