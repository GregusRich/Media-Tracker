using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Media_Tracker.Model
{
    public partial class Movie : ObservableObject 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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
