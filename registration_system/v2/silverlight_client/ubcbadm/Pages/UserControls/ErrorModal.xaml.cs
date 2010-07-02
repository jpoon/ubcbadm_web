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
        }

        public void Show()
        {
            heading_textBlock.Text = heading;
            description_textBlock.Text = description;
            Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
