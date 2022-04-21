using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Notes.Helpers;
using Notes.Models;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            User user = await GetUserFromDatabase();

            Title = $"Hello, {user.Name} 👋";
            EntryEmail.Text = user.Email;
            EntryName.Text = user.Name;

            base.OnAppearing();
        }


        private async Task<User> GetUserFromDatabase()
        {
            Response res = await HttpRequest.MakeGetRequest("user/" + App.User.Id);

            if (res.IsSuccess)
            {
                JObject data = JObject.Parse(res.ResponseData.ToString());

                int id = data["id"].Value<int>();
                string username = data["username"].Value<string>();
                string name = data["name"].Value<string>();
                string email = data["email"].Value<string>();

                User user = new User()
                {
                    Id = id,
                    Username = username,
                    Name = name,
                    Email = email
                };

                return user;
            }
            else
            {
                return null;
            }
        }

        private void LogoutButton_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
                if (result)
                {
                    App.Current.MainPage = new NavigationPage(new SignInPage());
                    App.User = null;
                    App.Current.Properties["userId"] = null;
                }
            });
        }
    }
}
