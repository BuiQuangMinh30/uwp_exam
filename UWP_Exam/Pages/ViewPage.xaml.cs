using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ViewPage : Page
    {
        public List<Person> listPerson;
        public static Person personal;
        public ViewPage()
        {
            this.InitializeComponent();
            this.Loaded += ViewPage_Loaded;
        }

        private void ViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            listPerson = DBInitialize.GetList();
            Debug.WriteLine(listPerson);
            ListDataGridContact.ItemsSource = listPerson;
        }
        
        private void Reset_List(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            listPerson = DBInitialize.GetList();
            Debug.WriteLine(listPerson);
            ListDataGridContact.ItemsSource = listPerson;
        }


        private void CreateTransactionButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.CreateContact));
        }

        private async void Search_Name(object sender, RoutedEventArgs e)
        {
            string name = (string)txtSearch.Text;
            ContentDialog contentDialog = new ContentDialog();
            try
            {
                List<Person> listSearchByName = DBInitialize.ListTransactionByName(name);
                if(listSearchByName.Count < 1)
                {
                    contentDialog.Title = "Fails";
                    contentDialog.Content = "Contact not found";
                    contentDialog.CloseButtonText = "Oke";
                    await contentDialog.ShowAsync();
                }
                else
                {
                    ListDataGridContact.ItemsSource = listSearchByName;
                }
               
            }
            catch
            {
                contentDialog.Title = "Lỗi!";
                contentDialog.Content = "Có lỗi xảy ra!";
                contentDialog.CloseButtonText = "Oke";
                await contentDialog.ShowAsync();
            }
        }
    }
}
