using System.Windows;
using System.Windows.Controls;

namespace ubcbadm
{
    public partial class ConfirmationPage : UserControl
    {
        ClubMember member;

        public ConfirmationPage(ClubMember member)
        {
            InitializeComponent();
            this.member = member;

            Loaded += new RoutedEventHandler(ConfirmationPage_Loaded);
        }

        void ConfirmationPage_Loaded(object sender, RoutedEventArgs e)
        {
            name_label.Content = member.firstName + " " + member.lastName + ",";
            welcome_textblock.Text = "Congratulations on becoming a member of the UBC Badminton Club! " +
                "Be sure to check your email inbox as we just sent an email to " + member.email +
                " containing important badminton club information.";
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            ((UserControlContainer)Application.Current.RootVisual).SwitchControl(page);
        }

        /*
        WebClient wc = new WebClient();
        wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
        wc.OpenReadAsync(new Uri(memberGetUrl + "?member));
    }

    void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
    {
        List<ClubMember> list = ClubMember.Deserialize<List<ClubMember>>(e.Result);
        foreach (ClubMember member in list)
        {
            //
        }
    }
         */
    }
}
