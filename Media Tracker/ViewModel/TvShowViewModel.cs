using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Media_Tracker.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Media_Tracker.ViewModel
{
    public partial class TvShowViewModel : ObservableObject
    {

        public IAsyncRelayCommand NavigateBackAsyncCommand { get; }
        public IAsyncRelayCommand NavigateToAddTvShowViewAsyncCommand { get; }

        public event EventHandler<string> TvShowAdded; // Used for an OK popup when a TvShow is added

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
        private TvShow selectedTvShow;

        [ObservableProperty]
        string selectedCategory = "All TvShows"; // Set "All TvShows" as the default selected category

        [ObservableProperty]
        TvShow newTvShow = new TvShow(); // For adding a new TvShow in the AddTvShowView

        private readonly DataService dataService;

        private bool _isDataLoaded = false;


        public TvShowViewModel(DataService dataService)
        {
            this.dataService = dataService;
            NavigateBackAsyncCommand = new AsyncRelayCommand(NavigateBackAsync);
            NavigateToAddTvShowViewAsyncCommand = new AsyncRelayCommand(NavigateToAddTvShowViewAsync);

            NewTvShow = new TvShow { ReleaseDate = DateTime.Today }; // Setting default date for new TvShow

            // Initializing the different types of TvShow Collections
            AllTvShows = new ObservableCollection<TvShow>()
            {
                new TvShow { TvShowTitle = "Silicon Valley", TvShowSeason = "S01", ReleaseDate = new DateTime(2014, 4, 6), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Silicon Valley", TvShowSeason = "S02", ReleaseDate = new DateTime(2015, 4, 12), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Rick and Morty", TvShowSeason = "S01", ReleaseDate = new DateTime(2013, 12, 2), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Breaking Bad", TvShowSeason = "S05", ReleaseDate = new DateTime(2012, 7, 15), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Game of Thrones", TvShowSeason = "S08", ReleaseDate = new DateTime(2019, 4, 14), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Stranger Things", TvShowSeason = "S04", ReleaseDate = new DateTime(2022, 5, 27), IsCompleted = false, IsReleased = true },
                new TvShow { TvShowTitle = "The Mandalorian", TvShowSeason = "S02", ReleaseDate = new DateTime(2020, 10, 30), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "The Witcher", TvShowSeason = "S02", ReleaseDate = new DateTime(2021, 12, 17), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Westworld", TvShowSeason = "S03", ReleaseDate = new DateTime(2020, 3, 15), IsCompleted = true, IsReleased = true },
                new TvShow { TvShowTitle = "Better Call Saul", TvShowSeason = "S06", ReleaseDate = new DateTime(2022, 4, 18), IsCompleted = false, IsReleased = true },
                // Upcoming shows
                new TvShow { TvShowTitle = "House of the Dragon", TvShowSeason = "S02", ReleaseDate = new DateTime(2025, 8, 21), IsCompleted = false, IsReleased = false },
                new TvShow { TvShowTitle = "The Last of Us", TvShowSeason = "S02", ReleaseDate = new DateTime(2024, 6, 15), IsCompleted = false, IsReleased = false },
            };

            AvailableTvShows = new ObservableCollection<TvShow>();
            UpcomingTvShows = new ObservableCollection<TvShow>();
            CompletedTvShows = new ObservableCollection<TvShow>();
            DisplayedTvShows = AllTvShows; // Start by showing all TvShows
        }

        // Generates test tv shows if database doesn't have any tv shows. 
        public async Task CreateTestTvShowsAsync()
        {
            foreach (var tvShow in AllTvShows)
            {
                await dataService.AddTvShowAsync(tvShow); // Add each tv show to the database
            }
        }

        [RelayCommand]
        private async Task NavigateBackAsync()
        {
            try
            {
                Debug.WriteLine("Back button clicked \n");
                await Shell.Current.GoToAsync("//TvShowView");
                ShowTvShowList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation Error: {ex.Message}");
            }
        }


        [RelayCommand]
        private async Task AddTvShow()
        {
            if (NewTvShow != null)
            {
                Debug.WriteLine("NewTvShow is not null. Attempting to add TV show to database\n");
                await dataService.AddTvShowAsync(NewTvShow);
                AllTvShows.Add(NewTvShow);
                TvShowAdded?.Invoke(this, NewTvShow.TvShowTitle);
                Debug.WriteLine($"TV Show {NewTvShow.TvShowTitle} has been added.\n");
                NewTvShow = new TvShow() { ReleaseDate = DateTime.Today };
            }
            else
            {
                Debug.WriteLine("Unable to add TV show, TV show cannot be 'null'\n");
            }
        }

        [RelayCommand]
        private async Task DeleteTvShow()
        {
            if (SelectedTvShow != null)
            {
                try
                {
                    Debug.WriteLine($"Attempting to delete TV show: {SelectedTvShow.TvShowTitle}");
                    await dataService.DeleteTvShowAsync(SelectedTvShow);
                    AllTvShows.Remove(SelectedTvShow);
                    Debug.WriteLine($"TV Show {SelectedTvShow.TvShowTitle} has been deleted.\n");
                    SelectedTvShow = null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting TV show: {ex.Message}\n");
                }
            }
            else
            {
                Debug.WriteLine("No TV show selected to delete.\n");
            }
        }

        public async Task LoadTvShowsAsync()
        {
            if (!_isDataLoaded)
            {
                var tvShowsFromDb = await dataService.GetTvShowsAsync();
                foreach (var tvShow in tvShowsFromDb)
                {
                    AllTvShows.Add(tvShow);
                }
                _isDataLoaded = true;
            }
        }



        [RelayCommand]
        private async Task NavigateToAddTvShowViewAsync()
        {
            Debug.WriteLine("Navigating to AddTvShowView button clicked \n");
            await Shell.Current.GoToAsync("//AddTvShowView");
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