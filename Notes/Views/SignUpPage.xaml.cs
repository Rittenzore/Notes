using System;
using System.Collections.Generic;
using Notes.Models;
using Xamarin.Forms;
using System.Net.Http;

using Newtonsoft.Json;
using Notes.Helpers;
using Newtonsoft.Json.Linq;

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
            string username = EntryUsername.Text;
            string password = EntryPassword.Text;
            string name = EntryName.Text;
            string email = EntryEmail.Text;

            List<KeyValuePair<string, string>> req = new List<KeyValuePair<string, string>>();
            req.Add(new KeyValuePair<string, string>("username", username));
            req.Add(new KeyValuePair<string, string>("password", password));
            req.Add(new KeyValuePair<string, string>("name", name));
            req.Add(new KeyValuePair<string, string>("email", email));

            Response res = await HttpRequest.MakePostRequest("/sign-up", req);
            JToken data = res.ResponseData;

            if (res.IsSuccess)
            {
                App.Current.MainPage = new NavigationPage(new SignInPage());
            }
            else
            {
                await DisplayAlert("Error", res.ResponseData.Value<string>(), "Ok");
            }
        }
    }
}
