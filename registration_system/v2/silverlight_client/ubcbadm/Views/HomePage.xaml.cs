using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ubcbadm
{
    public partial class HomePage : Page
    {
#if DEBUG
        const string MEMBER_URL = "http://localhost:8080/member";
#else
        const string MEMBER_URL = "http://ubc-badminton.appspot.com/member";
#endif

        const string ADMIN_PASSWORD = "paid";

        readonly static Dictionary<string, string> MEMBERSHIP_FEES = new Dictionary<string, string>() {
            {"New", "$50"},
            {"Returning", "$40"},
            {"Non_AMS", "$60"}
        };



        ClubMember member;

        public HomePage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            member = new ClubMember();
            member.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(member_PropertyChanged);

            /* Info Page */
            fees_comboBox.SelectedIndex = 0;

            /* Register Page */
            updateFeesOwed();
            LayoutRoot.DataContext = member;
            LayoutRoot.BindingValidationError += new EventHandler<ValidationErrorEventArgs>(member.BindingValidationError);
        }

        private void member_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "affiliation":
                    if (member.affiliation == "student")
                        studentNoGrid.Visibility = Visibility.Visible;
                    else
                        studentNoGrid.Visibility = Visibility.Collapsed;
                    updateFeesOwed();
                    break;
                case "memberType":
                    updateFeesOwed();
                    break;
            }

        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (admin_passwordBox.Password != ADMIN_PASSWORD)
            {
                adminPasswordError.Show();
            }
            else
            {
                if (member.affiliation != "student")
                    member.studentNo = "";

                WebClient client = new WebClient();
                client.Headers["Content-Type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                client.UploadStringCompleted += new UploadStringCompletedEventHandler(cnt_UploadStringCompleted);
                client.UploadStringAsync(new Uri(MEMBER_URL), "POST", ClubMember.Serialize(member));
            }
        }

        private void cnt_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null || !String.IsNullOrEmpty(e.Result))
            {
                if (e.Error != null)
                {
                    serverError.description = e.Error.Message;
                }
                else
                {
                    serverError.description = e.Result;
                }
                serverError.heading = "Uh Oh! Error";
                serverError.Show();
            }
            else
            {
                ConfirmationPage page = new ConfirmationPage(member);
                ((UserControlContainer)Application.Current.RootVisual).SwitchControl(page);
            }
        }

        private void updateFeesOwed()
        {
            if (member.isAffiliation_other)
                feesOwed_label.Content = MEMBERSHIP_FEES["Non_AMS"];
            else
            {
                // New
                if (member.isMemberType_New)
                    feesOwed_label.Content = MEMBERSHIP_FEES["New"];
                // Returning
                else
                    feesOwed_label.Content = MEMBERSHIP_FEES["Returning"];
            }
        }

        private void fees_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (fees_comboBox.SelectedIndex)
            {
                case 0:
                    // new member
                    feeAmount_label.Content = MEMBERSHIP_FEES["New"];
                    break;
                case 1:
                    // returning member
                    feeAmount_label.Content = MEMBERSHIP_FEES["Returning"];
                    break;
                case 2:
                    // non-ams
                    feeAmount_label.Content = MEMBERSHIP_FEES["Non_AMS"];
                    break;
            }
        }
    }
}
