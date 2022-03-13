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

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDB.db");
            var db = new SQLiteConnection(dbPath);
            if (db == null)
            {
                db.CreateTable<User>();
            }

            var item = new User()
            {
                Username = EntryUsername.Text,
                Password = EntryPassword.Text,
                Name = EntryName.Text,
                Email = EntryEmail.Text
            };

            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await this.DisplayAlert("Congratz", "Successfully signed up!", null, "Ok");

                if (!result)
                    App.Current.MainPage = new SignInPage();
            });
        }
    }
}
