using System;
using System.Threading.Tasks;
using Notes.Models;
using SQLite;

namespace Notes.Data
{
    public class UserDB
    {
        readonly SQLiteAsyncConnection db;

        public UserDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<User>().Wait();
        }

        public Task<User> SignIn(string username, string password)
        {
            return db.Table<User>()
                .Where(u => u.Username.Equals(username) && u.Password.Equals(password))
                .FirstOrDefaultAsync();
        }
    }
}
