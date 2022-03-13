using System;

using Xamarin.Forms;

namespace Notes.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SignInPage();
        }
    }
}
