using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Media_Tracker.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Media_Tracker.ViewModel
{
    public partial class TvShowViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<TvShow> allTvShows;

        [ObservableProperty]
        ObservableCollection<TvShow> availableTvShows;

        [ObservableProperty]
        ObservableCollection<TvShow> upcomingTvShows;

        [ObservableProperty]
        ObservableCollection<TvShow> completedTvShows;

        [ObservableProperty]
        ObservableCollection<TvShow> displayedTvShows;

        [ObservableProperty]
        string selectedCategory = "All TvShows"; // Set "All TvShows" as the default selected category

        public TvShowViewModel()
        {
            // Initializing the different types of TvShow Collections
            AllTvShows = new ObservableCollection<TvShow>()
            {
                new TvShow { TvShowTitle = "Silicon Valley", TvShowSeason = "Season 1", EpisodeLength = 28, ReleaseDate = new DateTime(2014, 4, 6), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Silicon Valley", TvShowSeason = "Season 2", EpisodeLength = 28, ReleaseDate = new DateTime(2015, 4, 12), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Rick and Morty", TvShowSeason = "Season 1", EpisodeLength = 22, ReleaseDate = new DateTime(2013, 12, 2), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Breaking Bad", TvShowSeason = "Season 5", EpisodeLength = 47, ReleaseDate = new DateTime(2012, 7, 15), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Game of Thrones", TvShowSeason = "Season 8", EpisodeLength = 80, ReleaseDate = new DateTime(2019, 4, 14), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Stranger Things", TvShowSeason = "Season 4", EpisodeLength = 50, ReleaseDate = new DateTime(2022, 5, 27), IsCompleted = false, IsReleased = true },
                new TvShow { TvShowTitle = "The Mandalorian", TvShowSeason = "Season 2", EpisodeLength = 40, ReleaseDate = new DateTime(2020, 10, 30), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "The Witcher", TvShowSeason = "Season 2", EpisodeLength = 60, ReleaseDate = new DateTime(2021, 12, 17), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Westworld", TvShowSeason = "Season 3", EpisodeLength = 58, ReleaseDate = new DateTime(2020, 3, 15), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Better Call Saul", TvShowSeason = "Season 6", EpisodeLength = 45, ReleaseDate = new DateTime(2022, 4, 18), IsCompleted = false, IsReleased = true },
                // Upcoming shows
                new TvShow { TvShowTitle = "House of the Dragon", TvShowSeason = "Season 1", EpisodeLength = 60, ReleaseDate = new DateTime(2022, 8, 21), IsCompleted = false, IsReleased = false },
                new TvShow { TvShowTitle = "The Last of Us", TvShowSeason = "Season 1", EpisodeLength = 60, ReleaseDate = new DateTime(2023, 1, 15), IsCompleted = false, IsReleased = false },
            };

            AvailableTvShows = new ObservableCollection<TvShow>();
            UpcomingTvShows = new ObservableCollection<TvShow>();
            CompletedTvShows = new ObservableCollection<TvShow>();
            DisplayedTvShows = AllTvShows; // Start by showing all TvShows
        }


        [RelayCommand]
        void ShowTvShowList()
        {
            switch (SelectedCategory)
            {
                case "All TvShows":
                    DisplayedTvShows = AllTvShows;
                    break;
                case "Available TvShows":
                    FilterAvailableTvShows();
                    DisplayedTvShows = AvailableTvShows;
                    break;
                case "Upcoming TvShows":
                    FilterUpcomingTvShows();
                    DisplayedTvShows = UpcomingTvShows;
                    break;
                case "Completed TvShows":
                    FilterCompletedTvShows();
                    DisplayedTvShows = CompletedTvShows;
                    break;
            }
        }

        private void FilterAvailableTvShows()
        {
            AvailableTvShows.Clear();
            foreach (var tvShow in AllTvShows)
            {
                if (tvShow.IsReleased)
                {
                    AvailableTvShows.Add(tvShow);
                }
            }
        }

        private void FilterUpcomingTvShows()
        {
            UpcomingTvShows.Clear();
            foreach (var tvShow in AllTvShows)
            {
                if (!tvShow.IsReleased)
                {
                    UpcomingTvShows.Add(tvShow);
                }
            }
        }

        private void FilterCompletedTvShows()
        {
            CompletedTvShows.Clear();
            foreach (var tvShow in AllTvShows)
            {
                if (tvShow.IsCompleted)
                {
                    CompletedTvShows.Add(tvShow);
                }
            }
        }
    }
}