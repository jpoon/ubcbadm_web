﻿using System;
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
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace ubcbadm
{
    public partial class MainPage : UserControl
    {
        public enum FeeTypes
        {
            New,
            Returning,
            Non_AMS
        }

        static string ADMIN_PASSWORD = "paid";
        //static string MEMBER_URL = "http://ubc-badminton.appspot.com/member";
        static string MEMBER_URL = "http://localhost:8080/member";
        static Dictionary<FeeTypes, string> MEMBERSHIP_FEES = new Dictionary<FeeTypes, string>() {
            {FeeTypes.New, "$50"},
            {FeeTypes.Returning, "$40"},
            {FeeTypes.Non_AMS, "$60"}
        };
        ClubMember member;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            fees_comboBox.SelectedIndex = 0;
            member = new ClubMember();
            LayoutRoot.DataContext = member;
            LayoutRoot.BindingValidationError += new EventHandler<ValidationErrorEventArgs>(member.BindingValidationError);
            feesOwed(null, null);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (admin_passwordBox.Password != ADMIN_PASSWORD)
            {
                adminPasswordError.Visibility = Visibility.Visible;
            }
            else
            {
                // affiliation
                if (student_radioButton.IsChecked == true)
                    member.affiliation = student_radioButton.Content.ToString();
                else if (faculty_radioButton.IsChecked == true)
                    member.affiliation = faculty_radioButton.Content.ToString();
                else
                    member.affiliation = other_radioButton.Content.ToString();

                // membership type
                if (new_radioButton.IsChecked == true)
                    member.memberType = new_radioButton.Content.ToString();
                else
                    member.memberType = returning_radioButton.Content.ToString();

                // skill level
                if (beginner_radioButton.IsChecked == true)
                    member.skillLevel = beginner_radioButton.Content.ToString();
                else if (novice_radioButton.IsChecked == true)
                    member.skillLevel = novice_radioButton.Content.ToString();
                else if (intermediate_radioButton.IsChecked == true)
                    member.skillLevel = intermediate_radioButton.Content.ToString();
                else
                    member.skillLevel = advanced_radioButton.Content.ToString();

                WebClient cnt = new WebClient();
                cnt.Headers["Content-type"] = "application/json";
                cnt.Encoding = Encoding.UTF8;
                cnt.UploadStringAsync(new Uri(MEMBER_URL), "POST", ClubMember.Serialize(member));
                cnt.UploadStringCompleted += new UploadStringCompletedEventHandler(cnt_UploadStringCompleted);
            }
        }

        void cnt_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                serverError.description = e.Error.Message;
                serverError.Visibility = Visibility.Visible;
            }
            else
            {
                uint membershipNo = uint.Parse(e.Result);
                ConfirmationPage page = new ConfirmationPage(member, membershipNo);
                ((UserControlContainer)Application.Current.RootVisual).SwitchControl(page);
            }
        }

        private void feesOwed(object sender, RoutedEventArgs e)
        {
            if (other_radioButton.IsChecked == true)
                // Non-AMS
                feesOwed_label.Content = MEMBERSHIP_FEES[FeeTypes.Non_AMS];
            else
            {
                // New
                if (new_radioButton.IsChecked == true)
                    feesOwed_label.Content = MEMBERSHIP_FEES[FeeTypes.New];
                // Returning
                else
                    feesOwed_label.Content = MEMBERSHIP_FEES[FeeTypes.Returning];
            }
        }

        private void student_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                studentNoGrid.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void student_radioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                studentNoGrid.Visibility = Visibility.Collapsed;
                studentNo_textBox.Text = "";
            }
            catch { }
        }

        private void fees_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (fees_comboBox.SelectedIndex)
            {
                case 0:
                    // new member
                    feeAmount_label.Content = MEMBERSHIP_FEES[FeeTypes.New];
                    break;
                case 1:
                    // returning member
                    feeAmount_label.Content = MEMBERSHIP_FEES[FeeTypes.Returning];
                    break;
                case 2:
                    // non-ams
                    feeAmount_label.Content = MEMBERSHIP_FEES[FeeTypes.Non_AMS];
                    break;
            }
        }
    }
}