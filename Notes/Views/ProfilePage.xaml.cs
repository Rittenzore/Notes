﻿using System;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            nameLabel.Text = $"Hello, {App.User.Name} 👋";
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
                if (result)
                {
                    App.Current.MainPage = new SignInPage();
                    App.User = null;
                }
            });
        }
    }
}
