using DiceExpressions.ViewModels;
using ReactiveUI;

namespace DiceExpressions.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase _currentPage;
        DiceExpressionsViewModel _diceExpressionsViewModel;

        public MainWindowViewModel()
        {
            _diceExpressionsViewModel = new DiceExpressionsViewModel();
        }

        public ViewModelBase CurrentPage
        {
            get { return _currentPage; }
            private set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
        }
    }
}