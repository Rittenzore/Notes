using System.Collections.Generic;
using SQLite;
using Notes.Models;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class NoteDB
    {
        readonly SQLiteAsyncConnection db;

        public NoteDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync(int userId)
        {
            return db.Table<Note>().Where(i => i.UserId == userId).ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            return db.Table<Note>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
            {
                return db.UpdateAsync(note);
            }
            else
            {
                return db.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            return db.DeleteAsync(note);
        }
    }
}
