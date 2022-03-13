using System;
using System.Collections.Generic;
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

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<User>();

            var item = new User()
            {
                username = EntryUsername.Text,
                password = EntryPassword.Text,
                name = EntryName.Text,
                email = EntryEmail.Text
            };

            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await this.DisplayAlert("Congratz", "Successfully signed up!", "Okay", "Cancel");

                if (result)
                    App.Current.MainPage = new SignInPage();
            });
        }
    }
}
