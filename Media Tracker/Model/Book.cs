using CommunityToolkit.Mvvm.ComponentModel;

namespace Media_Tracker.Model
{
    public partial class Book : ObservableObject
    {
        [ObservableProperty]
        string bookTitle;

        [ObservableProperty]
        string author;

        [ObservableProperty]
        DateTime releaseDate;

        [ObservableProperty]
        int rating;

        [ObservableProperty]
        bool isCompleted;

        // This property is computed each time it's called.
        public bool IsReleased => DateTime.Today >= ReleaseDate;
    }
}
