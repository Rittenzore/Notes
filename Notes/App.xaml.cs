using Xamarin.Forms;
using Notes.Data;
using System.IO;
using Notes.Views;
using Xamarin.Essentials;
using Notes.Models;

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
                        Path.Combine(FileSystem.AppDataDirectory, "NoteDB.db"));
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
                        Path.Combine(FileSystem.AppDataDirectory, "UserDB.db"));
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
