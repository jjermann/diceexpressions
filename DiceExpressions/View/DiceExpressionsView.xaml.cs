using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DiceExpressions.ViewModel;

namespace DiceExpressions.View
{
    public class DiceExpressionsView : Window
    {
        public DiceExpressionsView()
        {
            InitializeComponent();
            this.AttachDevTools();
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}