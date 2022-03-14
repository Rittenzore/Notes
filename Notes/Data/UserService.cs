using System;
using System.Threading.Tasks;
using Notes.Models;
using SQLite;

namespace Notes.Data
{
    public class UserService
    {
        readonly SQLiteAsyncConnection db;

        public UserService(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<User>().Wait();
        }

        public Task<User> GetUserAsync(int id)
        {
            return db.Table<User>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<User> SignIn(string username, string password)
        {
            return db.Table<User>()
                .Where(u => u.Username.Equals(username) && u.Password.Equals(password))
                .FirstOrDefaultAsync();
        }

        public Task<int> SignUp(User user)
        {
            if (user.Id != 0)
            {
                return db.UpdateAsync(user);
            }
            else
            {
                return db.InsertAsync(user);
            }
        }
    }
}
