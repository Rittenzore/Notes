using System;
using SQLite;

namespace Notes.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
