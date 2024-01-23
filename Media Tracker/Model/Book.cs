using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Media_Tracker.Model
{
    public partial class Book : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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
