using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
