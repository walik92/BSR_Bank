using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MyControls
{
    /// <summary>
    ///     Interaction logic for ErrorControl.xaml
    /// </summary>
    public partial class MyErrorControl : UserControl
    {
        public static readonly DependencyProperty ErrorTextProperty =
            DependencyProperty.Register("ErrorValue", typeof(string),
                typeof(MyErrorControl), new FrameworkPropertyMetadata(string.Empty));

        public MyErrorControl()
        {
            InitializeComponent();
        }

        public string ErrorValue
        {
            get { return GetValue(ErrorTextProperty).ToString(); }
            set { SetValue(ErrorTextProperty, value); }
        }
    }
}