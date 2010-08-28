using System.Windows;
using System.Windows.Controls;

namespace ubcbadm
{
    public partial class ConfirmationPage : Page
    {
        ClubMember member;
        int memberNo;

        public ConfirmationPage(ClubMember member, int memberNo)
        {
            InitializeComponent();
            this.member = member;
            this.memberNo = memberNo;

            Loaded += new RoutedEventHandler(ConfirmationPage_Loaded);
        }

        void ConfirmationPage_Loaded(object sender, RoutedEventArgs e)
        {
            name_label.Content = member.firstName + " " + member.lastName + ",";
            memberNo_label.Content = memberNo;
            welcome_textblock.Text = "Congratulations on becoming a member of the UBC Badminton Club! \r\n\r\n" +
                "Be sure to check your email inbox as we just sent an email to " + member.email +
                " containing important badminton club information.";
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            HomePage page = new HomePage();
            ((UserControlContainer)Application.Current.RootVisual).SwitchControl(page);
        }
    }
}
