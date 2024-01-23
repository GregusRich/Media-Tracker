using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Media_Tracker.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Media_Tracker.ViewModel
{
    public partial class MovieViewModel : ObservableObject
    {
        public IAsyncRelayCommand NavigateBackAsyncCommand { get; }
        public IAsyncRelayCommand NavigateToAddMovieViewAsyncCommand { get; }

        public event EventHandler<string> MovieAdded; // Used for an OK popup when a movie is added

        [ObservableProperty]
        ObservableCollection<Movie> allMovies;

        [ObservableProperty]
        ObservableCollection<Movie> availableMovies;

        [ObservableProperty]
        ObservableCollection<Movie> upcomingMovies;

        [ObservableProperty]
        ObservableCollection<Movie> completedMovies;

        [ObservableProperty]
        ObservableCollection<Movie> displayedMovies;

        [ObservableProperty]
        private Movie selectedMovie;

        [ObservableProperty]
        string selectedCategory = "All Movies"; // Set "All Movies" as the default selected category

        [ObservableProperty]
        Movie newMovie = new Movie(); // For adding a new movie in the AddMovieView

        private readonly DataService dataService;

        private bool _isDataLoaded = false;

        public MovieViewModel(DataService dataService)
        {
            this.dataService = dataService;
            NavigateBackAsyncCommand = new AsyncRelayCommand(NavigateBackAsync);
            NavigateToAddMovieViewAsyncCommand = new AsyncRelayCommand(NavigateToAddMovieViewAsync);

            NewMovie = new Movie { ReleaseDate = DateTime.Today }; // Setting default date

            // Initialising the different types of Movie Collections
            AllMovies = new ObservableCollection<Movie>()
            {
                new Movie { MovieTitle = "Avatar", MovieLength = 148, ReleaseDate = new DateTime(2009, 1, 1), IsCompleted = false, IsReleased = true },
                new Movie { MovieTitle = "The Dark Knight", MovieLength = 152, ReleaseDate = new DateTime(2008, 1, 1), IsCompleted = false, IsReleased = true },
                new Movie { MovieTitle = "Inception", MovieLength = 148, ReleaseDate = new DateTime(2010, 1, 1), IsCompleted = false , IsReleased = true },
                new Movie { MovieTitle = "Titanic 2", MovieLength = 194, ReleaseDate = new DateTime(1997, 1, 1), IsCompleted = true, IsReleased = true },
                new Movie { MovieTitle = "Gladiator", MovieLength = 155, ReleaseDate = new DateTime(2000, 1, 1), IsCompleted = false , IsReleased = true },
                new Movie { MovieTitle = "Interstellar", MovieLength = 169, ReleaseDate = new DateTime(2014, 1, 1), IsCompleted = false , IsReleased = true },
                new Movie { MovieTitle = "The Matrix", MovieLength = 136, ReleaseDate = new DateTime(1999, 1, 1), IsCompleted = false , IsReleased = true },
                new Movie { MovieTitle = "Frozen 2", MovieLength = 109, ReleaseDate = new DateTime(2024, 2, 1), IsCompleted = false, IsReleased = false },
                new Movie { MovieTitle = "John Wick 13", MovieLength = 101, ReleaseDate = new DateTime(2024, 5, 1), IsCompleted = false, IsReleased = false },
                new Movie { MovieTitle = "Toy Story 4", MovieLength = 81, ReleaseDate = new DateTime(2025, 4, 10), IsCompleted = false, IsReleased = false },
            };

            AvailableMovies = new ObservableCollection<Movie>();
            UpcomingMovies = new ObservableCollection<Movie>();
            CompletedMovies = new ObservableCollection<Movie>();
            DisplayedMovies = AllMovies; // Start by showing all movies
        }

        // Generates test movies if database doesn't have any movies. 
        public async Task CreateTestMoviesAsync()
        {
            foreach (var movie in AllMovies)
            {
                await dataService.AddMovieAsync(movie); // Add each movie to the database
            }
        }


        [RelayCommand]
        private async Task NavigateBackAsync()
        {
            try
            {
                Debug.WriteLine("Back button clicked \n");
                await Shell.Current.GoToAsync("//MovieView");
                ShowMovieList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation Error: {ex.Message}");
            }
        }

        // Only used in the AddMovieView to add a movie
        [RelayCommand]
        private async Task AddMovie()
        {
            try
            {
                // If Movie object is not null 
                if (NewMovie != null)

                {
                    Debug.WriteLine("NewMovie is not null. Attempting to add movie to run dataService.AddMovieAsync()\n\n");
                    await dataService.AddMovieAsync(NewMovie); // Add to db first
                    AllMovies.Add(NewMovie); // Then add to collection 
                    MovieAdded?.Invoke(this, NewMovie.MovieTitle); // Raise the event
                    Debug.WriteLine($"Movie {NewMovie.MovieTitle} has been added to the 'All Movies Collection'. \n\n");
                    NewMovie = new Movie() { ReleaseDate = DateTime.Today }; // Reset for next entry
                }

                else
                {
                    Debug.WriteLine("Unable to add movie, movie cannot be 'null' \n"); 
                }
            }

            catch (Exception ex)
            { 
                // Log the exception
                Debug.WriteLine($"Unable to add movie error: {ex.Message}");
            }
        }

        public async Task LoadMoviesAsync()
        {
            if (!_isDataLoaded)
            {
                // Load movies from the database
                var moviesFromDb = await dataService.GetMoviesAsync();
                foreach (var movie in moviesFromDb)
                {
                    AllMovies.Add(movie);
                }
                _isDataLoaded = true;
            }
        }

        [RelayCommand]
        public async Task DeleteMovie()
        {
            if (SelectedMovie != null)
            {
                try
                {
                    Debug.WriteLine($"Attempting to delete movie: {SelectedMovie.MovieTitle}");
                    await dataService.DeleteMovieAsync(SelectedMovie);
                    AllMovies.Remove(SelectedMovie);
                    Debug.WriteLine($"Movie {SelectedMovie.MovieTitle} has been successfully deleted.\n\n");
                    SelectedMovie = null; // Reset the selected movie
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting movie: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("No movie selected to delete.\n\n");
            }
        }


        // Navigate to the Add Movie Page
        [RelayCommand]
        private async Task NavigateToAddMovieViewAsync()
        {
            Debug.WriteLine("Navigating to AddMovieView button clicked \n");
            await Shell.Current.GoToAsync("//AddMovieView");
        }


        [RelayCommand]
        void ShowMovieList()
        {
            switch (SelectedCategory)
            {
                case "All Movies":
                    DisplayedMovies = AllMovies;
                    break;
                case "Available Movies":
                    FilterAvailableMovies();
                    DisplayedMovies = AvailableMovies;
                    break;
                case "Upcoming Movies":
                    FilterUpcomingMovies();
                    DisplayedMovies = UpcomingMovies;
                    break;
                case "Completed Movies":
                    FilterCompletedMovies();
                    DisplayedMovies = CompletedMovies;
                    break;
            }
        }


        private void FilterAvailableMovies()
        {
            AvailableMovies.Clear();
            foreach (var movie in AllMovies)
            {
                if (movie.IsReleased)
                {
                    AvailableMovies.Add(movie);
                }
            }
        }

        private void FilterUpcomingMovies()
        {
            UpcomingMovies.Clear();
            foreach (var movie in AllMovies)
            {
                if (!movie.IsReleased)
                {
                    UpcomingMovies.Add(movie);
                }
            }
        }

        private void FilterCompletedMovies()
        {
            CompletedMovies.Clear();
            foreach (var movie in AllMovies)
            {
                if (movie.IsCompleted)
                {
                    CompletedMovies.Add(movie);
                }
            }
        }
    }
}
