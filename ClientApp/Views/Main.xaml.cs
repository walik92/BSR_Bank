using ClientApp.ViewModels;
using MahApps.Metro.Controls;

namespace ClientApp.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        private readonly MainViewModel _model;

        public Main()
        {
            _model = new MainViewModel();
            DataContext = _model;
            if (_model.CloseAction == null)
                _model.CloseAction = Close;
            InitializeComponent();
        }
    }
}