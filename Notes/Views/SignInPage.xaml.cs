using System;
using System.Collections.Generic;
using System.IO;
using Notes.Models;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            var dppath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDB.db");
            var db = new SQLiteConnection(dppath);

            var myquery = db.Table<User>().Where(u => u.Username.Equals(EntryUsername.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();

            if (myquery != null)
            {
                App.Current.MainPage = new AppShell();
                App.User = myquery;
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var result = await DisplayAlert("Error", "Failed to sign in. Your username or password is incorrect", null, "Ok");

                    EntryPassword.Text = "";
                });
            }
        }
    }
}
