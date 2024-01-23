using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;


namespace Media_Tracker.Model
{
    public partial class TvShow : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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
