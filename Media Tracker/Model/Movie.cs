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

        [ObservableProperty]
        bool isReleased;
    }
}
