using ClientApp.ViewModels;
using MahApps.Metro.Controls;

namespace ClientApp.Views
{
    /// <summary>
    ///     Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        private LoginViewModel _model;

        public Login()
        {
            Init();
        }

        public Login(string error)
        {
            Init();
            _model.Error = error;
        }

        private void Init()
        {
            _model = new LoginViewModel();

            DataContext = _model;
            if (_model.CloseAction == null)
                _model.CloseAction = Close;
            InitializeComponent();
        }
    }
}