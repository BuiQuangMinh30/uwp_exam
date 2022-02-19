using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using UWP_Exam.DB;
using UWP_Exam.Empty;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Exam.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateContact : Page
    {
        private int countInt = 0;
        public CreateContact()
        {
            this.InitializeComponent();
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(84|0[3|5|7|8|9])+([0-9]{8})$").Success;
        }
        private void CheckValidate(string name, string phone)
        {
            countInt = 0;
            if (string.IsNullOrEmpty(name))
            {
                lblCheckname.Text = "please enter name";
            }
            else
            {
                lblCheckname.Text = "";
                countInt++;
            }
            if (string.IsNullOrEmpty(phone))
            {
                lblCheckPhone.Text = "please enter phone";
            }
            else
            {
                if (!IsPhoneNumber(phone))
                {
                    lblCheckPhone.Text = "This is not a phone number";
                }
                else
                {
                    lblCheckPhone.Text = "";
                    countInt++;
                }
            }
        }
        private async void InsertForContact(object sender, RoutedEventArgs e)
        {
            CheckValidate(name.Text, phone.Text);
            if(countInt < 2)
            {
                return;
            }
            Person personal = new Person()
            {
                Name = name.Text,
                Phone = phone.Text,
            };

            ContentDialog contentDialog = new ContentDialog();
            if (DBInitialize.InsertContact(personal))
            {
                contentDialog.Title = "Acction success";
                contentDialog.Content = "Create success";

                name.Text = "";
                phone.Text = "";
            }
            else
            {
                contentDialog.Title = "Acction fail";
                contentDialog.Content = "Create fails";
            }
            contentDialog.CloseButtonText = "OK";
            await contentDialog.ShowAsync();
        }

        private void ListContact(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.ViewPage));
        }

    }
}
