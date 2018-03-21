using System;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiceExpressions.Model.Densities;
using DiceExpressions.Model.Helpers;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.ViewModels;
using OxyPlot;
using ReactiveUI;

namespace DiceExpressions.ViewModel
{
    public abstract class DensityExpressionsViewModel<G, M, RF> :
        ViewModelBase
        where G :
            IBaseStructure<M>,
            IRealEmbedding<M,RF>
        where M :
            struct
        where RF :
            struct
    {
        protected static readonly TimeSpan ThrottleTimeSpan = TimeSpan.FromMilliseconds(100);
        protected static readonly IScheduler UsedScheduler = Scheduler.Default;

        public DensityExpressionsViewModel()
        {
            var thread = Scheduler.CurrentThread;
            this.WhenAnyValue(x => x.DiceExpression)
                .Throttle(ThrottleTimeSpan)
                .SelectLastAsync(x => ParseDiceExpression(x))
                .Catch(Observable.Return((DensityExpressionResult<G,M,RF>)null))
                .ToProperty(this, x => x.ParsedExpression, out _parsedExpression, null);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Select(x => (x == null) ? "No Expression to parse!" : x.ErrorString)
                .ToProperty(this, x => x.ParseError, out _parseError, null);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Where(x => x != null && x.ErrorString == null)
                .Select(x => x?.Density)
                .ToProperty(this, x => x.Density, out _density, null);
            this.WhenAnyValue(x => x.Density)
                .Throttle(ThrottleTimeSpan)
                .ObserveOn(UsedScheduler)
                .Catch(Observable.Return((IDensity<G,M,RF>)null))
                .Select(x => x?.OxyPlot())
                .ToProperty(this, x => x.Plot, out _plot, null);
            this.WhenAnyValue(x => x.ParsedExpression)
                .Where(x => x != null && x.ErrorString == null)
                .Select(x => x?.Probability)
                .ToProperty(this, x => x.Probability, out _probability, null);
            this.WhenAnyValue(x => x.Density)
                .Select(x => x?.GetTrimmedName())
                .ToProperty(this, x => x.DensityName, out _densityName, null);

        }

        abstract protected DensityExpressionResult<G,M,RF> ParseDiceExpression(string expression);

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

        private ObservableAsPropertyHelper<DensityExpressionResult<G,M,RF>> _parsedExpression;
        public DensityExpressionResult<G,M,RF> ParsedExpression
        {
            get { return _parsedExpression.Value; }
        }

        private ObservableAsPropertyHelper<IDensity<G,M,RF>> _density;
        public IDensity<G,M,RF> Density
        {
            get { return _density.Value; }
        }

        private ObservableAsPropertyHelper<RF?> _probability;
        public RF? Probability
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