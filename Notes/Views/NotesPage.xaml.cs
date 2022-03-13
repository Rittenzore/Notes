using System;
using Xamarin.Forms;
using Notes.Models;
using Xamarin.Essentials;

namespace Notes.Views
{
    public partial class NotesPage : ContentPage
    {
        int userId = Convert.ToInt32(Preferences.Get("userId", 1));

        public NotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            listView.ItemsSource = await App.NotesDB.GetNotesAsync(userId);

            base.OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateNotePage));
        }

        private async void OnSelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Note note = (Note)e.SelectedItem;
                await Shell.Current.GoToAsync(
                    $"{nameof(CreateNotePage)}?{nameof(CreateNotePage.ItemId)}={note.Id.ToString()}");
            }
        }
    }
}
