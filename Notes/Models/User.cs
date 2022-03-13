using System;
using SQLite;

namespace Notes.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
