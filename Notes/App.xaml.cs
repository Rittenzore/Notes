using Xamarin.Forms;
using Notes.Data;
using System.IO;
using Notes.Views;
using Xamarin.Essentials;
using Notes.Models;
using System;
using Notes.Helpers;

namespace Notes
{
    public partial class App : Application
    {
        static NoteService notesDB;
        static UserService userDB;
        static User user;

        public static NoteService NotesDB
        {
            get
            {
                if (notesDB == null)
                {
                    notesDB = new NoteService(
                        Path.Combine(FileSystem.AppDataDirectory, "Note.db"));
                }
                return notesDB;
            }
        }

        public static UserService UserDB
        {
            get
            {
                if (userDB == null)
                {
                    userDB = new UserService(
                        Path.Combine(FileSystem.AppDataDirectory, "User.db"));
                }
                return userDB;
            }
        }

        public static User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignInPage());
        }

        protected override void OnStart()
        {
            if (App.Current.Properties.ContainsKey("userId"))
            {
                var userId = Convert.ToInt32(App.Current.Properties["userId"]);
                User user = new User()
                {
                    Id = userId
                };
                App.User = user;
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new SignInPage());
                Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
