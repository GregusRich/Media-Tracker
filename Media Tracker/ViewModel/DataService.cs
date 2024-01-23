using CommunityToolkit.Mvvm.ComponentModel;
using Media_Tracker.Model;
using SQLite;

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

        // Movies: CRUD operations
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

        // TvShows: CRUD operations
        public async Task<List<TvShow>> GetTvShowsAsync()
        {
            await InitDatabaseAsync();
            return await db.Table<TvShow>().ToListAsync();
        }

        public async Task<int> AddTvShowAsync(TvShow tvShow)
        {
            await InitDatabaseAsync();
            return await db.InsertAsync(tvShow);
        }

        public async Task<int> DeleteTvShowAsync(TvShow tvShow)
        {
            await InitDatabaseAsync();
            return await db.DeleteAsync(tvShow);
        }

        // Books: CRUD operations
        public async Task<List<Book>> GetBooksAsync()
        {
            await InitDatabaseAsync();
            return await db.Table<Book>().ToListAsync();
        }

        public async Task<int> AddBookAsync(Book book)
        {
            await InitDatabaseAsync();
            return await db.InsertAsync(book);
        }

        public async Task<int> DeleteBookAsync(Book book)
        {
            await InitDatabaseAsync();
            return await db.DeleteAsync(book);
        }
    }
}