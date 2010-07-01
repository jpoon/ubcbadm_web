using System.Windows.Controls;

namespace ubcbadm
{
    public partial class UserControlContainer : UserControl
    {
        public UserControlContainer()
        {
            InitializeComponent();
        }

        public void SwitchControl(UserControl newControl)
        {
            LayoutRoot.Children.Clear();
            if (newControl != null)
            {
                Height = newControl.Height;
                Width = newControl.Width;
                LayoutRoot.Children.Add(newControl);
            }
        }
    }
}
