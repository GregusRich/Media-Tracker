using CommunityToolkit.Mvvm.ComponentModel;

namespace Media_Tracker.Model
{
    public partial class Movie : ObservableObject 
    {
        [ObservableProperty]
        string movieTitle;

        [ObservableProperty]
        double movieLength;

        [ObservableProperty]
        DateTime releaseDate;

        [ObservableProperty]
        bool isCompleted;

        // This property is computed each time it's called.
        public bool IsReleased => DateTime.Today >= ReleaseDate;
    }
}
