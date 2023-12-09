using CommunityToolkit.Mvvm.ComponentModel;

namespace Media_Tracker.Model
{
    public partial class TvShow : ObservableObject
    {
        [ObservableProperty]
        private string tvShowTitle;

        [ObservableProperty]
        private string tvShowSeason;

        [ObservableProperty]
        private DateTime releaseDate;

        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private bool isReleased;
    }
}
