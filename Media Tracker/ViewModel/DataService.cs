using CommunityToolkit.Mvvm.ComponentModel;
using Media_Tracker.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Media_Tracker.ViewModel
{
    public class DataService : ObservableObject
    {
        private readonly SQLiteAsyncConnection db;
        private bool isInitialized = false; // To determine if the db has been initialised 

        public DataService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
        }

        private async Task InitDatabaseAsync()
        {
            if (!isInitialized)
            {
            await db.CreateTableAsync<Movie>();
            await db.CreateTableAsync<TvShow>();
            await db.CreateTableAsync<Book>();
            isInitialized = true;
            }
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            await InitDatabaseAsync();
            return await db.Table<Movie>().ToListAsync();
        }

        public async Task<int> AddMovieAsync(Movie movie)
        {
            await InitDatabaseAsync();
            return await db.InsertAsync(movie);
        }

        public async Task<int> DeleteMovieAsync(Movie movie)
        {
            await InitDatabaseAsync();
            return await db.DeleteAsync(movie);
        } 
    }
}
