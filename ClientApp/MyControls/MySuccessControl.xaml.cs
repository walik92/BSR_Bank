using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MyControls
{
    /// <summary>
    ///     Interaction logic for MySuccessControl.xaml
    /// </summary>
    public partial class MySuccessControl : UserControl
    {
        public static readonly DependencyProperty SuccessTextProperty =
            DependencyProperty.Register("SuccessValue", typeof(string),
                typeof(MySuccessControl), new FrameworkPropertyMetadata(string.Empty));

        public MySuccessControl()
        {
            InitializeComponent();
        }


        public string SuccessValue
        {
            get { return GetValue(SuccessTextProperty).ToString(); }
            set { SetValue(SuccessTextProperty, value); }
        }
    }
}