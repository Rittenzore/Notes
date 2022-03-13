using System;
using SQLite;

namespace Notes.Models
{
    public class NotesTable
    {
        public int userID { get; set; }
        public Note note { get; set; }
    }
}
