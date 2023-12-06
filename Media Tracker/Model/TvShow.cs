using CommunityToolkit.Mvvm.ComponentModel;

namespace Media_Tracker.Model
{
    public partial class TvShow : ObservableObject
    {
        [ObservableProperty]
        string tvShowTitle;

        [ObservableProperty]
        double episodeLength;

        [ObservableProperty]
        DateTime releaseDate;

        [ObservableProperty]
        bool isCompleted;

        // Computed property that determines if the TV show is released
        public bool IsReleased => DateTime.Today >= ReleaseDate;
    }
}
