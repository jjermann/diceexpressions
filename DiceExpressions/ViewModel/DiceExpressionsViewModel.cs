using DiceExpressions.Model;
using DiceExpressions.ViewModels;
using OxyPlot;
using ReactiveUI;
using PType = System.Double;

namespace DiceExpressions.ViewModel
{
    public class DiceExpressionsViewModel : ViewModelBase
    {
        public DiceExpressionsViewModel()
        {
            var d1 = new Die(20);
            var d2 = new Constant<PType>((PType)1.7);
            var d3=d1+d2;
            var d4 = d1.ArithMult(3);
            var d5 = d1.ArithMult(d1);
            var svgStr = d5.GetOxyPlotSvg("Test");
            d5.SaveOxyPlotPdf("test.pdf");
            
            Result = d5.Expected().ToString();
            Plot = d5.OxyPlot();
        }

        private string _diceExpression;
        public string DiceExpression
        {
            get { return _diceExpression; }
            set { this.RaiseAndSetIfChanged(ref _diceExpression, value); }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            private set { this.RaiseAndSetIfChanged(ref _result, value); }
        }

        private PlotModel _plot;
        public PlotModel Plot
        {
            get { return _plot; }
            private set { this.RaiseAndSetIfChanged(ref _plot, value); }
        }
    }
}