using System;
using System.Globalization;
using System.Reactive.Concurrency;
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
        private static readonly TimeSpan ThrottleTimeSpan = TimeSpan.FromMilliseconds(100);
        private static readonly IScheduler UsedScheduler = Scheduler.Default;

        public DensityExpressionsViewModel()
        {
            this.WhenAnyValue(x => x.DiceExpression)
                .Throttle(ThrottleTimeSpan)
                .ObserveOn(UsedScheduler)
                .Catch(Observable.Return((string)null))
                .Select(x => ParseDiceExpression(x))
                .ToProperty(this, x => x.ParsedExpression, out _parsedExpression, null);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Select(x => (x == null) ? "No Expression to parse!" : x.ErrorString)
                .ToProperty(this, x => x.ParseError, out _parseError, null);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Where(x => x != null && x.ErrorString == null)
                .Select(x => x?.Density)
                .ToProperty(this, x => x.Density, out _density, null);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Where(x => x != null && x.ErrorString == null)
                .Select(x => x?.Probability)
                .ToProperty(this, x => x.Probability, out _probability, null);
            this.WhenAnyValue(x => x.Density)
                .Throttle(ThrottleTimeSpan)
                .ObserveOn(UsedScheduler)
                .Catch(Observable.Return((Density<T>)null))
                .Select(x => x?.OxyPlot())
                .ToProperty(this, x => x.Plot, out _plot, null);
            this.WhenAnyValue(x => x.Density)
                .Select(x => x?.TrimmedName)
                .ToProperty(this, x => x.DensityName, out _densityName, null);
            this.WhenAnyValue(x => x.Probability)
                .Select(x => x.HasValue ? x.Value.ToString("P3", CultureInfo.InvariantCulture) : (string)null)
                .ToProperty(this, x => x.ProbabilityFormatted, out _probabilityFormatted, null);
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

        private ObservableAsPropertyHelper<string> _probabilityFormatted;
        public string ProbabilityFormatted
        {
            get { return _probabilityFormatted.Value; }
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