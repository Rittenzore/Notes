using System;
using Notes.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class CreateNotePage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public CreateNotePage()
        {
            InitializeComponent();

            BindingContext = new Note();
        }

        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);

                Note note = await App.NotesDB.GetNoteAsync(id);

                BindingContext = note;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            note.Date = DateTime.UtcNow;

            note.UserId = App.User.Id;

            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            await App.NotesDB.DeleteNoteAsync(note);

            await Shell.Current.GoToAsync("..");
        }
    }
}
