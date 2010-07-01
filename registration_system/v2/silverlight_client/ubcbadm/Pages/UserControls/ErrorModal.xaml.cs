using System.Windows;
using System.Windows.Controls;

namespace ubcbadm
{
    public partial class ErrorModal : UserControl
    {
        public string heading { get; set; }
        public string description { get; set; }

        public ErrorModal()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ErrorModal_Loaded);
        }

        void ErrorModal_Loaded(object sender, RoutedEventArgs e)
        {
            heading_textBlock.Text = heading;
            description_textBlock.Text = description;
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
