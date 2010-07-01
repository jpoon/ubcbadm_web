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
    public partial class ConfirmationPage : UserControl
    {
        uint membershipNo;
        ClubMember member;

        public ConfirmationPage(ClubMember member, uint membershipNo)
        {
            InitializeComponent();
            this.member = member;
            this.membershipNo = membershipNo;

            Loaded += new RoutedEventHandler(ConfirmationPage_Loaded);
        }

        void ConfirmationPage_Loaded(object sender, RoutedEventArgs e)
        {
            name_label.Content = member.firstName + " " + member.lastName + ",";
            welcome_textblock.Text = "Your membership number is " + membershipNo + ". " +
                "Congratulations on becoming a member of the UBC Badminton Club! " +
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
