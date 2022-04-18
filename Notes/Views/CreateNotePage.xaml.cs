using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Notes.Helpers;
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

        public Task<Response> HttpResponse { get; private set; }

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

                Response res = await HttpRequest.MakeGetRequest("/note/" + App.User.Id + "/" + id);
                if (res.IsSuccess)
                {
                    JObject data = JObject.Parse(res.ResponseData.ToString());

                    int note_id = data["id"].Value<int>();
                    string text = data["text"].Value<string>();
                    string date = data["date"].Value<string>();
                    DateTime dateTime = DateTime.Parse(date);

                    Note new_note = new Note()
                    {
                        Id = note_id,
                        UserId = App.User.Id,
                        Text = text,
                        Date = dateTime
                    };

                    BindingContext = new_note;
                }
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            note.UserId = App.User.Id;
            
            string text = note.Text;
            DateTime date = note.Date;

            List<KeyValuePair<string, string>> req = new List<KeyValuePair<string, string>>();
            req.Add(new KeyValuePair<string, string>("user_id", note.UserId.ToString()));
            req.Add(new KeyValuePair<string, string>("text", text));

            Response res = await HttpRequest.MakePostRequest("/create-note", req);
            JToken data = res.ResponseData;

            if (res.IsSuccess)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string error_message = data["error"].ToString();

                    var result = await DisplayAlert("Error", error_message, null, "Ok");
                });

            }

        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            await App.NotesDB.DeleteNoteAsync(note);

            await Shell.Current.GoToAsync("..");
        }
    }
}
