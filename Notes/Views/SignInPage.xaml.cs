using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Notes.Helpers;
using Notes.Models;
using SQLite;
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

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text;
            string password = EntryPassword.Text;

            List<KeyValuePair<string, string>> req = new List<KeyValuePair<string, string>>();
            req.Add(new KeyValuePair<string, string>("username", username));
            req.Add(new KeyValuePair<string, string>("password", password));

            Response res = await HttpRequest.MakePostRequest("/sign-in", req);
            JToken data = res.ResponseData;

            if (res.IsSuccess)
            {
                App.Current.MainPage = new AppShell();
                User user = new User()
                {
                    Id = int.Parse(data["id"].Value<string>()),
                    Username = data["username"].ToString(),
                    Password = data["password"].ToString(),
                    Name = data["name"].ToString(),
                    Email = data["email"].ToString()
                };
                Console.WriteLine(user.Username);
                Console.WriteLine(user.Password);
                Console.WriteLine(user.Name);
                Console.WriteLine(user.Email);
                App.User = user;
                App.Current.Properties["userId"] = user.Id;
                Console.WriteLine(App.User);
                Console.WriteLine("=======");
                Console.WriteLine(App.Current.Properties["userId"]);
                await App.Current.SavePropertiesAsync();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string error_message = data.ToString();

                    var result = await DisplayAlert("Error", error_message, null, "Ok");

                    EntryPassword.Text = "";
                });
            }
        }
    }
}
