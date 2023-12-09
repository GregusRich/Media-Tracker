using CommunityToolkit.Mvvm.ComponentModel;

namespace Media_Tracker.Model
{
    public partial class Book : ObservableObject
    {
        [ObservableProperty]
        private string bookTitle;

        [ObservableProperty]
        private string author;

        [ObservableProperty]
        private DateTime releaseDate;

        [ObservableProperty]
        private double pages;

        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private bool isReleased;
    }
}
