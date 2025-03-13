using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginRegistrationApp
{
    public class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        // Register a new user
        public Task<int> RegisterUser(User user)
        {
            return _database.InsertAsync(user);
        }

        // Check if user exists during login
        public Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        // Check if username already exists
        public Task<User> GetUserByUsername(string username)
        {
            return _database.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        // Get all users (Read All)
        public Task<List<User>> GetAllUsers()
        {
            return _database.Table<User>().ToListAsync();
        }

        // Update user details
        public Task<int> UpdateUser(User user)
        {
            return _database.UpdateAsync(user);
        }

        // Delete a user
        public Task<int> DeleteUser(User user)
        {
            return _database.DeleteAsync(user);
        }
    }
}