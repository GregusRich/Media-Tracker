using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Media_Tracker.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Media_Tracker.ViewModel
{
    public partial class MovieViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Movie> allMovies;

        [ObservableProperty]
        ObservableCollection<Movie> availableMovies;

        [ObservableProperty]
        ObservableCollection<Movie> upcomingMovies;

        [ObservableProperty]
        ObservableCollection<Movie> completedMovies;

        [ObservableProperty]
        string selectedCategory = "All Movies"; // Set "All Movies" as the default selected category

        public MovieViewModel()
        {
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

            LoadMovies();
        }

        private void LoadMovies()
        {
            AddAllMoviesToList();
            FilterAvailableMovies();
            FilterUpcomingMovies();
            FilterCompletedMovies();
        }

        [RelayCommand]
        void ShowMovieList()
        {
            switch (SelectedCategory)
            {
                case "All Movies":
                    AddAllMoviesToList();
                    break;
                case "Available Movies":
                    FilterAvailableMovies();
                    break;
                case "Upcoming Movies":
                    FilterUpcomingMovies();
                    break;
                case "Completed Movies":
                    FilterCompletedMovies();
                    break;
            }
        }

        private void AddAllMoviesToList()
        {
            AllMovies.Clear();
            foreach (var movie in AllMovies)
            {
                AllMovies.Add(movie);
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
