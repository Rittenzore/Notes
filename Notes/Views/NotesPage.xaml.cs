using System;
using Xamarin.Forms;
using Notes.Models;
using Notes.Helpers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Views
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            listView.ItemsSource = await GetNotesFromDatabase();

            base.OnAppearing();
        }

        private async Task<List<Note>> GetNotesFromDatabase()
        {
            Response res = await HttpRequest.MakeGetRequest("/notes/" + App.User.Id);

            if (res.IsSuccess)
            {
                List<Note> n_notes = new List<Note>();
                JObject data = JObject.Parse(res.ResponseData.ToString());
                object[] notes = data["notes"].ToObject<object[]>();
                foreach (object a_note in notes)
                {
                    JObject json_event = JObject.Parse(a_note.ToString());
                    int id = json_event["id"].Value<int>();
                    int userId = json_event["user_id"].Value<int>();
                    string text = json_event["text"].Value<string>();
                    string date = json_event["date"].Value<string>();
                    DateTime dateTime = DateTime.Parse(date);

                    Note new_note = new Note()
                    {
                        Id = id,
                        UserId = userId,
                        Text = text,
                        Date = dateTime
                    };

                    n_notes.Add(new_note);
                }
                Console.WriteLine("============ data ===========");
                Console.WriteLine(notes);
                return n_notes;
            }
            return null;
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
                Response res = await HttpRequest.MakeGetRequest("note/" + App.User.Id + "/" + note.Id);

                if (res.IsSuccess)
                {
                    JObject data = JObject.Parse(res.ResponseData.ToString());

                    int id = data["id"].Value<int>();
                    string text = data["text"].Value<string>();
                    string date = data["date"].Value<string>();
                    DateTime dateTime = DateTime.Parse(date);

                    Note new_note = new Note()
                    {
                        Id = id,
                        UserId = App.User.Id,
                        Text = text,
                        Date = dateTime
                    };

                    await Shell.Current.GoToAsync(
                        $"{nameof(CreateNotePage)}?{nameof(CreateNotePage.ItemId)}={new_note.Id.ToString()}");
                }
            }
        }
    }
}
