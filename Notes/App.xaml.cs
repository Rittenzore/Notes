using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;

using Notes.Views;

namespace Notes
{
    public partial class App : Application
    {
        static NoteDB notesDB;

        public static NoteDB NotesDB
        {
            get
            {
                if (notesDB == null)
                {
                    notesDB = new NoteDB(
                        Path.Combine(
                            Environment.GetFolderPath(
                                Environment.SpecialFolder.MyDocuments),
                                "NoteDB.db"));
                }
                return notesDB;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new SignInPage());
            MainPage = new AppShell();

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
