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
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            var dppath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDB.db");
            var db = new SQLiteConnection(dppath);

            var myquery = db.Table<User>().Where(u => u.username.Equals(EntryUsername.Text) && u.password.Equals(EntryPassword.Text)).FirstOrDefault();

            if (myquery != null)
            {
                App.Current.MainPage = new NavigationPage(new AppShell());
                Preferences.Set("noteID", myquery.ID);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var result = await this.DisplayAlert("Error", "Failed to sign in. Your username or password is incorrect", "Okay", "Cancel");

                    if (result)
                        App.Current.MainPage = new NavigationPage(new SignInPage());
                    else
                    {
                        App.Current.MainPage = new NavigationPage(new SignInPage());
                    }
                });
            }
        }
    }
}
