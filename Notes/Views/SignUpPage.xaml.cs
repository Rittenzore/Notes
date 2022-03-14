using System;
using System.IO;
using Notes.Models;
using SQLite;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            var item = new User()
            {
                Username = EntryUsername.Text,
                Password = EntryPassword.Text,
                Name = EntryName.Text,
                Email = EntryEmail.Text
            };

            var signUp = await App.UserDB.SignUp(item);

            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await DisplayAlert("Congratz", "Successfully signed up!", null, "Ok");

                if (!result)
                    App.Current.MainPage = new NavigationPage(new SignInPage());
            });
        }
    }
}
