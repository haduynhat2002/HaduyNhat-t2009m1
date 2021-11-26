using Database.Entities;
using Database.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Database.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoteForm : Page
    {
        private ContactModel noteModel = new ContactModel();
        public NoteForm()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var note = new Note()
            {              
                Name = txtName.Text,
                PhoneNumber = txtPhoneNumber.Text,                
            };
            var result = noteModel.Save(note);
            if (result)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Add contact success";
                contentDialog.PrimaryButtonText = "Okie";
                contentDialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.NoteList));
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Add contact fails. Please your again";
                contentDialog.PrimaryButtonText = "Okie";
                contentDialog.ShowAsync();
            }
        }
    }
}
