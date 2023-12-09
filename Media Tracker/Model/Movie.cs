using CommunityToolkit.Mvvm.ComponentModel;

namespace Media_Tracker.Model
{
    public partial class Movie : ObservableObject 
    {
        [ObservableProperty]
        private string movieTitle;

        [ObservableProperty]
        private double movieLength;

        [ObservableProperty]
        private DateTime releaseDate;

        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private bool isReleased;
    }
}
