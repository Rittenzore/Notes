using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;

using Notes.Views;
using Xamarin.Essentials;

namespace Notes
{
    public partial class App : Application
    {
        static NoteDB notesDB;
        static UserDB userDB;

        public static NoteDB NotesDB
        {
            get
            {
                if (notesDB == null)
                {
                    notesDB = new NoteDB(
                        Path.Combine(FileSystem.AppDataDirectory, "NoteDB.db"));
                }
                return notesDB;
            }
        }

        public static UserDB UserDB
        {
            get
            {
                if (userDB == null)
                {
                    userDB = new UserDB(
                        Path.Combine(FileSystem.AppDataDirectory, "UserDB.db"));
                }
                return userDB;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignInPage());
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
